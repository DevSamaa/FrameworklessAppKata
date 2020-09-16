using System;
using System.Net;
using System.Text;

namespace FrameworklessAppKata
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new HttpListener();
            // This is built into C#, it's a listener, it waits for an HTTP request
            
            server.Prefixes.Add("http://localhost:8080/");
            //8080 can be changed to another number like 5050
            //house example: localhost is my house,8080 is my door, if a friend wants to talk to me they have to come to that door!
            
            server.Start();
            //in the previous step you were setting up your house + door, now you're actually listening...waiting!
            
            while (true)
            {
                var context = server.GetContext();  // Wait for a type of HTTP request(example: GET, or POST). It's always just one method! This is like your friend asking you a question
                
                Console.WriteLine($"{context.Request.HttpMethod} {context.Request.Url}");
                //This records which question your friend asked you, in this case, it was a GET request + the URL

                var incomingBody = context.Request.InputStream; //sandy s,a,n,d,y   76,87,45
                var length = context.Request.ContentLength64;

                var incomingBodyBytes = new byte[length]; //this is empty to begin with
                int n = incomingBody.Read(incomingBodyBytes, 0, (int)length); // puts incomingBody into the bytes array
                var incomingBodyString = Encoding.Default.GetString(incomingBodyBytes);

                var buffer = System.Text.Encoding.UTF8.GetBytes($"Hello Bob and {incomingBodyString} {DateTime.Now}");
                // buffer = [56,76,34,23,54] the numbers are the ascii values of letters, they are probably stored in binary
                
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer,0, buffer.Length);  // forces send of response
                //This is how you respond to your friend
                
                incomingBody.Close();
                context.Response.OutputStream.Close();
            }
            server.Stop();  // never reached...
        }
        
        
    }
}
//TODO research and find out when you should use GET/POST request and what you want to return to them.

//This will display the thing you type in the URL bar http://localhost:8080/?name=SANDYisHere!
// var incomingName = context.Request.QueryString;
// var buffer = System.Text.Encoding.UTF8.GetBytes($"Hello {incomingName["name"]} {DateTime.Now}");


//var buffer = System.Text.Encoding.UTF8.GetBytes($"Hello Bob and {incomingBodyString} {DateTime.Now}");
//  128 64 32 16     8 4 2 1
// 0111 1000, 8 bit = 1 byte
// buffer = [1001111,10001111,10001111,10001111,10001111]

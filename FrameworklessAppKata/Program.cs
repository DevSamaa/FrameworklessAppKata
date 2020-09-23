using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            
            var userNames = new List<string>();
            userNames.Add("Bob");
            
            while (true)
            {
                var context = server.GetContext();  // Wait for a type of HTTP request(example: GET, or POST). It's always just one method! This is like your friend asking you a question
                Console.WriteLine($"{context.Request.HttpMethod} {context.Request.Url}");
                //This records which question your friend asked you, in this case, it was a GET request + the URL

                if (context.Request.HttpMethod == "GET")
                {
                    var rawURL = context.Request.RawUrl;
                    var showMessage = "";
                    if (rawURL == "/list")
                    {
                        showMessage =string.Join(",", userNames);
                    }
                    else
                    {
                        showMessage = GetRequestMessage(userNames); 
                    }
                    
                    var buffer = System.Text.Encoding.UTF8.GetBytes(showMessage);
                    context.Response.ContentLength64 = buffer.Length;
                    context.Response.OutputStream.Write(buffer,0, buffer.Length);  // forces send of response
                    //This is how you respond to your friend 
                }
                
                if (context.Request.HttpMethod == "POST")
                {
                    var bodyString = GetStringFromStream2(context);
                    if (userNames.Contains(bodyString))
                    {
                        context.Response.StatusCode = (int) HttpStatusCode.Conflict;
                        //TODO display a message with the correct issue "username already exists"
                        
                        // var buffer = System.Text.Encoding.UTF8.GetBytes(showMessage);
                        // context.Response.ContentLength64 = buffer.Length;
                        // context.Response.OutputStream.Write(buffer,0, buffer.Length);  // forces send of response
                    }
                    else
                    {
                        userNames.Add(bodyString);
                    }
                    //check whether the "bodyString" already exists (userNames)
                    //if it's already inside userNames, don't add it +return a statuscode that indicates what is happening.
                    //otherwise:
                }

                if (context.Request.HttpMethod =="PUT")
                {
                    // localhost:8080/Samaaaa
                    // body: Samaa
                    
                    //what I need to change
                    var oldEntry=context.Request.RawUrl.Substring(1);

                    //what I want to change it to
                    var newEntry = GetStringFromStream2(context);
                    
                    //find Samaa in the list and update it to "bodyString"
                    var indexOfOldEntry = userNames.IndexOf(oldEntry);
                    userNames[indexOfOldEntry] = newEntry;
                }

                if (context.Request.HttpMethod =="DELETE")
                { 
                    var rawURL=context.Request.RawUrl;
                    var name = rawURL.Substring(1);
                   
                    if (userNames.Contains(name)&& name !="Bob")
                    {
                        userNames.Remove(name);
                    }
                    else
                    {
                        // context.Response.StatusCode = 404;
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                    }
                }
                context.Response.OutputStream.Close();

            }
            server.Stop();  // never reached...
        }

        private static string GetRequestMessage(List<string> userNames)
        {
            var placeholder = string.Join(",",userNames);
            return $"Hello {placeholder} {DateTime.Now}";
        }

        private static string GetStringFromStream(HttpListenerContext context)
        {
            var body = context.Request.InputStream; 
            var length = context.Request.ContentLength64;
            var bodyBytes = new byte[length]; 
            int n = body.Read(bodyBytes, 0, (int)length); 
            body.Close();
            return Encoding.Default.GetString(bodyBytes);
        }
        
        private static string GetStringFromStream2(HttpListenerContext context)
        {
            StreamReader stream = new StreamReader(context.Request.InputStream);
            return stream.ReadToEnd();
        }
        
        
    }
}


//This will display the thing you type in the URL bar http://localhost:8080/?name=SANDYisHere!
// var incomingName = context.Request.QueryString;
// var buffer = System.Text.Encoding.UTF8.GetBytes($"Hello {incomingName["name"]} {DateTime.Now}");

//var buffer = System.Text.Encoding.UTF8.GetBytes($"Hello Bob and {incomingBodyString} {DateTime.Now}");
//  128 64 32 16     8 4 2 1
// 0111 1000, 8 bit = 1 byte
// buffer = [1001111,10001111,10001111,10001111,10001111]

// var buffer = System.Text.Encoding.UTF8.GetBytes($"Hello Bob and {userNames[0]} {DateTime.Now}");
// buffer = [56,76,34,23,54] the numbers are the ascii values of letters, they are probably stored in binary


//TODO figure out how to modularize the methods, how to make your code more testable
//you probably have to run your whole program and write a test that sends GET and POST requests to localhost.
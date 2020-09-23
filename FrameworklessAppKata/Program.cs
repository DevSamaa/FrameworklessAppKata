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
                    var message = "";
                    if (rawURL == "/list")
                    {
                        message =string.Join(",", userNames);
                    }
                    else
                    {
                        message = GetRequestMessage(userNames); 
                    }
                    SendResponse(message,context);
                }
                
                if (context.Request.HttpMethod == "POST")
                {
                    var bodyString = GetStringFromStream2(context);
                    if (userNames.Contains(bodyString))
                    {
                        context.Response.StatusCode = (int) HttpStatusCode.Conflict;
                        //TODO display a message with the correct issue "username already exists"

                        var message = "This username already exists.";
                        SendResponse(message,context);
                    }
                    else
                    {
                        userNames.Add(bodyString);
                    }
                   
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

        private static void SendResponse(string message, HttpListenerContext context)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes(message);
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer,0, buffer.Length);
        }
        
        private static string GetStringFromStream2(HttpListenerContext context)
        {
            var stream = new StreamReader(context.Request.InputStream);
            return stream.ReadToEnd();
        }
        
        
    }
}

//TODO figure out how to modularize the methods, how to make your code more testable
//you probably have to run your whole program and write a test that sends GET and POST requests to localhost.
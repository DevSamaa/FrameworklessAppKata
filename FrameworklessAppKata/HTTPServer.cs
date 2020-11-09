using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FrameworklessAppKata
{
    public class HTTPServer
    {
        public HttpListener _server;

        public HTTPServer()
        {
            _server = new HttpListener();
            // This is built into C#, it's a listener, it waits for an HTTP request
        }
     
        public Tuple<CancellationTokenSource, Task>  Run()
        {
            _server.Prefixes.Add("http://*:8080/");
            //8080 can be changed to another number like 5050 -->house example: localhost is my house,8080 is my door, if a friend wants to talk to me they have to come to that door!
            
            _server.Start();
            //in the previous step you were setting up your house + door, now you're actually listening...waiting!
            
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            
            var task = Task.Run(() =>
            {
                  var userNames = new List<string>();
                  userNames.Add("Bobby333");
                  Console.WriteLine($" new thread is going into the loop {DateTime.Now.ToString("hh:MM:ss:fff")}");

                    while (!token.IsCancellationRequested)
                    {
                        Console.WriteLine($" new thread is waiting for a request {DateTime.Now.ToString("hh:MM:ss:fff")}");

                        var context = _server.GetContext();  // Wait for a type of HTTP request(example: GET, or POST). It's always just one method! This is like your friend asking you a question
                        Console.WriteLine($" new thread gets a request {DateTime.Now.ToString("hh:MM:ss:fff")}");
                        Console.WriteLine($"{context.Request.HttpMethod} {context.Request.Url}");
                        //This records which question your friend asked you, in this case, it was a GET request + the URL

                      
                        switch (context.Request.HttpMethod)
                        {
                            case "GET":
                                var getRequest = new GetRequest();
                                getRequest.Run(context, userNames);
                                break;
                            case "POST":
                                var postRequest = new PostRequest();
                                postRequest.Run(context, userNames);
                                break;
                        }
                        

                        if (context.Request.HttpMethod =="PUT")
                        {
                            //what I need to change
                            var oldEntry=context.Request.RawUrl.Substring(1);

                            //what I want to change it to
                            var newEntry = GetStringFromStream(context);
                            
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
                                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                            }
                        }
                        context.Response.OutputStream.Close(); 
                    }
                    Console.WriteLine($" new thread is going to stop the server {DateTime.Now.ToString("hh:MM:ss:fff")}");
                    _server.Stop();  
                           
            }, tokenSource.Token);

            return new Tuple<CancellationTokenSource, Task>(tokenSource,task);
        }
        
        
        private string GetStringFromStream(HttpListenerContext context)
        {
            var stream = new StreamReader(context.Request.InputStream);
            return stream.ReadToEnd();
        }
         
    }
}
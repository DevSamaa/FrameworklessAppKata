using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FrameworklessAppKata
{
    public class HTTPApp
    {
        private readonly HttpListener _server;
        private readonly RequestRouter _requestRouter;
        public readonly CancellationTokenSource CancellationTokenSource;


        public HTTPApp()
        {
            _server = new HttpListener();
            // This is built into C#, it's a listener, it waits for an HTTP request
            _requestRouter = new RequestRouter();
            CancellationTokenSource = new CancellationTokenSource();
        }

        public Task Run(string uri)
        {
            _server.Prefixes.Add(uri);
            //8080 can be changed to another number like 5050 -->house example: localhost is my house,8080 is my door, if a friend wants to talk to me they have to come to that door!
            _server.Start();
            //in the previous step you were setting up your house + door, now you're actually listening...waiting!

            //Task is like my racetrack, i can have multiple tasks/race tracks going at the same time
            //CancellationTokenSource is like my traffic controller (person controlling the lights, brains of the operation)
            // and cancellationTokenSource.Token (so the.Token part) is the actual traffic light

            var cancellationToken = CancellationTokenSource.Token;
            
            var task = Task.Run(() =>
                {
                      var userNames = new List<string>();
                      userNames.Add("Bob");
                      
                        while (!cancellationToken.IsCancellationRequested)
                        {
                            var context = _server.GetContext();  // Wait for a type of HTTP request(example: GET, or POST). It's always just one method! This is like your friend asking you a question
                            Console.WriteLine($"{context.Request.HttpMethod} {context.Request.Url}");
                            //This records which question your friend asked you, in this case, it was a GET request + the URL
                      
                            var request = _requestRouter.Decide(context);
                            request.Process(context, userNames);
                            context.Response.OutputStream.Close(); 
                        }
                        _server.Stop();
                    
                }, cancellationToken);
            
            return task;
        }
        

    }
}
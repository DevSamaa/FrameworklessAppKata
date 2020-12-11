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
        public static string FirstUser;


        public HTTPApp()
        {
            _server = new HttpListener();
            _requestRouter = new RequestRouter();
            CancellationTokenSource = new CancellationTokenSource();
            FirstUser = Environment.GetEnvironmentVariable("SECRET_USERNAME");
            if (string.IsNullOrEmpty(FirstUser))
            {
                FirstUser = "DefaultBob";
            }
        }

        public Task Run(string uri)
        {
            _server.Prefixes.Add(uri);
            _server.Start();
            var cancellationToken = CancellationTokenSource.Token;
            
            var task = Task.Run(() =>
                {
                      var userNames = new List<string>();
                      userNames.Add(FirstUser);
                      
                        while (!cancellationToken.IsCancellationRequested)
                        {
                            var context = _server.GetContext();  
                            Console.WriteLine($"{context.Request.HttpMethod} {context.Request.Url}");

                            var apiTokenIsValid = ApiTokenIsValid(context);
                            if (apiTokenIsValid)
                            {
                                var request = _requestRouter.Decide(context);
                                request.Process(context, userNames);
                            }
                            else
                            {
                                context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                            }
                            context.Response.OutputStream.Close();
                        }
                        _server.Stop();
                    
                }, cancellationToken);
            
            return task;
        }


        private bool ApiTokenIsValid(HttpListenerContext context)
        {
            return !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("API-TOKEN-SECRET")) && Environment.GetEnvironmentVariable("API-TOKEN-SECRET") == context.Request.Headers["API-token"];
        }
    }
}
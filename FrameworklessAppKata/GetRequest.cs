using System;
using System.Collections.Generic;
using System.Net;

namespace FrameworklessAppKata
{
    public class GetRequest
    {
        private readonly ResponseHelper _responseHelper;

        public GetRequest(ResponseHelper responseHelper)
        {
            _responseHelper = responseHelper;
        }
        public void Run(HttpListenerContext context, List<string>userNames)
        {
            var rawUrl = context.Request.RawUrl;
            var message = GetResponseMessage(rawUrl, userNames);
            _responseHelper.SendResponse(message, context);
            Console.WriteLine($" new thread sends a response {DateTime.Now:hh:MM:ss:fff}");
        }

        private string GetResponseMessage(string rawUrl, List<string> userNames)
        {
            var message = "";
            if (rawUrl == "/list")
            {
                message = string.Join(",", userNames);
            }
            else
            {
                var placeholder = string.Join(",", userNames);
                message = $"Hello {placeholder} {DateTime.Now}";
            }

            return message;
        }

       
    }
}
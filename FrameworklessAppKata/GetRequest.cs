using System;
using System.Collections.Generic;
using System.Net;

namespace FrameworklessAppKata
{
    public class GetRequest
    {
        public void Run(HttpListenerContext context, List<string>userNames)
        {
            var rawUrl = context.Request.RawUrl;
            var message = GetResponseMessage(rawUrl, userNames);
            SendResponse(message, context);
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

        //TODO: this is a duplicate, it's also in the HTTPServer class, figure out how to fix this, perhaps a separate class!?
        private void SendResponse(string message, HttpListenerContext context)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes(message);
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer,0, buffer.Length);
        }
    }
}
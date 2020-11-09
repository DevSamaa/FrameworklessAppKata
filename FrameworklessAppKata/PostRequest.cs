using System.Collections.Generic;
using System.IO;
using System.Net;

namespace FrameworklessAppKata
{
    public class PostRequest
    {
        public void Run(HttpListenerContext context, List<string> userNames)
        {
            var bodyString = GetStringFromStream(context);
            if (userNames.Contains(bodyString))
            {
                context.Response.StatusCode = (int) HttpStatusCode.Conflict;
                var message = "This username already exists.";
                SendResponse(message,context);
            }
            else
            {
                userNames.Add(bodyString);
            }
        }
        
        //The methods below are duplicates, figure out what to do with them.
        private void SendResponse(string message, HttpListenerContext context)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes(message);
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer,0, buffer.Length);
        }
        
        private string GetStringFromStream(HttpListenerContext context)
        {
            var stream = new StreamReader(context.Request.InputStream);
            return stream.ReadToEnd();
        }
    }
}
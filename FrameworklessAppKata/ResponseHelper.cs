using System.IO;
using System.Net;

namespace FrameworklessAppKata
{
    public class ResponseHelper
    {
        public void SendResponse(string message, HttpListenerContext context)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes(message);
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer,0, buffer.Length);
        }
        
    }
}
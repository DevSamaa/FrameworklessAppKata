using System.IO;
using System.Net;
using System.Net.Mime;

namespace FrameworklessAppKata
{
    public class ResponseHelper
    {
        public void SendResponse(string message, HttpListenerContext context)
        {
            // message = "{ \"name\":\"JASONBobby\" }";
            var buffer = System.Text.Encoding.UTF8.GetBytes(message);
            // context.Response.ContentType = "application/json";
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer,0, buffer.Length);
        }
        
    }
}
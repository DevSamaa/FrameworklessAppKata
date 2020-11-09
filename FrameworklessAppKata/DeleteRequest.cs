using System.Collections.Generic;
using System.Net;

namespace FrameworklessAppKata
{
    public class DeleteRequest
    {
        public void Run(HttpListenerContext context, List<string> userNames)
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
    }
}
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace FrameworklessAppKata
{
    public class PutRequest:IRequest
    {
        public void Process(HttpListenerContext context, List<string> userNames)
        {
            var oldEntry = context.Request.RawUrl.Substring(1);

            var newEntry = new StreamReader(context.Request.InputStream).ReadToEnd();

            if (!userNames.Contains(oldEntry) && !userNames.Contains(newEntry))
            {
                userNames.Add(newEntry);
                context.Response.StatusCode = (int) HttpStatusCode.Created;
            }
            else if (userNames.Contains(newEntry))
            {
                context.Response.StatusCode = (int) HttpStatusCode.Conflict;
            }
            else
            {
                var indexOfOldEntry = userNames.IndexOf(oldEntry);
                userNames[indexOfOldEntry] = newEntry;
            }
            

            
        }
    }
}
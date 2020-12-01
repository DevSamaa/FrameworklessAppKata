using System.Collections.Generic;
using System.IO;
using System.Net;

namespace FrameworklessAppKata
{
    public class PutRequest:IRequest
    {
        public void Process(HttpListenerContext context, List<string> userNames)
        {
            //what I need to change 
            var oldEntry = context.Request.RawUrl.Substring(1);

            //what I want to change it to (body of request)
            var newEntry = new StreamReader(context.Request.InputStream).ReadToEnd();

            //find Samaa in the list and update it to "bodyString"
            if (userNames.Contains(oldEntry) == false)
            {
                userNames.Add(newEntry);
                context.Response.StatusCode = (int) HttpStatusCode.Created;
            }
            else
            {
                var indexOfOldEntry = userNames.IndexOf(oldEntry);
                userNames[indexOfOldEntry] = newEntry;
            }
           
        }
    }
}
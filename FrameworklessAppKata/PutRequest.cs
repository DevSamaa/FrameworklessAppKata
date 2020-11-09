using System.Collections.Generic;
using System.IO;
using System.Net;

namespace FrameworklessAppKata
{
    public class PutRequest
    {
        public void Run(HttpListenerContext context, List<string> userNames)
        {
            //what I need to change
            var oldEntry=context.Request.RawUrl.Substring(1);

            //what I want to change it to
            var newEntry = GetStringFromStream(context);
                            
            //find Samaa in the list and update it to "bodyString"
            var indexOfOldEntry = userNames.IndexOf(oldEntry);
            userNames[indexOfOldEntry] = newEntry;
        }
        
        //below is a duplicate, figure out what to do with it
        private string GetStringFromStream(HttpListenerContext context)
        {
            var stream = new StreamReader(context.Request.InputStream);
            return stream.ReadToEnd();
        }
    }
    
    
}
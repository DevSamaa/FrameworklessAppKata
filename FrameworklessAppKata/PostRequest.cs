using System.Collections.Generic;
using System.IO;
using System.Net;

namespace FrameworklessAppKata
{
    public class PostRequest:IRequest
    {
        private readonly ResponseHelper _responseHelper;

        public PostRequest(ResponseHelper responseHelper)
        {
            _responseHelper = responseHelper;
        }
        public void Process(HttpListenerContext context, List<string> userNames)
        {
            var bodyString = new StreamReader(context.Request.InputStream).ReadToEnd();
            if (userNames.Contains(bodyString))
            {
                context.Response.StatusCode = (int) HttpStatusCode.Conflict;
                var message = "This username already exists.";
                _responseHelper.SendResponse(message, context);
            }
            else
            {
                userNames.Add(bodyString);
            }
        }
        
        
    }
}
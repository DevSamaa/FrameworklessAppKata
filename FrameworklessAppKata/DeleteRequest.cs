using System;
using System.Collections.Generic;
using System.Net;

namespace FrameworklessAppKata
{
    public class DeleteRequest:IRequest
    {
        private readonly ResponseHelper _responseHelper;

        public DeleteRequest(ResponseHelper responseHelper)
        {
            _responseHelper = responseHelper;
        }
        public void Process(HttpListenerContext context, List<string> userNames)
        {
            var rawUrl=context.Request.RawUrl;
            var name = rawUrl.Substring(1);

            if (name == HTTPApp.FirstUser)
            {
                context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                var message = "The power user cannot be deleted.";
                _responseHelper.SendResponse(message, context);
            }
            else if (userNames.Contains(name))
            {
                userNames.Remove(name);
            }
            else 
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                var message = "This username cannot be found.";
                _responseHelper.SendResponse(message, context);
            }
        }
    }
}
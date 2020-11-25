using System;
using System.Collections.Generic;
using System.Net;

namespace FrameworklessAppKata
{
    public class GetRequest:IRequest
    {
        private readonly ResponseHelper _responseHelper;

        public GetRequest(ResponseHelper responseHelper)
        {
            _responseHelper = responseHelper;
        }
        public void Process(HttpListenerContext context, List<string>userNames)
        {
            var rawUrl = context.Request.RawUrl;
            var message = GetResponseMessage(rawUrl, userNames);
            _responseHelper.SendResponse(message, context);
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
                message = $"Hi {placeholder} {DateTime.Now}";
            }

            return message;
        }

       
    }
}
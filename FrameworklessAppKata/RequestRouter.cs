using System.Collections.Generic;
using System.Net;

namespace FrameworklessAppKata
{
    public class RequestRouter
    {
        private readonly ResponseHelper _responseHelper;

        public RequestRouter()
        {
            _responseHelper = new ResponseHelper();
        }
        public IRequest Decide(HttpListenerContext context)
        {
            IRequest request = null;
            switch (context.Request.HttpMethod)
            {
                case "GET":
                    request = new GetRequest(_responseHelper);
                    break;
                case "POST":
                    request = new PostRequest(_responseHelper);
                    break;
                case "PUT":
                    request = new PutRequest();
                    break;
                case "DELETE":
                    request = new DeleteRequest();
                    break;
            }

            return request;
        }
    }
}
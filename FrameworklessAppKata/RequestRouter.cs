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
        public void Decide(HttpListenerContext context, List<string> userNames)
        {
            switch (context.Request.HttpMethod)
            {
                case "GET":
                    var getRequest = new GetRequest(_responseHelper);
                    getRequest.Run(context, userNames);
                    break;
                case "POST":
                    var postRequest = new PostRequest(_responseHelper);
                    postRequest.Run(context, userNames);
                    break;
                case "PUT":
                    var putRequest = new PutRequest();
                    putRequest.Run(context, userNames);
                    break;
                case "DELETE":
                    var deleteRequest = new DeleteRequest();
                    deleteRequest.Run(context, userNames);
                    break;
            }
        }
    }
}
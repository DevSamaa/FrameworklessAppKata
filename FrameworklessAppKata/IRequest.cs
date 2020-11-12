using System.Collections.Generic;
using System.Net;

namespace FrameworklessAppKata
{
    public interface IRequest
    {
        public void Process(HttpListenerContext context, List<string> userNames);
    }
}
using System.Collections.Generic;
using System.Net;

namespace FrameworklessAppKata
{
    public interface IRequest
    {
        public void Run(HttpListenerContext context, List<string> userNames);
    }
}
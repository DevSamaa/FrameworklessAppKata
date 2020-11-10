using System.Net;

namespace FrameworklessAppKata
{
    public class HttpListenerContextProxy: IHttpListenerContextProxy
    {
        private readonly HttpListenerContext _context;
        
        public HttpListenerContextProxy(HttpListenerContext context)
        {
            _context = context;
        }

        public HttpListenerRequest Request()
        {
            return _context.Request;
        }
        
        //add one more for response
        
    }
}
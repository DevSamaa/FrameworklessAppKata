using System.Net;

namespace FrameworklessAppKata
{
    public interface IHttpListenerContextProxy
    {
        public HttpListenerRequest Request();
        //don't need the word public
    }
}
using System.Threading.Tasks;

namespace FrameworklessAppKata
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpServer = new HTTPApp();
            await httpServer.Run("http://*:8080/");
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FrameworklessAppKata
{
    class Program
    {
        static void Main(string[] args)
        {
               var httpServer = new HTTPServer();
               var tuple = httpServer.Run();
               tuple.Item2.Wait();
        }
    }
}



//Next step: Get my code to be incorporate into a Docker image that has .NET
//Docker image with .NET
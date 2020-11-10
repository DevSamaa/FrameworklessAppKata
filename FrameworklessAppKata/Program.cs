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
               var httpServer = new HTTPApp();
               var tuple = httpServer.Run();
               tuple.Item2.Wait();
               
        }
    }
}
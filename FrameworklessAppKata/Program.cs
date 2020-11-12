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
        static async Task Main(string[] args)
        {
               var httpServer = new HTTPApp();
               await httpServer.Run("http://*:8080/");
        }
    }
}
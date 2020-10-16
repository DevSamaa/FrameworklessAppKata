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


//NExt step: Break the test, git push, see if the test in buildkite is still passing or not. Hopefully it should break!
// If it doesn't break we need to do something else!
//you shall pass
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

//TODO figure out how to modularize the methods, how to make your code more testable
//you probably have to run your whole program and write a test that sends GET and POST requests to localhost.

//Notes: the goal is to get Run() to start the cancelable task and to have another method that is able to cancel the task.
//look up cancellable tasks C#
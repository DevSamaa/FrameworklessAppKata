using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FrameworklessAppKata;
using Xunit;


namespace FrameworklessAppTests
{
    public class ProgramTests
    {

        [Fact]
        public async Task HttpServerShouldGetRequest()
        {
            //arrange
            var httpServer = new HTTPServer();
            
            //act
            Console.WriteLine($"main thread is starting a new thread {DateTime.Now.ToString("hh:MM:ss:fff")}");
            var tuple = httpServer.Run();
            
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:8080");
            Console.WriteLine($" main thread sent an http request {DateTime.Now.ToString("hh:MM:ss:fff")}");
            var getResponse = await httpClient.GetAsync("");

            Console.WriteLine($" main thread is going to send a cancel signal {DateTime.Now.ToString("hh:MM:ss:fff")}");
            tuple.Item1.Cancel();

            //assert
            Assert.Equal(HttpStatusCode.OK,getResponse.StatusCode);
        }
        
        
    }
}
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using FrameworklessAppKata;
using Xunit;


namespace FrameworklessAppTests
{
    public class ProgramTests
    {

        [Fact]
        public async void HTTPServerShouldGetRequest()
        {
            //arrange
            var httpServer = new HTTPServer();
            
            var tokenSource = new CancellationTokenSource();
            // CancellationToken ct = tokenSource.Token;
            
            //act
            httpServer.StartServer();
            Console.WriteLine($"main thread is starting a new thread {DateTime.Now.ToString("hh:MM:ss:fff")}");
            httpServer.Run(tokenSource);
            
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:8080");
            Console.WriteLine($" main thread sent an http request {DateTime.Now.ToString("hh:MM:ss:fff")}");
            var getResponse= await httpClient.GetAsync("");

            
            Console.WriteLine($" main thread is going to send a cancel signal {DateTime.Now.ToString("hh:MM:ss:fff")}");
            tokenSource.Cancel();

            //assert
            Assert.Equal(HttpStatusCode.OK,getResponse.StatusCode);

        }
    }
}
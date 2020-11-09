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
            var tuple = httpServer.Run();
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:8080");
            var postResponseTask1 =  httpClient.PostAsync("", new StringContent("samaa"));
            var postResponseTask2 =  httpClient.PostAsync("", new StringContent("sandy"));
            var postResponse1 = await postResponseTask1;
            var postResponse2 = await postResponseTask2; 

            Assert.Equal(HttpStatusCode.OK,postResponse1.StatusCode);
            Assert.Equal(HttpStatusCode.OK,postResponse2.StatusCode);


            var getResponse = await httpClient.GetAsync("");
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            var stringContainsSamaa = await getResponse.Content.ReadAsStringAsync();
            Assert.Contains("samaa", stringContainsSamaa);
            Assert.Contains("sandy", stringContainsSamaa);


            tuple.Item1.Cancel();

            //assert

        }
        
        
    }
}
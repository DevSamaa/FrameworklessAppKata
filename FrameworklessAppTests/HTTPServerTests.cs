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
        [Trait("Category","End to End")]
        public async Task HttpServerShouldPostAndGet()
        {
            //arrange
            var httpServer = new HTTPApp();
        
            //act & assert
            var tuple = httpServer.Run();
            var httpClient = new HttpClient();
        
            httpClient.BaseAddress = new Uri("http://localhost:8080");
            
            var postResponse1 =  await httpClient.PostAsync("", new StringContent("samaa"));
            var postResponse2 =  await httpClient.PostAsync("", new StringContent("sandy"));
            Assert.Equal(HttpStatusCode.OK,postResponse1.StatusCode);
            Assert.Equal(HttpStatusCode.OK,postResponse2.StatusCode);
        
            var getResponse = await httpClient.GetAsync("");
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            var returnedString = await getResponse.Content.ReadAsStringAsync();
            Assert.Contains("samaa", returnedString);
            Assert.Contains("sandy", returnedString);
        
            tuple.Item1.Cancel();
        }

        
        
        
    }
}
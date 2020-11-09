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
        public async Task HttpServerShouldPostAndGet()
        {
            //arrange
            var httpServer = new HTTPServer();
        
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

        //TODO figure out how to have more than 1 test in a file
        [Fact]
        public async Task PutRequestShouldChangeName()
        {
            var httpServer = new HTTPServer();
            var tuple = httpServer.Run();
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5050");
            
            var postResponse1 =  await httpClient.PostAsync("", new StringContent("samaa"));
            var putResponse = await httpClient.PutAsync("/samaa", new StringContent("newSamaa"));
            var getResponse = await httpClient.GetAsync("");
            var returnedString = await getResponse.Content.ReadAsStringAsync();
            Assert.Contains("newSamaa", returnedString);

            tuple.Item1.Cancel();

        }
        
        
    }
}
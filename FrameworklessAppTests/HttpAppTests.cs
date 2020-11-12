using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FrameworklessAppKata;
using Xunit;


namespace FrameworklessAppTests
{
    public class HttpAppTests
    {

        [Fact]
        [Trait("Category","End to End")]
        public async Task HttpServer_Should_PostAndGetRequests()
        {
            //arrange
            var httpApp = new HTTPApp();
        
            //act & assert
            var taskTerminator = httpApp.Run("http://*:8080/");
            var httpClient = new HttpClient();
        
            httpClient.BaseAddress = new Uri("http://localhost:8080/");
            
            var postResponse1 =  await httpClient.PostAsync("", new StringContent("samaa"));
            var postResponse2 =  await httpClient.PostAsync("", new StringContent("sandy"));
            Assert.Equal(HttpStatusCode.OK,postResponse1.StatusCode);
            Assert.Equal(HttpStatusCode.OK,postResponse2.StatusCode);
        
            var getResponse = await httpClient.GetAsync("");
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            var returnedString = await getResponse.Content.ReadAsStringAsync();
            Assert.Contains("samaa", returnedString);
            Assert.Contains("sandy", returnedString);
        
            taskTerminator.CancellationTokenSource.Cancel();
            // httpClient.GetAsync("");
            // await taskTerminator.Task;
            // Console.WriteLine("test is done!");
        }

        [Fact]
        [Trait("Category","End to End")]
        public async Task HttpServer_Should_PutNewInfoIntoUserNamesList()
        {
            //arrange
            var httpApp = new HTTPApp();
        
            //act & assert
            var tuple = httpApp.Run("http://*:8081/");
            var httpClient = new HttpClient();
        
            httpClient.BaseAddress = new Uri("http://localhost:8081/");
            
            var postResponse1 =  await httpClient.PostAsync("", new StringContent("samaa"));
            Assert.Equal(HttpStatusCode.OK,postResponse1.StatusCode);

            var putResponse = await httpClient.PutAsync("/samaa", new StringContent("newSamaa"));
            var getResponse = await httpClient.GetAsync("");
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            
            var returnedString = await getResponse.Content.ReadAsStringAsync();
            Assert.Contains("newSamaa", returnedString);
        
            // tuple.Item1.Cancel();
            tuple.CancellationTokenSource.Cancel();
        }
        
        [Fact]
        [Trait("Category","End to End")]
        public async Task HttpServer_Should_DeleteRequests()
        {
            //arrange
            var httpApp = new HTTPApp();
        
            //act & assert
            var tuple = httpApp.Run("http://*:8082/");
            var httpClient = new HttpClient();
        
            httpClient.BaseAddress = new Uri("http://localhost:8082/");
            
            //post new name
            var postResponse =  await httpClient.PostAsync("", new StringContent("samaa"));
            Assert.Equal(HttpStatusCode.OK,postResponse.StatusCode);
            
            // check that new name is in list
            var getResponse = await httpClient.GetAsync("");
            var returnedString = await getResponse.Content.ReadAsStringAsync();    
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.Contains("samaa", returnedString);

            //delete new name from list
            var deleteResponse = await httpClient.DeleteAsync("/samaa");
           Assert.Equal(HttpStatusCode.OK,deleteResponse.StatusCode );
            
           // check that new name is NOT in list
           var getResponseAfterDeleting = await httpClient.GetAsync("");
           var returnedStringAfterDeleting = await getResponseAfterDeleting.Content.ReadAsStringAsync();    
           Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
           Assert.DoesNotContain("samaa", returnedStringAfterDeleting);
        
            // tuple.Item1.Cancel();
            tuple.CancellationTokenSource.Cancel();
        }

        
    }
}
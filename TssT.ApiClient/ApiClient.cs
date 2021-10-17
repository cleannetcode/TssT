using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TssT.ApiClient
{
    public class ApiClient
    {
        public readonly TestsApiClient Tests;

        public ApiClient(string apiUrl)
        {
            var httpClient = new HttpClient();
            
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applications/json"));
            httpClient.BaseAddress = new Uri(apiUrl);

            Tests = new TestsApiClient("Test", httpClient);
        }
        
    }
}
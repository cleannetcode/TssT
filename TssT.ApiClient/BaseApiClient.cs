using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;

namespace TssT.ApiClient
{
    public abstract class BaseApiClient
    {
        private readonly HttpClient _httpClient;
    
        protected BaseApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        protected async Task<TOutputContract> CallApiAsync<TOutputContract>(
            string requestUri,
            HttpMethod httpMethod,
            object content = null,
            string authToken = "",
            CancellationToken cancellationToken = default)
        {
            var jsonContent = JsonConvert.SerializeObject(content);
            
            var requestMessage = new HttpRequestMessage(httpMethod, requestUri)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };
            
            if (!string.IsNullOrWhiteSpace(authToken))
                requestMessage.Headers.Add("Authorization", $"Bearer {authToken}");

            var response = await _httpClient.SendAsync(requestMessage, cancellationToken);
            var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<TOutputContract>(responseJson);
            
            throw new HttpResponseException(new HttpResponseMessage()
            {
                RequestMessage = requestMessage,
                Content = new StringContent(responseJson),
                StatusCode = response.StatusCode 
            });
        }
    }
}
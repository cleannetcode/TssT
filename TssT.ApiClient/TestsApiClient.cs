using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TssT.Api.Contracts;
using TssT.API.Contracts;
using TssT.Core.Models;

namespace TssT.ApiClient
{
    public class TestsApiClient: BaseApiClient
    {
        private readonly string _endpoint;
        public TestsApiClient(string endpoint, HttpClient httpClient):base(httpClient)
        {
            _endpoint = endpoint;
        }

        public async Task<BaseCreateResponse> CreateAsync(NewTest newTest, CancellationToken cancellationToken = default)
        {
            return await CallApiAsync<BaseCreateResponse>(
                $"{_endpoint}",
                HttpMethod.Post,
                newTest,
                string.Empty,
                cancellationToken
            );
        }
        
        public async Task<BaseCollectionResponse<Test>> GetAsync(CancellationToken cancellationToken = default)
        {
            return await CallApiAsync<BaseCollectionResponse<Test>>(
                $"{_endpoint}",
                HttpMethod.Get,
                cancellationToken
            );
        }

        public async Task<Test> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return await CallApiAsync<Test>(
                $"{_endpoint}/{id}",
                HttpMethod.Get,
                cancellationToken
            );
        }
    }
}
using EditTestClient.Api.Responses;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace EditTestClient.Api.Results
{
    public interface IResultApi
    {
        public Task<(HttpStatusCode statusCode, List<ResultResponse> results)> GetResults(int testId, string token);

        public Task<HttpStatusCode> AddResult(int testId, int result, string token);
    }
}

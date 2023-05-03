using EditTestClient.Api.Responses;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace EditTestClient.Api
{
    public interface IResultApi
    {
        public Task<KeyValuePair<HttpStatusCode, List<ResultResponse>>> GetResults(int testId, string token);

        public Task<HttpStatusCode> AddResult(int testId, int result, string token);
    }
}

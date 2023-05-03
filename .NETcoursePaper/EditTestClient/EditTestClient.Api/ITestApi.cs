using EditTestClient.Api.Requests;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EditTestClient.Api.Responses;

namespace EditTestClient.Api
{
    public interface ITestApi
    {
        public Task<KeyValuePair<HttpStatusCode, List<TestResponse>>> GetTests(string token);

        public Task<KeyValuePair<HttpStatusCode, TestResponse>> GetConcreteTest(int id, string token);

        public Task<HttpStatusCode> DeleteTest(int testId, string token);

        public Task<HttpStatusCode> UpdateTest(TestRequest testRequest, int testId, string token);

        public Task<HttpStatusCode> AddTest(TestRequest test, string token);
    }
}

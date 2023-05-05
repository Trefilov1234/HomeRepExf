using EditTestClient.Api.Requests;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EditTestClient.Api.Responses;

namespace EditTestClient.Api.Tests
{
    public interface ITestApi
    {
        public Task<(HttpStatusCode StatusCode, List<TestResponse> Tests)> GetTests(string token);

        public Task<(HttpStatusCode StatusCode, TestResponse Test)> GetTest(int id, string token);

        public Task<HttpStatusCode> DeleteTest(int testId, string token);

        public Task<HttpStatusCode> UpdateTest(TestRequest testRequest, int testId, string token);

        public Task<HttpStatusCode> AddTest(TestRequest test, string token);
    }
}

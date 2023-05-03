using EditTestClient.Api.Requests;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using EditTestClient.Api.Responses;
using EditTestClient.Api.Helpers;

namespace EditTestClient.Api
{
    public class TestApi : ApiBase, ITestApi
    {
        public TestApi(string baseUri) : base(baseUri) { }

        public async Task<HttpStatusCode> AddTest(TestRequest test,string token)
        {
            var response = await SendAsync(HttpMethod.Post, "/tests", token,body:test);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteTest(int testId,string token)
        {
            var response = await SendAsync(HttpMethod.Delete, $"/tests/{testId}", token);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> UpdateTest(TestRequest testRequest,int testId,string token)
        {
            var response = await SendAsync(HttpMethod.Put, $"/tests/{testId}", token, body: testRequest);
            return response.StatusCode;
        }

        public async Task<KeyValuePair<HttpStatusCode, List<TestResponse>>> GetTests(string token)
        {
            var response = await SendAsync(HttpMethod.Get, "/tests", token);   
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var testResp = JsonSerializeHelper.Deserialize<List<TestResponse>>(responseBody);
            return new KeyValuePair<HttpStatusCode, List<TestResponse>>(response.StatusCode, testResp);
        }

        public async Task<KeyValuePair<HttpStatusCode, TestResponse>> GetConcreteTest(int id,string token)
        {
            var response = await SendAsync(HttpMethod.Get, $"/tests/{id}", token);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var testResp = JsonSerializeHelper.Deserialize<TestResponse>(responseBody);
            return new KeyValuePair<HttpStatusCode, TestResponse>(response.StatusCode, testResp);
        } 
    }
}

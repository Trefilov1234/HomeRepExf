using EditTestClient.Api.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EditTestClient.Api.Responses;

namespace EditTestClient.Api
{
    public class TestApi : ApiBase, ITestApi
    {
        public TestApi(string baseUri) : base(baseUri) { }

        public Task<HttpResponseMessage> CreateUser(UserRequest user)
        {
            return RegisterUser(HttpMethod.Post, "/users", body: user);
        }
        public Task<HttpStatusCode> LoginUser(UserRequest user)
        {
            return VerifyUser(HttpMethod.Post, "/login", body: user);
        }
        public Task<HttpStatusCode> AddTest(TestRequest test)
        {
            return AddTest(HttpMethod.Post, "/tests", body: test);
        }
        public Task<HttpStatusCode> AddQuestion(QuestionRequest question,int testId)
        {
            return AddQuestion(HttpMethod.Post, $"/tests/{testId}/questions", body: question);
        }
        public Task<HttpStatusCode> UpdateQuestion(QuestionRequest question,int testId,int questionId)
        {
            return UpdateQuestion(HttpMethod.Put, $"/tests/{testId}/questions/{questionId}", body: question);
        }
        public Task<QuestionResponse> GetConcreteQuestion(int testId, int questionId)
        {
            return GetConcreteQuestion(HttpMethod.Get, $"/tests/{testId}/questions/{questionId}");
        }
        public Task<HttpStatusCode> DeleteQuestion(int testId,int questionId)
        {
            return DeleteQuestion(HttpMethod.Delete, $"/tests/{testId}/questions/{questionId}");
        }
        public Task<HttpStatusCode> DeleteTest(int testId)
        {
            return DeleteQuestion(HttpMethod.Delete, $"/tests/{testId}");
        }
        public Task<HttpStatusCode> UpdateTest(TestRequest testRequest,int testId)
        {
            return DeleteQuestion(HttpMethod.Put, $"/tests/{testId}", body: testRequest);
        }
        public Task<KeyValuePair<HttpStatusCode, List<TestResponse>>> GetTests()
        {
            return GetTests(HttpMethod.Get, "/tests");
        }
        public Task<KeyValuePair<HttpStatusCode, TestResponse>> GetConcreteTest(int id)
        {
            return GetConcreteTest(HttpMethod.Get, $"/tests/{id}");
        }
        public Task<KeyValuePair<HttpStatusCode, List<QuestionResponse>>> GetQuestions(int testId)
        {
            return GetQuestions(HttpMethod.Get, $"/tests/{testId}/questions");
        }
    }
}

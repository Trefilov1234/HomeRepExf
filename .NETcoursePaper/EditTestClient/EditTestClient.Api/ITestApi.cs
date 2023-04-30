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
    public interface ITestApi
    {
        public Task<HttpResponseMessage> CreateUser(UserRequest user);
        public Task<HttpStatusCode> LoginUser(UserRequest user);
        public Task<HttpStatusCode> AddTest(TestRequest test);
        public Task<HttpStatusCode> AddQuestion(QuestionRequest question,int testId);
        public Task<HttpStatusCode> UpdateQuestion(QuestionRequest question, int testId, int questionId);
        public Task<KeyValuePair<HttpStatusCode, List<TestResponse>>> GetTests();
        public Task<KeyValuePair<HttpStatusCode, TestResponse>> GetConcreteTest(int id);
        public Task<KeyValuePair<HttpStatusCode, List<QuestionResponse>>> GetQuestions(int testId);
        public Task<QuestionResponse> GetConcreteQuestion(int testId, int questionId);
        public Task<HttpStatusCode> DeleteQuestion(int testId, int questionId);
        public Task<HttpStatusCode> DeleteTest(int testId);
        public Task<HttpStatusCode> UpdateTest(TestRequest testRequest, int testId);
    }
}

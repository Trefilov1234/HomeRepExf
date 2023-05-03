using EditTestClient.Api.Requests;
using EditTestClient.Api.Responses;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace EditTestClient.Api
{
    public interface IQuestionApi
    {
        public Task<HttpStatusCode> AddQuestion(QuestionRequest question, int testId, string token);

        public Task<HttpStatusCode> UpdateQuestion(QuestionRequest question, int testId, int questionId, string token);

        public Task<KeyValuePair<HttpStatusCode, List<QuestionResponse>>> GetQuestions(int testId, string token);

        public Task<QuestionResponse> GetConcreteQuestion(int testId, int questionId, string token);

        public Task<HttpStatusCode> DeleteQuestion(int testId, int questionId, string token);
    }
}

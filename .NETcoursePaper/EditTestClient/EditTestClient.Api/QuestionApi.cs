using EditTestClient.Api.Requests;
using EditTestClient.Api.Responses;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using EditTestClient.Api.Helpers;

namespace EditTestClient.Api
{
    public class QuestionApi:ApiBase,IQuestionApi
    {
        public QuestionApi(string baseUri) : base(baseUri) { }

        public async Task<HttpStatusCode> AddQuestion(QuestionRequest question, int testId,string token)
        {
            var response = await SendAsync(HttpMethod.Post, $"/tests/{testId}/questions", token, body: question);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> UpdateQuestion(QuestionRequest question, int testId, int questionId,string token)
        {
            var response = await SendAsync(HttpMethod.Put, $"/tests/{testId}/questions/{questionId}", token, body: question);
            return response.StatusCode;
        }

        public async Task<QuestionResponse> GetConcreteQuestion(int testId, int questionId,string token)
        {
            var response = await SendAsync(HttpMethod.Get, $"/tests/{testId}/questions/{questionId}", token);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var questionResp = JsonSerializeHelper.Deserialize<QuestionResponse>(responseBody);
            return questionResp;
        }

        public async Task<HttpStatusCode> DeleteQuestion(int testId, int questionId,string token)
        {
            var response = await SendAsync(HttpMethod.Delete, $"/tests/{testId}/questions/{questionId}", token);
            return response.StatusCode;
        }

        public async Task<KeyValuePair<HttpStatusCode, List<QuestionResponse>>> GetQuestions(int testId,string token)
        {
            var response = await SendAsync(HttpMethod.Get, $"/tests/{testId}/questions", token);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var questionResp = JsonSerializeHelper.Deserialize<List<QuestionResponse>>(responseBody);
            return new KeyValuePair<HttpStatusCode, List<QuestionResponse>>(response.StatusCode, questionResp);
        }
    }
}

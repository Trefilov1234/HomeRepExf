using EditTestClient.Api.Helpers;
using EditTestClient.Api.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EditTestClient.Api.Requests;

namespace EditTestClient.Api
{
    public abstract class ApiBase : IDisposable
    {
        private readonly string _baseUri;
        private readonly HttpClient _httpClient;
        private string JWToken;
        protected ApiBase(string baseUri)
        {
            _baseUri = baseUri;
            _httpClient = new HttpClient();
        }

        protected async Task<HttpResponseMessage> SendAsync(
            HttpMethod method,
            string path,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var uri = RequestUriHelper.GetUriWithQueryString(_baseUri + path, parameters);

            var request = new HttpRequestMessage(method, uri);
            if (body != null)
                request.Content = new StringContent(JsonSerializeHelper.Serialize(body));
            if(JWToken != null)
                request.Headers.Add("JWT", $"{JWToken}");
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);


            return response;
        }
        protected async Task<HttpStatusCode> VerifyUser(HttpMethod method,
            string path,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var response = await SendAsync(method, path, parameters, body).ConfigureAwait(false);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var userResp = JsonSerializeHelper.Deserialize<UserResponse>(responseBody);
            JWToken = userResp.JWT;
            return response.StatusCode;
        }
        protected async Task<HttpResponseMessage> RegisterUser(HttpMethod method,
            string path,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var response = await SendAsync(method, path, parameters, body).ConfigureAwait(false);
            return response;
        }
        protected async Task<HttpStatusCode> AddTest(HttpMethod method,
            string path,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var response = await SendAsync(method, path, parameters, body).ConfigureAwait(false);
            return response.StatusCode;
        }
        protected async Task<HttpStatusCode> AddQuestion(HttpMethod method,
            string path,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var response = await SendAsync(method, path, parameters, body).ConfigureAwait(false);
            return response.StatusCode;
        }
        protected async Task<QuestionResponse> GetConcreteQuestion(HttpMethod method,
            string path,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var response = await SendAsync(method, path, parameters, body).ConfigureAwait(false);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var questResp = JsonSerializeHelper.Deserialize<QuestionResponse>(responseBody);
            return questResp;
        }
        protected async Task<HttpStatusCode> UpdateQuestion(HttpMethod method,
            string path,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var response = await SendAsync(method, path, parameters, body).ConfigureAwait(false);
            return response.StatusCode;
        }
        protected async Task<HttpStatusCode> DeleteQuestion(HttpMethod method,
            string path,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var response = await SendAsync(method, path, parameters, body).ConfigureAwait(false);
            return response.StatusCode;
        }
        protected async Task<HttpStatusCode> DeleteTest(HttpMethod method,
            string path,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var response = await SendAsync(method, path, parameters, body).ConfigureAwait(false);
            return response.StatusCode;
        }
        protected async Task<HttpStatusCode> UpdateTest(HttpMethod method,
            string path,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var response = await SendAsync(method, path, parameters, body).ConfigureAwait(false);
            return response.StatusCode;
        }
        protected async Task<KeyValuePair<HttpStatusCode,List<TestResponse>>> GetTests(HttpMethod method,
            string path,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var response = await SendAsync(method, path, parameters, body).ConfigureAwait(false);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var testResp = JsonSerializeHelper.Deserialize<List<TestResponse>>(responseBody);
            
            return new KeyValuePair<HttpStatusCode, List<TestResponse>>(response.StatusCode, testResp);
        }
        protected async Task<KeyValuePair<HttpStatusCode, TestResponse>> GetConcreteTest(HttpMethod method,
            string path,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var response = await SendAsync(method, path, parameters, body).ConfigureAwait(false);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var testResp = JsonSerializeHelper.Deserialize<TestResponse>(responseBody);

            return new KeyValuePair<HttpStatusCode, TestResponse>(response.StatusCode, testResp);
        }
        protected async Task<KeyValuePair<HttpStatusCode, List<QuestionResponse>>> GetQuestions(HttpMethod method,
            string path,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var response = await SendAsync(method, path, parameters, body).ConfigureAwait(false);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var questResp = JsonSerializeHelper.Deserialize<List<QuestionResponse>>(responseBody);

            return new KeyValuePair<HttpStatusCode, List<QuestionResponse>>(response.StatusCode, questResp);
        }
        //protected async Task<TResult> SendAsync<TResult>(
        //    HttpMethod method,
        //    string path,
        //    Dictionary<string, string> parameters = null,
        //    object body = null)
        //{
        //    var response = await SendAsync(method, path, parameters, body).ConfigureAwait(false);
        //    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        //    return JsonSerializeHelper.Deserialize<TResult>(responseBody);
        //}

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}

using EditTestClient.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EditTestClient.Api
{
    public abstract class ApiBase : IDisposable
    {
        private readonly string _baseUri;
        private readonly HttpClient _httpClient;
        private const string AuthorizationHeaderKey = "JWT";
        protected ApiBase(string baseUri)
        {
            _baseUri = baseUri;
            _httpClient = new HttpClient();
        }

        protected async Task<HttpResponseMessage> SendAsync(
            HttpMethod method,
            string path,
            string token,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var uri = RequestUriHelper.GetUriWithQueryString(_baseUri + path, parameters);

            var request = new HttpRequestMessage(method, uri);
            if (!string.IsNullOrEmpty(token))
                request.Headers.Add(AuthorizationHeaderKey, $"{token}");
            if (body != null)
                request.Content = new StringContent(JsonSerializeHelper.Serialize(body));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            return response;
        }

        protected async Task<TResult> SendAsync<TResult>(
            HttpMethod method,
            string path,
            string token,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var response = await SendAsync(method, path,token, parameters, body).ConfigureAwait(false);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializeHelper.Deserialize<TResult>(responseBody);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}

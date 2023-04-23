using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestClient.Api.Helpers;

namespace TestClient.Api
{
    public abstract class ApiBase : IDisposable
    {
        private readonly string _baseUri;
        private readonly HttpClient _httpClient;

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

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            return response;
        }

        protected async Task<TResult> SendAsync<TResult>(
            HttpMethod method,
            string path,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var response = await SendAsync(method, path, parameters, body).ConfigureAwait(false);

            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializeHelper.Deserialize<TResult>(responseBody);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}

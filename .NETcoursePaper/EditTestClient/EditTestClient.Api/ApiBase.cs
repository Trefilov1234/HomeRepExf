using EditTestClient.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace EditTestClient.Api
{
    public abstract class ApiBase : IDisposable
    {
        private readonly string _baseUri;
        private readonly HttpClient _httpClient;
        private const string AuthorizationHeaderKey = "Authorization";
        protected ApiBase(string baseUri)
        {
            _baseUri = baseUri;
            _httpClient = new HttpClient();
        }

        protected async Task<HttpStatusCode> SendAsync(
            HttpMethod method,
            string path,
            string token,
            Dictionary<string, string> parameters = null,
            object body = null)
		{
			var response = await SendInternalAsync(method, path, token, parameters, body).ConfigureAwait(false);
			return response.StatusCode;
        }

        protected async Task<(HttpStatusCode StatusCode, TResult Value)> SendAsync<TResult>(
            HttpMethod method,
            string path,
            string token,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var response = await SendInternalAsync(method, path, token, parameters, body).ConfigureAwait(false);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return (response.StatusCode, JsonSerializeHelper.Deserialize<TResult>(responseBody));
		}

		private async Task<HttpResponseMessage> SendInternalAsync(
			HttpMethod method,
			string path,
			string token,
			Dictionary<string, string> parameters,
			object body)
		{
			var uri = RequestUriHelper.GetUriWithQueryString(_baseUri + path, parameters);

			var request = new HttpRequestMessage(method, uri);
			if (!string.IsNullOrEmpty(token))
				request.Headers.Add(AuthorizationHeaderKey, $"Bearer {token}");
			if (body != null)
				request.Content = new StringContent(JsonSerializeHelper.Serialize(body));

			var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

			return response;
		}

		public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}

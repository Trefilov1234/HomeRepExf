using EditTestClient.Api.Requests;
using EditTestClient.Api.Responses;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using EditTestClient.Api.Helpers;

namespace EditTestClient.Api.Users
{
    public class UserApi : ApiBase, IUserApi
    {
        public UserApi(string baseUri) : base(baseUri) { }

        public Task<HttpResponseMessage> CreateUser(UserRequest user)
        {
            return SendAsync(HttpMethod.Post, "/users", null, body: user);
        }

        public async Task<(HttpStatusCode statusCode, UserResponse user)> LoginUser(UserRequest user)
        {
            var response = await SendAsync(HttpMethod.Post, "/login", null, body: user);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var userResp = JsonSerializeHelper.Deserialize<UserResponse>(responseBody);
            return (response.StatusCode, userResp);
        }
    }
}

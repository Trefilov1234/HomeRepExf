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

        public Task<HttpStatusCode> CreateUser(UserRequest user)
        {
            return SendAsync(HttpMethod.Post, "/users", null, body: user);
        }

        public async Task<(HttpStatusCode StatusCode, UserResponse User)> LoginUser(UserRequest user)
        {
			// todo(v): сделать так как тут для остальных методов, где есть десериализация
			return await SendAsync<UserResponse>(HttpMethod.Post, "/login", null, body: user);
        }
    }
}

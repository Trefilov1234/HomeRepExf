using EditTestClient.Api.Requests;
using EditTestClient.Api.Responses;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace EditTestClient.Api.Users
{
    public interface IUserApi
    {
        public Task<HttpStatusCode> CreateUser(UserRequest user);

        public Task<(HttpStatusCode StatusCode, UserResponse User)> LoginUser(UserRequest user);
    }
}

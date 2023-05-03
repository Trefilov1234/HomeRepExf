using EditTestClient.Api.Requests;
using EditTestClient.Api.Responses;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace EditTestClient.Api
{
    public interface IUserApi
    {
        public Task<HttpResponseMessage> CreateUser(UserRequest user);

        public Task<KeyValuePair<HttpStatusCode, UserResponse>> LoginUser(UserRequest user);
    }
}

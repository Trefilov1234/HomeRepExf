using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestClient.Api.Requests;
using TestClient.Api.Responses;

namespace TestClient.Api
{
    public class TestApi: ApiBase,ITestApi
    {
        public TestApi(string baseUri):base(baseUri) { }
        
        public Task<UserResponse> CreateUser(UserRequest user)
        {
            return SendAsync<UserResponse>(HttpMethod.Post, "/users", body: user);
        }
    }
}

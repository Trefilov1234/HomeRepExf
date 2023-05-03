using EditTestClient.Api.Responses;
using System.Collections.Generic;

namespace EditTestClient.Services
{
    public class TestService:ITestService
    {
        public List<TestResponse> TestBank { get;set; }
    }
}

using TestServer.Domain.Entities;
using TestServer.Requests;
using TestServer.Responses;

namespace TestServer.Extensions
{
    public static class TestExtensions
    {
        public static TestResponse ToResponse(this Test test)
        {
            return new TestResponse
            {
                Name = test.Name,
                AttemptsCount = test.AttemptsCount
            };
        }

        public static Test ToEntity(this TestRequest testRequest)
        {
            return new Test
            {
                Name= testRequest.Name,
                AttemptsCount= testRequest.AttemptsCount
            };

        }
    }
}

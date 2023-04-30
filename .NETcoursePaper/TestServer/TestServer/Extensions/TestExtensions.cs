using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServer.Common.Extensions;
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

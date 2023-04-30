using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestServer.Helpers;
using TestServer.Requests;
using TestServer.Services;
using TestServer.Extensions;
using TestServer.Common.Extensions;

namespace TestServer.Commands
{
    public class GetTestsCommand:ICommand
    {
        public string Path => @"/tests";
        public HttpMethod Method => HttpMethod.Get;

        private readonly ITestService _testService;

        public GetTestsCommand(ITestService testService)
        {
            _testService = testService;
        }
        public async Task HandleRequestAsync(HttpListenerContext context)
        {
            var tokenReq = context.Request.Headers.Get("JWT");
            var jwtData = JWT.ValidateToken(tokenReq);
            if (!jwtData.Key)
            {
                await context.WriteResponseAsync(401).ConfigureAwait(false);
                return;
            }

            if (jwtData.Value[0].Equals("teacher"))
            {
                var isSuccessTeacher = await _testService.GetTests(jwtData.Value[1]);
                List<TestResponse> responses=new();
                foreach (var el in isSuccessTeacher)
                {
                    responses.Add(new TestResponse() { Name = el.Name, AttemptsCount = el.AttemptsCount, CreatedBy = el.User.Login,Id=el.Id });
                }
                
                if (isSuccessTeacher != null)
                {
                    await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(responses)).ConfigureAwait(false);
                }
                else
                {
                    await context.WriteResponseAsync(409).ConfigureAwait(false);
                }
                return;
            }
            else
            {
                var isSuccess = await _testService.GetTests();
                if (isSuccess != null)
                {
                    await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(isSuccess)).ConfigureAwait(false);
                }
                else
                {
                    await context.WriteResponseAsync(409).ConfigureAwait(false);
                }
            }
        }
    }
}

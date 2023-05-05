using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using TestServer.Common.Extensions;
using TestServer.Helpers;
using TestServer.Requests;
using TestServer.Services.Tests;
using TestServer.Extensions;
using System.Text.RegularExpressions;

namespace TestServer.Commands
{
    public class AddTestCommand : ICommand
    {
        public string Path => @"/tests";
        public HttpMethod Method => HttpMethod.Post;

        private readonly ITestService _testService;

        public AddTestCommand(ITestService testService)
        {
            _testService = testService;
        }
        public async Task HandleRequestAsync(HttpListenerContext context, Match path)
        {
            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<TestRequest>(requestBody, out var testRequest))
            {
                await context.WriteResponseAsync(400, "Invalid request body content").ConfigureAwait(false);
                return;
            }
            var tokenReq = context.Request.Headers.Get("Authorization");
            var jwtData = JWT.ValidateToken(tokenReq);
            if (jwtData.IsFaulted)
            {
                await context.WriteResponseAsync(401).ConfigureAwait(false);
                return;
            }
            if (!jwtData.UserRole.Equals(UserRoles.Teacher))
            {
                await context.WriteResponseAsync(403).ConfigureAwait(false);
                return;
            }
            var test = testRequest.ToEntity();
			// todo(v): проверить наличие теста
			var isSuccess = await _testService.AddTest(test, jwtData.Login);
            if (!isSuccess)
            {
                await context.WriteResponseAsync(409).ConfigureAwait(false);
                return;
            }
            await context.WriteResponseAsync(200).ConfigureAwait(false);
        }
    }
}

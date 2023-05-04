using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using TestServer.Common.Extensions;
using TestServer.Helpers;
using TestServer.Services.Tests;
using TestServer.Extensions;
using TestServer.Services.DTO;
using System.Text.RegularExpressions;

namespace TestServer.Commands
{
    public class UpdateTestCommand : ICommand
    {
        private const string IdKey = "Id";
        public string Path => @$"/tests/(?<{IdKey}>\d+)";
        public HttpMethod Method => HttpMethod.Put;

        private readonly ITestService _testService;

        public UpdateTestCommand(ITestService testService)
        {
            _testService = testService;
        }
        public async Task HandleRequestAsync(HttpListenerContext context, Match path)
        {
            var id = path.GetIntGroup(IdKey);
            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<TestResponseDTO>(requestBody, out var questionRequest))
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
            var isSuccess = await _testService.UpdateTestById(questionRequest, id);
            if (!isSuccess)
            {
                await context.WriteResponseAsync(409).ConfigureAwait(false);
                return;
            }
            await context.WriteResponseAsync(200).ConfigureAwait(false);
        }
    }
}

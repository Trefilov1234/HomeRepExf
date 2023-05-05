using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using TestServer.Common.Extensions;
using TestServer.Helpers;
using TestServer.Services.Tests;
using TestServer.Extensions;
using System.Text.RegularExpressions;

namespace TestServer.Commands
{
    public class GetTestCommand : ICommand
    {
        private const string IdKey = "Id";
        public string Path => @$"/tests/(?<{IdKey}>\d+)";
        public HttpMethod Method => HttpMethod.Get;

        private readonly ITestService _testService;

        public GetTestCommand(ITestService testService)
        {
            _testService = testService;
        }
        public async Task HandleRequestAsync(HttpListenerContext context, Match path)
        {
            var id = path.GetIntGroup(IdKey);
            var tokenReq = context.Request.Headers.Get("Authorization");
            var jwtData = JWT.ValidateToken(tokenReq);
            if (jwtData.IsFaulted)
            {
                await context.WriteResponseAsync(401).ConfigureAwait(false);
                return;
            }
            var isSuccess = await _testService.GetTestById(id);
            if (isSuccess == null)
            {
                await context.WriteResponseAsync(409).ConfigureAwait(false);
                return;
            }
            await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(isSuccess)).ConfigureAwait(false);
        }
    }
}

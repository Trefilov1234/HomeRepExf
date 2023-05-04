using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using TestServer.Common.Extensions;
using TestServer.Helpers;
using TestServer.Services.Results;
using TestServer.Extensions;
using System.Text.RegularExpressions;

namespace TestServer.Commands
{
    public class GetResultsCommand : ICommand
    {
        private const string TestIdKey = "TestId";
        public string Path => @$"/tests/(?<{TestIdKey}>\d+)/results";

        public HttpMethod Method => HttpMethod.Get;

        private readonly IResultService _resultService;

        public GetResultsCommand(IResultService resultService)
        {
            _resultService = resultService;
        }
        public async Task HandleRequestAsync(HttpListenerContext context, Match path)
        {
            var testId = path.GetIntGroup(TestIdKey);
            var tokenReq = context.Request.Headers.Get("Authorization");
            var jwtData = JWT.ValidateToken(tokenReq);
            if (jwtData.IsFaulted)
            {
                await context.WriteResponseAsync(401).ConfigureAwait(false);
                return;
            }
            var isSuccess = await _resultService.GetResults(testId, jwtData.Login);
            if (isSuccess == null)
            {
                await context.WriteResponseAsync(409).ConfigureAwait(false);
                return;
            }
            await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(isSuccess)).ConfigureAwait(false);
        }
    }
}

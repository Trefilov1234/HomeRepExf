using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestServer.Common.Extensions;
using TestServer.Helpers;
using TestServer.Services;
using System.Text.RegularExpressions;
using TestServer.Extensions;

namespace TestServer.Commands
{
    public class GetConcreteTestCommand: ICommand
    {
        private const string IdKey = "Id";
        public string Path => @$"/tests/(?<{IdKey}>\d+)";
        public HttpMethod Method => HttpMethod.Get;

        private readonly ITestService _testService;

        public GetConcreteTestCommand(ITestService testService)
        {
            _testService = testService;
        }
        public async Task HandleRequestAsync(HttpListenerContext context)
        {
            var match = Regex.Match(context.Request.Url.AbsolutePath, Path, RegexOptions.IgnoreCase);
            var id = int.Parse(match.Groups[IdKey].Value);
            var tokenReq = context.Request.Headers.Get("JWT");
            var jwtData = JWT.ValidateToken(tokenReq);
            if (!jwtData.Key)
            {
                await context.WriteResponseAsync(401).ConfigureAwait(false);
                return;
            }

            var isSuccess = await _testService.GetTestById(id);
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

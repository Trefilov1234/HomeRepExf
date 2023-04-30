using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestServer.Common.Extensions;
using TestServer.Helpers;
using TestServer.Services;
using TestServer.Extensions;
using TestServer.Requests;

namespace TestServer.Commands
{
    public class UpdateTestCommand:ICommand
    {
        private const string IdKey = "Id";
        public string Path => @$"/tests/(?<{IdKey}>\d+)";
        public HttpMethod Method => HttpMethod.Put;

        private readonly ITestService _testService;

        public UpdateTestCommand(ITestService testService)
        {
            _testService = testService;
        }
        public async Task HandleRequestAsync(HttpListenerContext context)
        {
            var match = Regex.Match(context.Request.Url.AbsolutePath, Path, RegexOptions.IgnoreCase);
            var id = int.Parse(match.Groups[IdKey].Value);
            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<TestResponse>(requestBody, out var questionRequest))
            {
                await context.WriteResponseAsync(400, "Invalid request body content").ConfigureAwait(false);
                return;
            }
            var tokenReq = context.Request.Headers.Get("JWT");
            var jwtData = JWT.ValidateToken(tokenReq);
            if (!jwtData.Key)
            {
                await context.WriteResponseAsync(401).ConfigureAwait(false);
                return;
            }
            if (!jwtData.Value[0].Equals("teacher"))
            {
                await context.WriteResponseAsync(403).ConfigureAwait(false);
                return;
            }
            var isSuccess = await _testService.UpdateTestById(questionRequest,id);
            if (isSuccess)
            {
                await context.WriteResponseAsync(200).ConfigureAwait(false);
            }
            else
            {
                await context.WriteResponseAsync(409).ConfigureAwait(false);
            }

        }
    }
}

using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using TestServer.Common.Extensions;
using TestServer.Helpers;
using TestServer.Services;
using TestServer.Extensions;

namespace TestServer.Commands
{
    public class AddResultCommand:ICommand
    {
        private const string testId = "testId";
        public string Path => @$"/tests/(?<{testId}>\d+)/results";
        public HttpMethod Method => HttpMethod.Post;

        private readonly IResultService _resultService;

        public AddResultCommand(IResultService resultService)
        {
            _resultService = resultService;
        }
        public async Task HandleRequestAsync(HttpListenerContext context)
        {
            var id = Path.GetIntGroup(context, testId); 
            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<int>(requestBody, out var result))
            {
                await context.WriteResponseAsync(400, "Invalid request body content").ConfigureAwait(false);
                return;
            }
            var tokenReq = context.Request.Headers.Get("JWT");
            var jwtData = JWT.ValidateToken(tokenReq);
            if (!jwtData.IsSuccess)
            {
                await context.WriteResponseAsync(401).ConfigureAwait(false);
                return;
            }
            var isSuccess = await _resultService.AddResult(id, jwtData.Login,result);
            if (!isSuccess)
            {
                await context.WriteResponseAsync(409).ConfigureAwait(false);
                return;
            }
            await context.WriteResponseAsync(200).ConfigureAwait(false);
        }
    }
}

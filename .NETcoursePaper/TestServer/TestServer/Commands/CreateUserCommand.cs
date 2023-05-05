using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestServer.Extensions;
using TestServer.Helpers;
using TestServer.Requests;
using TestServer.Services.Users;


namespace TestServer.Commands
{
    public class CreateUserCommand : ICommand
    {
        public string Path => @"/users";
        public HttpMethod Method => HttpMethod.Post;

        private readonly IUserService _userService;

        public CreateUserCommand(IUserService userService)
        {
            _userService = userService;
        }
        public async Task HandleRequestAsync(HttpListenerContext context, Match path)
        {
            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<UserRequest>(requestBody, out var userRequest))
            {
                await context.WriteResponseAsync(400, "Invalid request body content").ConfigureAwait(false);
                return;
            }
            var user = userRequest.ToEntity();
            var isSuccess = await _userService.AddUserBD(user);
            if (!isSuccess)
            {
                await context.WriteResponseAsync(409).ConfigureAwait(false);
                return;
            }
            await context.WriteResponseAsync(201).ConfigureAwait(false);
        }
    }
}

using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using TestServer.Helpers;
using TestServer.Requests;
using TestServer.Responses;
using TestServer.Services.Users;
using TestServer.Extensions;
using TestServer.Common.Extensions;
using TestServer.Context;
using System.Text.RegularExpressions;

namespace TestServer.Commands
{
    public class LoginUserCommand : ICommand
    {
        public string Path => @"/login";
        public HttpMethod Method => HttpMethod.Post;

        private readonly IUserService _userService;

        public LoginUserCommand(IUserService userService)
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

			// todo(v): если не собираешься создавать новую запись в бд, то лучше не создавать entity
			var user = userRequest.ToEntity();
            string userType;

			// todo(v): использовать _userService.GetUser(userRequest.Login)
			using (var db = new TestContext())
            {
                userType = db.Users.FirstOrDefault(x => x.Login.Equals(user.Login)).UserType;
            }

            var isSuccess = _userService.CheckUser(user);
            if (!isSuccess)
            {
                await context.WriteResponseAsync(409).ConfigureAwait(false);
                return;
            }

            await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(new UserResponse()
            {
                JWT = JWT.GetToken(user.Login, userType),
                UserType = userType
            })).ConfigureAwait(false);
        }
    }
}

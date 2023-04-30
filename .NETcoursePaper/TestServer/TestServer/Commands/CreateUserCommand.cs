using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestServer.Common.Extensions;
using TestServer.Extensions;
using TestServer.Helpers;
using TestServer.Requests;
using TestServer.Responses;
using TestServer.Services;


namespace TestServer.Commands
{
    public class CreateUserCommand: ICommand
    {
        public string Path => @"/users";
        public HttpMethod Method => HttpMethod.Post;

        private readonly IUserService _userService;

        public CreateUserCommand(IUserService userService)
        {
            _userService = userService;
        }
        public async Task HandleRequestAsync(HttpListenerContext context)
        {
            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<UserRequest>(requestBody, out var userRequest))
            {
                await context.WriteResponseAsync(400, "Invalid request body content").ConfigureAwait(false);
                return;
            }
            var user = userRequest.ToEntity();
            
            var isSuccess=await _userService.AddUserBD(user);
            if(isSuccess)
            {
                await context.WriteResponseAsync(201).ConfigureAwait(false);
            }
            else
            {
                await context.WriteResponseAsync(409).ConfigureAwait(false);
            }  
        }
    }
}

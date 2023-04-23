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

            //if (!Enum.TryParse(userRequest.UserType, true, out string userType))
            //{
            //    await context.WriteResponseAsync(400, $"Failed parse detail type {detailRequest.Type}").ConfigureAwait(false);
            //    return;
            //}

            //var manufacturer = await _manufacturersProvider.GetManufacturerByNameAsync(detailRequest.ManufacturerName).ConfigureAwait(false);
            //if (manufacturer == null)
            //{
            //    await context.WriteResponseAsync(404, $"Not found manufacturer by name {detailRequest.ManufacturerName}").ConfigureAwait(false);
            //    return;
            //}

            //var detail = detailRequest.ToEntity(detailType, manufacturer.Id);

            //var createdDetail = await _detailsProvider.CreateDetailAsync(detail).ConfigureAwait(false);

            //var detailResponse = createdDetail.ToResponse();

            //await context.WriteResponseAsync(201, JsonSerializeHelper.Serialize(detailResponse)).ConfigureAwait(false);
            var user = userRequest.ToEntity();
            var addedUser=await _userService.
        }
    }
}

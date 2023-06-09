﻿using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using TestServer.Helpers;
using TestServer.Services.Tests;
using TestServer.Extensions;
using TestServer.Common.Extensions;
using TestServer.Services.DTO;
using System.Text.RegularExpressions;

namespace TestServer.Commands
{
    public class GetTestsCommand : ICommand
    {
        public string Path => @"/tests";
        public HttpMethod Method => HttpMethod.Get;

        private readonly ITestService _testService;

        public GetTestsCommand(ITestService testService)
        {
            _testService = testService;
        }
        public async Task HandleRequestAsync(HttpListenerContext context, Match path)
        {
            var tokenReq = context.Request.Headers.Get("Authorization");
            var jwtData = JWT.ValidateToken(tokenReq);
            if (jwtData.IsFaulted)
            {
                await context.WriteResponseAsync(401).ConfigureAwait(false);
                return;
            }
            if (jwtData.UserRole.Equals(UserRoles.Teacher))
            {
                var isSuccessTeacher = await _testService.GetTests(jwtData.Login);
                if (isSuccessTeacher == null)
                {
                    await context.WriteResponseAsync(409).ConfigureAwait(false);
                    return;
                }
                List<TestResponseDTO> responses = new();
                foreach (var el in isSuccessTeacher)
                {
                    responses.Add(new TestResponseDTO() { Name = el.Name, AttemptsCount = el.AttemptsCount, CreatedBy = el.User.Login, Id = el.Id });
                }
                await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(responses)).ConfigureAwait(false);
                return;
            }
            // todo: переименовать
            var isSuccess = await _testService.GetTests();
            if (isSuccess == null)
            {
                await context.WriteResponseAsync(409).ConfigureAwait(false);
                return;
            }
            await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(isSuccess)).ConfigureAwait(false);
        }
    }
}

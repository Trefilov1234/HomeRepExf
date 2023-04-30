﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestServer.Common.Extensions;
using TestServer.Helpers;
using TestServer.Requests;
using TestServer.Responses;
using TestServer.Services;
using TestServer.Extensions;

namespace TestServer.Commands
{
    public class AddTestCommand:ICommand
    {
        public string Path => @"/tests";
        public HttpMethod Method => HttpMethod.Post;

        private readonly ITestService _testService;

        public AddTestCommand(ITestService testService)
        {
            _testService = testService;
        }
        public async Task HandleRequestAsync(HttpListenerContext context)
        {
            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<TestRequest>(requestBody, out var testRequest))
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
            
            var test = testRequest.ToEntity();

            var isSuccess = await _testService.AddTest(test, jwtData.Value[1]) ;
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

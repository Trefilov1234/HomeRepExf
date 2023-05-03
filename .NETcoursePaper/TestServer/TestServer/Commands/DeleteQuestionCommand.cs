﻿using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using TestServer.Common.Extensions;
using TestServer.Services;
using TestServer.Extensions;

namespace TestServer.Commands
{
    public class DeleteQuestionCommand:ICommand
    {
        private const string testId = "testId";
        private const string questionId = "questionId";
        public string Path => @$"/tests/(?<{testId}>\d+)/questions/(?<{questionId}>\d+)";
        public HttpMethod Method => HttpMethod.Delete;

        private readonly IQuestionService _questionService;

        public DeleteQuestionCommand(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        public async Task HandleRequestAsync(HttpListenerContext context)
        {
            var question_id = Path.GetIntGroup(context, questionId);
            var test_id = Path.GetIntGroup(context, testId);
            var tokenReq = context.Request.Headers.Get("JWT");
            var jwtData = JWT.ValidateToken(tokenReq);
            if (!jwtData.IsSuccess)
            {
                await context.WriteResponseAsync(401).ConfigureAwait(false);
                return;
            }
            var isSuccess = await _questionService.DeleteQuestion(question_id, test_id);
            if (!isSuccess)
            {
                await context.WriteResponseAsync(409).ConfigureAwait(false);
                return;
            }
            await context.WriteResponseAsync(200).ConfigureAwait(false);
        }
    }
}

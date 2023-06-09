﻿using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using TestServer.Common.Extensions;
using TestServer.Helpers;
using TestServer.Services.Questions;
using TestServer.Extensions;
using TestServer.Services.DTO;
using System.Text.RegularExpressions;

namespace TestServer.Commands
{
    public class UpdateQuestionCommand : ICommand
    {
        private const string testId = "testId";
        private const string questionId = "questionId";
        public string Path => @$"/tests/(?<{testId}>\d+)/questions/(?<{questionId}>\d+)";
        public HttpMethod Method => HttpMethod.Put;

        private readonly IQuestionService _questionService;

        public UpdateQuestionCommand(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        public async Task HandleRequestAsync(HttpListenerContext context, Match path)
        {
            var question_id = path.GetIntGroup(questionId);
            var test_id = path.GetIntGroup(testId);
            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<QuestionRequestDTO>(requestBody, out var questionRequest))
            {
                await context.WriteResponseAsync(400, "Invalid request body content").ConfigureAwait(false);
                return;
            }
            var tokenReq = context.Request.Headers.Get("Authorization");
            var jwtData = JWT.ValidateToken(tokenReq);
            if (jwtData.IsFaulted)
            {
                await context.WriteResponseAsync(401).ConfigureAwait(false);
                return;
            }
            if (!jwtData.UserRole.Equals(UserRoles.Teacher))
            {
                await context.WriteResponseAsync(403).ConfigureAwait(false);
                return;
            }
            var isSuccess = await _questionService.UpdateQuestion(question_id, test_id, questionRequest);
            if (!isSuccess)
            {
                await context.WriteResponseAsync(409).ConfigureAwait(false);
                return;
            }
            await context.WriteResponseAsync(200).ConfigureAwait(false);
        }
    }
}

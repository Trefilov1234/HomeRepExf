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
using TestServer.Requests;
using TestServer.Services;
using TestServer.Extensions;
using TestServer.Services.ObjectsHelpers;

namespace TestServer.Commands
{
    public class UpdateQuestionCommand:ICommand
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
        public async Task HandleRequestAsync(HttpListenerContext context)
        {
            var match = Regex.Match(context.Request.Url.AbsolutePath, Path, RegexOptions.IgnoreCase);
            var test_id = int.Parse(match.Groups[testId].Value);
            var question_id= int.Parse(match.Groups[questionId].Value);
            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<Services.ObjectsHelpers.QuestionRequest>(requestBody, out var questionRequest))
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
            var isSuccess = await _questionService.UpdateQuestion(question_id,test_id,questionRequest);
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

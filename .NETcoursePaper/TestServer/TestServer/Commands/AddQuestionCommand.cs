using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestServer.Common.Extensions;
using TestServer.Helpers;
using TestServer.Requests;
using TestServer.Services;
using TestServer.Extensions;
using System.Text.RegularExpressions;

namespace TestServer.Commands
{
    public class AddQuestionCommand:ICommand
    {
        private const string testId = "Id";
        public string Path => @$"/tests/(?<{testId}>\d+)/questions";
        public HttpMethod Method => HttpMethod.Post;

        private readonly IQuestionService _questionService;

        public AddQuestionCommand(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        public async Task HandleRequestAsync(HttpListenerContext context)
        {
            var match = Regex.Match(context.Request.Url.AbsolutePath, Path, RegexOptions.IgnoreCase);
            var id = int.Parse(match.Groups[testId].Value);
            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<QuestionRequest>(requestBody, out var questionRequest))
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

            var question = questionRequest.ToEntity(id);

            var isSuccess = await _questionService.AddQuestion(question);
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

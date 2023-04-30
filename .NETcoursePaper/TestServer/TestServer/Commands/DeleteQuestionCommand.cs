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
            var match = Regex.Match(context.Request.Url.AbsolutePath, Path, RegexOptions.IgnoreCase);
            var test_id = int.Parse(match.Groups[testId].Value);
            var gusetion_id = int.Parse(match.Groups[questionId].Value);
            var tokenReq = context.Request.Headers.Get("JWT");
            var jwtData = JWT.ValidateToken(tokenReq);
            if (!jwtData.Key)
            {
                await context.WriteResponseAsync(401).ConfigureAwait(false);
                return;
            }

            var isSuccess = await _questionService.DeleteQuestion(gusetion_id, test_id);
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

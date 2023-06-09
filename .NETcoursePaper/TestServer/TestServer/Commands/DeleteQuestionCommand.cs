﻿using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using TestServer.Common.Extensions;
using TestServer.Services.Questions;
using TestServer.Extensions;
using System.Text.RegularExpressions;

namespace TestServer.Commands
{
    public class DeleteQuestionCommand : ICommand
    {
		// todo(v): переименовать
		private const string testId = "testId";
        private const string questionId = "questionId";
        public string Path => @$"/tests/(?<{testId}>\d+)/questions/(?<{questionId}>\d+)";
        public HttpMethod Method => HttpMethod.Delete;

        private readonly IQuestionService _questionService;

        public DeleteQuestionCommand(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        public async Task HandleRequestAsync(HttpListenerContext context, Match path)
        {
			// todo(v): переименовать
			var question_id = path.GetIntGroup(questionId);
            var test_id = path.GetIntGroup(testId);
            var tokenReq = context.Request.Headers.Get("Authorization");
            var jwtData = JWT.ValidateToken(tokenReq);
            if (jwtData.IsFaulted)
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

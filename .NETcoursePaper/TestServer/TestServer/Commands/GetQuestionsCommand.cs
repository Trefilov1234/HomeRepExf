using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestServer.Common.Extensions;
using TestServer.Helpers;
using TestServer.Services;
using TestServer.Extensions;
using System.Text.RegularExpressions;
using TestServer.Services.ObjectsHelpers;

namespace TestServer.Commands
{
    public class GetQuestionsCommand:ICommand
    {
        private const string TestIdKey = "TestId";
        public string Path => @$"/tests/(?<{TestIdKey}>\d+)/questions";
        
        public HttpMethod Method => HttpMethod.Get;

        private readonly IQuestionService _questionService;

        public GetQuestionsCommand(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        public async Task HandleRequestAsync(HttpListenerContext context)
        {
            var match = Regex.Match(context.Request.Url.AbsolutePath, Path, RegexOptions.IgnoreCase);
            var testId = int.Parse(match.Groups[TestIdKey].Value);
            var tokenReq = context.Request.Headers.Get("JWT");
            var jwtData = JWT.ValidateToken(tokenReq);
            if (!jwtData.Key)
            {
                await context.WriteResponseAsync(401).ConfigureAwait(false);
                return;
            }

            if (jwtData.Value[0].Equals("teacher"))
            {
                var isSuccessTeacher = await _questionService.GetQuestions(testId);
                List<QuestionResponse> responses = new();
                foreach (var el in isSuccessTeacher)
                {
                    responses.Add(new QuestionResponse() { Text=el.Text,Answers=el.Answers, RightAnswer=el.RightAnswer,AnswerValue=el.AnswerValue, Id = el.Id });
                }

                if (isSuccessTeacher != null)
                {
                    await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(responses)).ConfigureAwait(false);
                }
                else
                {
                    await context.WriteResponseAsync(409).ConfigureAwait(false);
                }
                return;
            }
            else
            {
                var isSuccess = await _questionService.GetQuestions(testId);
                if (isSuccess != null)
                {
                    await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(isSuccess)).ConfigureAwait(false);
                }
                else
                {
                    await context.WriteResponseAsync(409).ConfigureAwait(false);
                }
            }
        }
    }
}

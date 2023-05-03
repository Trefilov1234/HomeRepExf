using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using TestServer.Common.Extensions;
using TestServer.Helpers;
using TestServer.Services;
using TestServer.Extensions;
using TestServer.Services.DTO;

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
            var testId = Path.GetIntGroup(context, TestIdKey);
            var tokenReq = context.Request.Headers.Get("JWT");
            var jwtData = JWT.ValidateToken(tokenReq);
            if (!jwtData.IsSuccess)
            {
                await context.WriteResponseAsync(401).ConfigureAwait(false);
                return;
            }
            var isSuccess = await _questionService.GetQuestions(testId);
            if (isSuccess == null)
            {
                await context.WriteResponseAsync(409).ConfigureAwait(false);
                return;
            }
            List<QuestionResponseDTO> responses = new();
            foreach (var el in isSuccess)
            {
                responses.Add(new QuestionResponseDTO() { Text=el.Text,Answers=el.Answers, RightAnswer=el.RightAnswer,AnswerValue=el.AnswerValue, Id = el.Id,TestName=el.Test.Name });
            }
            await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(responses)).ConfigureAwait(false);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServer.Context;
using TestServer.Domain.Entities;
using TestServer.Requests;

namespace TestServer.Extensions
{
    public static class QuestionExtensions
    {
        //public static TestResponse ToResponse(this Test test)
        //{
        //    return new TestResponse
        //    {
        //        Name = test.Name,
        //        AttemptsCount = test.AttemptsCount
        //    };
        //}

        public static Question ToEntity(this QuestionRequest request,int testId)
        {
            return new Question()
            {
                Text = request.Text,
                Answers = request.Answers,
                RightAnswer = request.RightAnswer,
                AnswerValue = request.AnswerValue,
                TestId = testId,
                Image = request.Image, 
            };
        }
    }
}

using TestServer.Domain.Entities;
using TestServer.Requests;

namespace TestServer.Extensions
{
    public static class QuestionExtensions
    {
        public static Question ToEntity(this QuestionRequest request, int testId)
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

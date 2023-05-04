using EditTestClient.Api.Responses;
using System.Collections.Generic;

namespace EditTestClient.Services.Questions
{
    public interface IQuestionService
    {
        public List<QuestionResponse> QuestionBank { get; set; }
        public string ImagePath { get; set; }

        public bool AddQuestion(byte[] image, string task, string answers, string rightAnswer, int answerValue);

        public bool UpdateQuestion(int index, byte[] image, string task, string answers, string rightAnswer, int answerValue);
    }
}

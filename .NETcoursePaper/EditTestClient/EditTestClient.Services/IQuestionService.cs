using EditTestClient.Api.Requests;
using EditTestClient.Api.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditTestClient.Services
{
    public interface IQuestionService
    {
        public List<QuestionResponse> QuestionBank { get; set; }
        public string ImagePath { get; set; }
        public bool AddQuestion(byte[] image, string task, string answers, string rightAnswer, int answerValue);
        public bool UpdateQuestion(int index, byte[] image, string task, string answers, string rightAnswer, int answerValue);
    }
}

using EditTestClient.Api.Requests;
using EditTestClient.Api.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EditTestClient.Services
{
    public class QuestionService:IQuestionService
    {
        public List<QuestionResponse> QuestionBank { get; set; }
        public string ImagePath { get; set; }
        public QuestionService() 
        {
            QuestionBank=new List<QuestionResponse>();
        }
        public bool AddQuestion(byte[] image,string task,string answers,string rightAnswer,int answerValue)
        {
            if (QuestionBank.FirstOrDefault(x=>x.Text.Equals(task))==null)
            {
                QuestionBank.Add(new QuestionResponse()
                {
                    Image = image,
                    Text = task,
                    Answers = answers,
                    RightAnswer = rightAnswer,
                    AnswerValue = answerValue,
                });
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateQuestion(int index,byte[] image, string task, string answers, string rightAnswer, int answerValue)
        {
            var tasks= QuestionBank.Select(x => x.Text).ToList();  
            if(tasks.Contains(task)&&tasks.IndexOf(task)!=index)
            {
                return false;
            }
            QuestionBank[index].Image = image;
            QuestionBank[index].Text = task;
            QuestionBank[index].Answers = answers;
            QuestionBank[index].RightAnswer = rightAnswer;
            QuestionBank[index].AnswerValue = answerValue;
            return true;
        }
    }
}

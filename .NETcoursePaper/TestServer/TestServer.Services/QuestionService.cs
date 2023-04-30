using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServer.Context;
using TestServer.Domain.Entities;
using TestServer.Services.ObjectsHelpers;

namespace TestServer.Services
{
    public class QuestionService: IQuestionService
    {
        public async Task<bool> AddQuestion(Question question)
        {
            using(var db=new TestContext())
            {
                try
                {
                    db.Questions.Add(question);
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<List<Question>> GetQuestions(int testId)
        {
            using(var db=new TestContext())
            {
                var questions = await db.Questions.Where(x => x.TestId.Equals(testId)).ToListAsync();
                return questions;
            }
        }
        public async Task<bool> UpdateQuestion(int questionId,int testId,QuestionRequest question)
        {
            using(var db=new TestContext())
            {
                var curQuestion = await db.Questions.FirstOrDefaultAsync(x => x.Id == questionId);
                if (curQuestion == null) return false;
                curQuestion.Text = question.Text;
                curQuestion.Answers=question.Answers;
                curQuestion.AnswerValue=question.AnswerValue;
                curQuestion.RightAnswer=question.RightAnswer;
                curQuestion.Image=question.Image;
                await db.SaveChangesAsync();
            }
            return true;
        }
        public async Task<Question> GetQuestionById(int questionId,int testId)
        {
            using (var db = new TestContext())
            {
                var question = await db.Questions.FirstOrDefaultAsync(x => x.TestId.Equals(testId)&&x.Id.Equals(questionId));
                return question;
            }
        }
        public async Task<bool> DeleteQuestion(int questionId,int testId)
        {
            using (var db = new TestContext())
            {
                var question = await db.Questions.FirstOrDefaultAsync(x => x.TestId.Equals(testId) && x.Id.Equals(questionId));
                if(question == null) return false;
                db.Questions.Remove(question);
                await db.SaveChangesAsync();
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServer.Domain.Entities;
using TestServer.Services.ObjectsHelpers;

namespace TestServer.Services
{
    public interface IQuestionService
    {
        public Task<bool> AddQuestion(Question question);
        public Task<List<Question>> GetQuestions(int testId);
        public Task<bool> UpdateQuestion(int questionId, int testId, QuestionRequest question);
        public Task<Question> GetQuestionById(int questionId, int testId);
        public Task<bool> DeleteQuestion(int questionId, int testId);
    }
}

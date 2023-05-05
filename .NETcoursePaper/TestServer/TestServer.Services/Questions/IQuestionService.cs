using System.Collections.Generic;
using System.Threading.Tasks;
using TestServer.Domain.Entities;
using TestServer.Services.DTO;

namespace TestServer.Services.Questions
{
    public interface IQuestionService
    {
        public Task<bool> AddQuestion(Question question);
        public Task<List<Question>> GetQuestions(int testId);
        public Task<bool> UpdateQuestion(int questionId, int testId, QuestionRequestDTO question);
        public Task<Question> GetQuestionById(int questionId, int testId);
        public Task<bool> DeleteQuestion(int questionId, int testId);
    }
}

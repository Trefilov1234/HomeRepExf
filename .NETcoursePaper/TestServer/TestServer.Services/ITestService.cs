using System.Collections.Generic;
using System.Threading.Tasks;
using TestServer.Domain.Entities;
using TestServer.Services.DTO;

namespace TestServer.Services
{
    public interface ITestService
    {
        public Task<bool> AddTest(Test test,string login);
        public Task<List<Test>> GetTests();
        public Task<List<Test>> GetTests(string login);
        public Task<Test> GetTestById(int id);
        public Task<bool> DeleteTestById(int id);
        public Task<bool> UpdateTestById(TestResponseDTO test, int id);
    }
}

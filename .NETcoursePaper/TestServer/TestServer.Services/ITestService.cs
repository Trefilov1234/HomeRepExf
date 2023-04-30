using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServer.Domain.Entities;

namespace TestServer.Services
{
    public interface ITestService
    {
        public Task<bool> AddTest(Test test,string login);
        public Task<List<Test>> GetTests();
        public Task<List<Test>> GetTests(string login);
        public Task<Test> GetTestById(int id);
        public Task<bool> DeleteTestById(int id);
        public Task<bool> UpdateTestById(TestResponse test, int id);
    }
}

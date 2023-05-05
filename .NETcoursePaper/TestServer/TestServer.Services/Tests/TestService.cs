using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TestServer.Context;
using TestServer.Domain.Entities;
using TestServer.Services.DTO;

namespace TestServer.Services.Tests
{
    public class TestService : ITestService
    {
        public async Task<bool> AddTest(Test test, string login)
        {
            using var db = new TestContext();
            var curUser = db.Users.FirstOrDefault(x => x.Login == login);
            if (curUser == null) return false;
            var curTest = db.Tests.FirstOrDefault(x => x.Name.Equals(test.Name));
            if (curTest != null)
            {
                return false;
            }
            test.UserId = curUser.Id;
            db.Tests.Add(test);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Test>> GetTests(string login)
        {
            using var db = new TestContext();
            var user = await db.Users.FirstOrDefaultAsync(x => x.Login.Equals(login));
            var tests = await db.Tests.Where(x => x.UserId.Equals(user.Id)).ToListAsync();
            return tests;
        }

        public async Task<List<Test>> GetTests()
        {
            using var db = new TestContext();
            var tests = await db.Tests.ToListAsync();
            return tests;
        }

        public async Task<Test> GetTestById(int id)
        {
            using var db = new TestContext();
            var curTest = await db.Tests.FirstOrDefaultAsync(x => x.Id == id);
            return curTest;
        }

        public async Task<bool> DeleteTestById(int id)
        {
            using var db = new TestContext();
            var curTest = await db.Tests.FirstOrDefaultAsync(x => x.Id == id);
            if (curTest == null) return false;
            db.Tests.Remove(curTest);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTestById(TestResponseDTO test, int id)
        {
            using var db = new TestContext();
            var curTest = await db.Tests.FirstOrDefaultAsync(x => x.Id == id);
            if (curTest == null) return false;
            curTest.Name = test.Name;
            curTest.AttemptsCount = test.AttemptsCount;
            await db.SaveChangesAsync();
            return true;
        }
    }
}

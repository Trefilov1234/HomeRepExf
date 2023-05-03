using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TestServer.Context;
using TestServer.Domain.Entities;
using TestServer.Services.DTO;

namespace TestServer.Services
{
    public class ResultService: IResultService
    {
        public async Task<bool> AddResult(int testId,string login,int result)
        {
            using var db = new TestContext();
            var user = await db.Users.FirstOrDefaultAsync(x => x.Login.Equals(login));
            try
            {

                db.UserResults.Add(new UserResult()
                {
                    TestId = testId,
                    UserId = user.Id,
                    Result = result
                });
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<List<CustomResultDTO>> GetResults(int testId,string login)
        {
            using var db = new TestContext();
            var user = await db.Users.FirstOrDefaultAsync(x => x.Login.Equals(login));
            var test = await db.Tests.FirstOrDefaultAsync(x => x.Id.Equals(testId));
            var results = await db.UserResults.Where(x => x.UserId.Equals(user.Id) && x.TestId.Equals(test.Id)).ToListAsync();
            var list = results.Select(x => new CustomResultDTO()
            {
                TestName = test.Name,
                UserLogin = user.Login,
                Result = x.Result
            }).ToList();
            return list;

        }
    }
}

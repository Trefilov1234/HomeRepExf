using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServer.Context;
using TestServer.Context.Entities;

namespace TestServer.Services
{
    public class UserService
    {
        public async Task AddUserBD(User user)
        {
            using (var db = new TestContext())
            {

            }
        }
    }
}

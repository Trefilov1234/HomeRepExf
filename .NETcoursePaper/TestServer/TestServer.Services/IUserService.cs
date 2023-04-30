using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServer.Domain.Entities;

namespace TestServer.Services
{
    public interface IUserService
    {
        public Task<bool> AddUserBD(User user);
        public bool CheckUser(User user);
    }
}

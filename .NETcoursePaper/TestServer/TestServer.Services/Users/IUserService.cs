using System.Threading.Tasks;
using TestServer.Domain.Entities;

namespace TestServer.Services.Users
{
    public interface IUserService
    {
        public Task<bool> AddUserBD(User user);
        public bool CheckUser(User user);
    }
}

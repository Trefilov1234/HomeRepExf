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

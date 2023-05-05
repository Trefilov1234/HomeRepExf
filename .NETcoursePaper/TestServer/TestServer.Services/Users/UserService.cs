using System.Linq;
using System.Threading.Tasks;
using TestServer.Context;
using TestServer.Domain.Entities;

namespace TestServer.Services.Users
{
    public class UserService : IUserService
    {
        public async Task<bool> AddUserBD(User user)
        {
            using var db = new TestContext();
            var curUser = db.Users.FirstOrDefault(x => x.Login.Equals(user.Login) && x.PasswordHash.Equals(user.PasswordHash) && x.UserType.Equals(user.UserType));
            if (curUser != null)
            {
                return false;
            }
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return true;
        }

        public bool CheckUser(User user)
        {
            using var db = new TestContext();
            var curUser = db.Users.FirstOrDefault(x => x.Login.Equals(user.Login) && x.PasswordHash.Equals(user.PasswordHash));
            if (curUser != null)
            {
                return true;
            }
            return false;
        }
    }
}

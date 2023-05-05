using System.Linq;
using System.Threading.Tasks;
using TestServer.Context;
using TestServer.Domain.Entities;

namespace TestServer.Services.Users
{
    public class UserService : IUserService
    {
		// todo(v): убрать логику проверки наличия пользователя, и вместо этого использовать GetUser в команде
		public async Task<bool> AddUserBD(User user)
        {
            using var db = new TestContext();
			// todo(v): проверять только совпадение по логину
			// todo(v): ? сделать Login уникальным в базе данных
			// todo(v): используй FirstOrDefaultAsync
			var curUser = db.Users.FirstOrDefault(x => x.Login.Equals(user.Login) && x.PasswordHash.Equals(user.PasswordHash) && x.UserType.Equals(user.UserType));
            if (curUser != null)
            {
                return false;
            }
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return true;
        }

		// todo(v): переделать в GetUser(string login)
		public bool CheckUser(User user)
        {
            using var db = new TestContext();
			// todo(v): используй FirstOrDefaultAsync
            var curUser = db.Users.FirstOrDefault(x => x.Login.Equals(user.Login) && x.PasswordHash.Equals(user.PasswordHash));
            if (curUser != null)
            {
                return true;
            }
            return false;
        }
    }
}

using System.Collections.Generic;

namespace TestServer.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string UserType { get; set; }
        public ICollection<Test> Tests { get; set; }
        public ICollection<UserResult> UserResults { get; set; }
    }
}

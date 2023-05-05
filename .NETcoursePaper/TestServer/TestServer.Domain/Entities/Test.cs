using System.Collections.Generic;

namespace TestServer.Domain.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AttemptsCount { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<UserResult> UserResults { get; set; }
    }
}

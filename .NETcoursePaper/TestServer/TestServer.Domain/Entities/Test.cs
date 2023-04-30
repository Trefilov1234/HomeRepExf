using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

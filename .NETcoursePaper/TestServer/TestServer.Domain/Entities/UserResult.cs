namespace TestServer.Domain.Entities
{
    public class UserResult
    {
        public int Id { get; set; }
        // todo: PassedBy?User?
        public int UserId { get; set; }
        public User User { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public int Result { get; set; }
    }
}

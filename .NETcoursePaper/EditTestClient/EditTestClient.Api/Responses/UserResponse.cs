namespace EditTestClient.Api.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string UserType { get; set; }
        public string JWT { get; set; }
    }
}

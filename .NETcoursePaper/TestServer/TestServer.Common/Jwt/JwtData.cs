namespace TestServer.Common.Jwt
{
    public class JwtData
    {
		// todo(v): добавить Id юзера
		public bool IsFaulted { get; set; }
        public string Login { get; set; }
        public string UserRole { get; set; }
    }
}

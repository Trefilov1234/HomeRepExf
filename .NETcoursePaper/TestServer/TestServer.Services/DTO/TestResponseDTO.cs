namespace TestServer.Services.DTO
{
	// todo(v): переименовать в TestDto
	public class TestResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AttemptsCount { get; set; }
        public string CreatedBy { get; set; }
    }
}

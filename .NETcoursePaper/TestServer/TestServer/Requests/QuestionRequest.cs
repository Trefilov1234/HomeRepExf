namespace TestServer.Requests
{
    public class QuestionRequest
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string Text { get; set; }
        public string Answers { get; set; }
        public string RightAnswer { get; set; }
        public int AnswerValue { get; set; }
        public string TestName { get; set; }
    }
}

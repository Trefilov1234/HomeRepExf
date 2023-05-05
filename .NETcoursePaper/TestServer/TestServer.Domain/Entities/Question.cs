namespace TestServer.Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public byte[] Image { get; set; }
        public string Answers { get; set; }
		public string RightAnswer { get; set; }
		// todo(v): Score, Cost
		public int AnswerValue { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
    }

	// todo(v):

	//class Answer
	//{
	//	// QuestionId
	//	public string Text { get; set; }
	//	public string IsRight { get; set; }
	//}

	// -- // Answers = (Answer[] answers => serialize)
}

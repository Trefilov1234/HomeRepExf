﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer.Context.Entities
{
    public class Question
    {
        public int Id { get;set; }
        public string Text { get; set; }
        public byte[] Image { get; set; }
        public string Answers { get; set; }
        public string RightAnswer { get; set; }
        public int AnswerValue { get; set; }
        public int TestId { get;set; }
        public Test Test { get; set; }
    }
}

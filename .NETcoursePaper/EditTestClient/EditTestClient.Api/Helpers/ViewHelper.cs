using EditTestClient.Api.Requests;
using EditTestClient.Api.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditTestClient.Api.Helpers
{
    public static class ViewHelper
    {
        public static  List<string> GetTests(List<TestResponse> testResponses)
        {
            var testNames = testResponses.Select(x => x.Name).ToList();
            var attemptsCount = testResponses.Select(x => x.AttemptsCount).ToList();
            List<string> nameNAttempts = new List<string>();
            for (var i = 0; i < testNames.Count; i++)
            {
                nameNAttempts.Add($"{testNames[i]} - {attemptsCount[i]} attempts");
            }
            return nameNAttempts;
        }
        public static List<string> GetQuestions(List<QuestionResponse> questionRequests)
        {
            var texts= questionRequests.Select(x=>x.Text).ToList();
            List<string> nameNAttempts = new List<string>();
            for (var i = 0; i < texts.Count; i++)
            {
                nameNAttempts.Add(texts[i]);
            }
            return nameNAttempts;
        }
    }

}

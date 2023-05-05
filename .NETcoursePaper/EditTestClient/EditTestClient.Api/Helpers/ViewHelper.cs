using EditTestClient.Api.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EditTestClient.Api.Helpers
{
    public static class ViewHelper
    {
        public static List<string> GetTests(List<TestResponse> testResponses)
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
            var texts = questionRequests.Select(x => x.Text).ToList();
            List<string> nameNAttempts = new List<string>();
            for (var i = 0; i < texts.Count; i++)
            {
                nameNAttempts.Add(texts[i]);
            }
            return nameNAttempts;
        }

        public static List<string> GetAnswers(string answers)
        {
            var splittedAnswers = answers.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> variants = new List<string>();
            for (var i = 0; i < splittedAnswers.Count; i++)
            {
                variants.Add($"{i + 1}) {splittedAnswers[i]}");
            }
            return variants;
        }

        public static string ListToString(List<string> list)
        {
            var sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.AppendLine(item);
            }
            return sb.ToString();
        }

        public static string ListToString(List<int> list)
        {
            var sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append(item.ToString() + " ");
            }
            return sb.ToString();
        }

        public static List<string> GetResults(List<ResultResponse> list)
        {
            List<string> results = new List<string>();
            foreach (var item in list)
            {
                results.Add($"{item.UserLogin} - {item.TestName} - {item.Result}");
            }
            return results;
        }
    }

}

using EditTestClient.Api.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EditTestClient.Services.CalculateResults
{
    public class CalculateResultService : ICalculateResultService
    {
        public List<(int answerValue, List<int> rightAnswers)> RightAnswers { get; set; }
        public Dictionary<int, List<int>> UserAnswers { get; set; }

        public CalculateResultService()
        {
            RightAnswers = new List<(int answerValue, List<int> rightAnswers)>();
            UserAnswers = new Dictionary<int, List<int>>();
        }

        public void InitializeRightAnswers(List<QuestionResponse> questions)
        {
            foreach (QuestionResponse questionsItem in questions)
            {
                var answers = questionsItem.Answers.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                var rightAns = questionsItem.RightAnswer.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                var answerValue = questionsItem.AnswerValue;
                int index;
                var curRightAnswers = new List<int>();
                foreach (var el in rightAns)
                {
                    index = answers.IndexOf(el) + 1;
                    curRightAnswers.Add(index);
                }
                curRightAnswers = curRightAnswers.OrderBy(el => el).ToList();
                RightAnswers.Add((answerValue, curRightAnswers));
            }
        }

        public int GetMaxScore()
        {
            int max = RightAnswers.Sum(x => x.answerValue);
            return max;
        }

        public void AddOrUpdateUserAnswer(int index, string answer)
        {
            var splitted = answer.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var curUserAnswers = new List<int>();
            foreach (var item in splitted)
            {
                if (int.TryParse(item, out var variant))
                {
                    curUserAnswers.Add(variant);
                }
            }
            curUserAnswers = curUserAnswers.OrderBy(el => el).ToList();
            try
            {
                var temp = UserAnswers[index].SequenceEqual(curUserAnswers);
            }
            catch (Exception)
            {
                UserAnswers.Add(index, curUserAnswers);
                return;
            }
            UserAnswers[index] = curUserAnswers;
        }

        public int CalculateResult()
        {
            int i = 0;
            int result = 0;
            foreach (var item in UserAnswers)
            {
                var rightAnswerData = RightAnswers[i];
                if (item.Value.Count == rightAnswerData.rightAnswers.Count && item.Value.SequenceEqual(rightAnswerData.rightAnswers))
                {
                    result += rightAnswerData.answerValue;
                }
                i++;
            }
            return result;
        }
    }
}

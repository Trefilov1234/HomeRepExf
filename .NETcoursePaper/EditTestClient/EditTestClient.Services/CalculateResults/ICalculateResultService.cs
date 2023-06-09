﻿using EditTestClient.Api.Responses;
using System.Collections.Generic;

namespace EditTestClient.Services.CalculateResults
{
    public interface ICalculateResultService
    {
        public List<(int answerValue, List<int> rightAnswers)> RightAnswers { get; set; }
        public Dictionary<int, List<int>> UserAnswers { get; set; }

        public void InitializeRightAnswers(List<QuestionResponse> questions);

        public void AddOrUpdateUserAnswer(int index, string answer);

        public int CalculateResult();

        public int GetMaxScore();
    }
}

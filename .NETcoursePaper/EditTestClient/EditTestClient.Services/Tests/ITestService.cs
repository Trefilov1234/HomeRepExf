﻿using EditTestClient.Api.Responses;
using System.Collections.Generic;

namespace EditTestClient.Services.Tests
{
    public interface ITestService
    {
        public List<TestResponse> TestBank { get; set; }
    }
}

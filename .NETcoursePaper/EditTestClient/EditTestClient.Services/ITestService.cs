﻿using EditTestClient.Api.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditTestClient.Services
{
    public interface ITestService
    {
        public List<TestResponse> TestBank { get; set; }
    }
}

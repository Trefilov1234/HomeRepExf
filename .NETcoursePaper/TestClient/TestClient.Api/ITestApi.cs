﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestClient.Api.Requests;
using TestClient.Api.Responses;

namespace TestClient.Api
{
    public interface ITestApi
    {
        public Task<UserResponse> CreateUser(UserRequest user);
    }
}

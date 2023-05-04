﻿using EditTestClient.Api.Requests;
using EditTestClient.Api.Responses;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace EditTestClient.Api.Users
{
    public interface IUserApi
    {
        public Task<HttpResponseMessage> CreateUser(UserRequest user);

        public Task<(HttpStatusCode statusCode, UserResponse user)> LoginUser(UserRequest user);
    }
}
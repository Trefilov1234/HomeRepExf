﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServer.Common.Extensions;
using TestServer.Context.Entities;
using TestServer.Requests;
using TestServer.Responses;

namespace TestServer.Extensions
{
    public static class UserExtensions
    {
        public static UserResponse ToResponse(this User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Login = user.Login,
                PasswordHash = user.PasswordHash,
                UserType=user.UserType
            };
        }

        public static User ToEntity(this UserRequest userRequest)
        {
            return new User
            {
                Login = userRequest.Login,
                PasswordHash = Convert.ToBase64String(PasswordHasher.GenerateSha256Hash(userRequest.Password, PasswordHasher.GenerateSalt())),
                UserType = userRequest.UserType
                
            };
            
        }
    }
}
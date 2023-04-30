using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TestServer.Common.Extensions
{
    public static class JWT
    {
        private static readonly string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
        public static string GetToken(string login,string HashPassword,string UserType)
        {
            var handler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sec));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var identity = new ClaimsIdentity(new GenericIdentity(login), new[] { new Claim("UserType", UserType),new Claim("Login", login) });
            var token = handler.CreateJwtSecurityToken(subject: identity,
                                                       signingCredentials: signingCredentials,
                                                       audience: "TestAudience",
                                                       issuer: "TestIssuer"
                                                       );
            return handler.WriteToken(token);
        }
        public static KeyValuePair<bool, List<string>> ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();
            string userType;
            string login;
            SecurityToken validatedToken;
            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                userType=claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "UserType")?.Value;
                login = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "Login")?.Value;
            }
            catch (Exception )
            {
                return new KeyValuePair<bool, List<string>>(false, null);
            }
            return new KeyValuePair<bool, List<string>>(true, new List<string>() { userType,login }); ;
        }

        private static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = "TestIssuer",
                ValidAudience = "TestAudience",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sec)) // The same key as the one that generate the token
            };
        }
    }
}

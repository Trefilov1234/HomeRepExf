using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using TestServer.Common.Jwt;

namespace TestServer.Common.Extensions
{
	// todo(v): сделать не static и сохранить в приватных полях Issuer и SecurityKey
	public static class JWT
    {
        private const string BearerPrefix = "Bearer ";

        public static string GetToken(string login, string UserType)
        {
            var handler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationTokens.SecurityKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var identity = new ClaimsIdentity(new GenericIdentity(login),
                new[] { new Claim("UserType", UserType), new Claim("Login", login) });
            var token = handler.CreateJwtSecurityToken(subject: identity, signingCredentials: signingCredentials, issuer: ConfigurationTokens.Issuer);
            return handler.WriteToken(token);
        }

        public static JwtData ValidateToken(string authToken)
        {
            if (authToken?.StartsWith(BearerPrefix, StringComparison.OrdinalIgnoreCase) == true)
                authToken = authToken.Substring(BearerPrefix.Length);
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();
            string userType;
            string login;
            SecurityToken validatedToken;
            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                userType = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "UserType")?.Value;
                login = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "Login")?.Value;
            }
            catch (Exception)
            {
                return new JwtData() { IsFaulted = true };
            }
            return new JwtData { UserRole = userType, Login = login };
        }

        private static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false,
                ValidateAudience = false,
				// todo(v): true
				ValidateIssuer = false,
                ValidIssuer = ConfigurationTokens.Issuer,
				// todo(v): использовать static
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationTokens.SecurityKey))
            };
        }
    }
}

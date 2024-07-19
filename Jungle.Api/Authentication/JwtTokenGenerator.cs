using Jungle.Shared.Requests;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jungle.Api.Authentication
{
    public static class JwtTokenGenerator
    {
        private const string tokenSecret = "Fortheloveofgodpleasemakeitlongerthanthis";
        private static readonly TimeSpan tokenExpiration = TimeSpan.FromHours(2);

        public static string Generate(UserInfoDto userInfo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(tokenSecret);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, userInfo.Email),
                new(JwtRegisteredClaimNames.Email, userInfo.Email),
                new("userId", userInfo.UserId.ToString()),
                new("isAdmin", userInfo.IsAdmin.ToString(), ClaimValueTypes.Boolean),
                new("isTenant", userInfo.IsTenant.ToString(), ClaimValueTypes.Boolean)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(tokenExpiration),
                Issuer = "Jungle e-commerce",
                Audience = "Jungle e-commerce",
                IssuedAt = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
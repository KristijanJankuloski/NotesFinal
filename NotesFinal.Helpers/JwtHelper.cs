using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NotesFinal.DTOs.UserDTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotesFinal.Helpers
{
    public static class JwtHelper
    {
        public static string GenerateToken(UserTokenDto user, IConfiguration configuration)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
            };

            JwtSecurityToken token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(int.Parse(configuration["Jwt:ExpireTime"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GenerateRefreshToken(UserTokenDto user, IConfiguration configuration)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Sid, user.Id.ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(int.Parse(configuration["Jwt:RefreshExpireTime"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static UserTokenDto GetCurrentUser(ClaimsPrincipal user)
        {
            return new UserTokenDto
            {
                Id = int.Parse(user.FindFirst(ClaimTypes.Sid)?.Value),
                Username = user.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                FirstName = user.FindFirst(ClaimTypes.GivenName)?.Value,
                LastName = user.FindFirst(ClaimTypes.Surname)?.Value
            };
        }
    }
}

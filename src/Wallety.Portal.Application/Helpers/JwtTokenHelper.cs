using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Wallety.Portal.Core.Entity.User;

namespace Wallety.Portal.Application.Helpers
{
    public static class JwtTokenHelper
    {
        public static string GenerateJwtToken(UserEntity existingUser, UserRoleEntity role, string secretKey)
        {
            var claimList = new List<Claim>
            {
                new(ClaimTypes.Role, role.RoleName!),
                new(ClaimTypes.Email, existingUser.Email!),
                new(ClaimTypes.Name, existingUser.FirstName),
                new(ClaimTypes.Surname, existingUser.Surname),
                new(ClaimTypes.NameIdentifier, existingUser.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddDays(1);

            var token = new JwtSecurityToken(
                claims: claimList,
                notBefore: DateTime.UtcNow,
                expires: expireDate,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

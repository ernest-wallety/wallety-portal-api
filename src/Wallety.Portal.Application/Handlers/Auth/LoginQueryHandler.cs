using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Wallety.Portal.Application.Commands;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Response.Auth;
using Wallety.Portal.Application.Response.User;
using Wallety.Portal.Core.Entity.User;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Services;
using Wallety.Portal.Core.Utils;

namespace Wallety.Portal.Application.Handlers.Auth
{
    public class LoginQueryHandler(
        IConfigurationService config,
        IUserRepository repository,
        ICachingInMemoryService caching) :
        IRequestHandler<CreateLoginCommand, LoginResponse>
    {
        private readonly IConfigurationService _config = config;
        private readonly IUserRepository _repository = repository;
        private readonly ICachingInMemoryService _caching = caching;

        public async Task<LoginResponse> Handle(CreateLoginCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _repository.GetUserByEmail()
                ?? throw new ArgumentException("The provided email or username is invalid.");

            if (!(request.Password == existingUser.PasswordHash)) throw new UnauthorizedAccessException("Password is incorrect.");

            bool isPasswordVerified = CryptoUtil.VerifyPassword(request.Password, existingUser.SecurityStamp!, existingUser.PasswordHash);

            if (!isPasswordVerified) throw new UnauthorizedAccessException("Password is incorrect.");

            var expiryDate = DateTime.Now.AddDays(1);

            var responseLogin = MapResponseLogin(existingUser!, GenerateJwtToken(existingUser!));

            // Set the user with token and expiry date
            existingUser.Token = responseLogin.Token;
            existingUser.ExpiryDateTime = expiryDate;
            existingUser.LoginTimeStamp = DateTime.Now;
            existingUser.UpdatedBy = $"{existingUser.FirstName} {existingUser.Surname}";

            SetUserInCache(existingUser!, responseLogin);

            // Update the user with token and expiry date
            await _repository.UpdateUser(existingUser!);

            return responseLogin;
        }

        #region Helper methods
        private string GenerateJwtToken(UserEntity existingUser)
        {
            var claimList = new List<Claim>
        {
            new(ClaimTypes.Name, existingUser.Email!),
            new(ClaimTypes.Role, existingUser.RoleName!)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.SecretKey()!));
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

        private MapResponseLogin(UserEntity existingUser, string token)
        {
            var expiryDate = DateTime.Now.AddDays(1);
            int timeStamp = DateUtil.ConvertToTimeStamp(expiryDate);

            var user = LazyMapper.Mapper.Map<UserResponse>(existingUser);

            return new LoginResponse
            {
                ResponseMessage = "Logged in successfully!",

                RoleCodes = existingUser.Roles.Select(role => new UserRoleResponse
                {
                    RoleCode = role.RoleCode,
                    RoleName = role.RoleName
                }).ToList(),

                Success = true,

                User = user,

                UserId = user.UserId,
                SessionToken = token,
                Email = user.Email!,
                Username = user.Username!,
                ExpireDate = expiryDate,
                Role = user.,
                TimeStamp = timeStamp
            };
        }


        private void SetUserInCache(UserEntity existingUser, LoginResponse responseLogin)
        {
            _caching.Set("Token", responseLogin., TimeSpan.FromDays(1));
            _caching.Set("LoggedInUserId", existingUser.UserId, TimeSpan.FromDays(1));
            _caching.Set(responseLogin.SessionToken!, responseLogin, TimeSpan.FromDays(1));
        }
        #endregion
    }
}

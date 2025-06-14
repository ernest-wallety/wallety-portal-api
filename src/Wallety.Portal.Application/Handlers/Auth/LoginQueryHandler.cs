using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Wallety.Portal.Application.Commands;
using Wallety.Portal.Application.Helpers;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Response.Auth;
using Wallety.Portal.Application.Response.User;
using Wallety.Portal.Core.Entity.User;
using Wallety.Portal.Core.Enum;
using Wallety.Portal.Core.Helpers;
using Wallety.Portal.Core.Helpers.Constants;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Services;
using Wallety.Portal.Core.Specs;
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
            var existingUser = await _repository.GetUserByEmail(request.Email)
                ?? throw new ArgumentException("The provided email or username is invalid.").WithDisplayData(EnumValidationDisplay.Toastr);

            bool isPasswordVerified = CryptoUtil.VerifyPassword(request.Password, existingUser.SecurityStamp!, existingUser.PasswordHash);

            if (!isPasswordVerified) throw new UnauthorizedAccessException("Password is incorrect.").WithDisplayData(EnumValidationDisplay.Toastr);

            var expiryDate = DateTime.Now.AddDays(1);

            var userSession = await _repository.GetUserSession(existingUser.UserId);

            if (userSession.Items.Any())
            {
                var session = userSession.Items.Where(x => x.IsActive).FirstOrDefault();

                if (session != null) await _repository.UpdateUserSession(session);
            }

            var role = await GetUserRolesAndDefaultRole(existingUser);

            var responseLogin = MapLoginResponse(existingUser!, JwtTokenHelper.GenerateJwtToken(existingUser!, role.Items.Where(r => r.IsDefault == true).FirstOrDefault()!, _config.SecretKey()));

            // Set the user with token and expiry date
            // existingUser.Token = responseLogin.SessionToken;
            // existingUser.ExpiryDateTime = expiryDate;
            // existingUser.LoginTimeStamp = DateTime.Now;
            // existingUser.UpdatedBy = $"{existingUser.FirstName} {existingUser.Surname}";

            SetUserInCache(existingUser!, responseLogin, request.Password);

            // Update the user with token, user session and expiry date
            // await _repository.UpdateUser(existingUser!);

            var sessionResponse = await _repository.CreateUserSession(new UserSessionEntity
            {
                UserId = existingUser.UserId,
                SessionToken = responseLogin.SessionToken!
            });

            if (!sessionResponse.IsSuccess)
                throw new Exception(sessionResponse.ResponseMessage).WithDisplayData(EnumValidationDisplay.Toastr);

            return responseLogin;
        }

        #region Helper methods
        private async Task<DataList<UserRoleEntity>> GetUserRolesAndDefaultRole(UserEntity existingUser)
        {
            var userRoles = existingUser.Roles.ToList();
            var defaultRole = userRoles.FirstOrDefault(role => role.IsDefault == true)?.RoleId;

            if (!string.IsNullOrEmpty(defaultRole!.ToString()))
            {
                if (userRoles.Any(rc => rc.RoleId == RoleConstants.ADMIN.ToString()))
                    defaultRole = await UpdateDefaultRole(existingUser, RoleConstants.ADMIN.ToString());
                else if (userRoles.Any(rc => rc.RoleId == RoleConstants.CUSTOMER_SERVICE_AGENT.ToString()))
                    defaultRole = await UpdateDefaultRole(existingUser, RoleConstants.CUSTOMER_SERVICE_AGENT.ToString());
                else if (userRoles.Any(rc => rc.RoleId == RoleConstants.EXECUTIVE.ToString()))
                    defaultRole = await UpdateDefaultRole(existingUser, RoleConstants.EXECUTIVE.ToString());
            }

            return await _repository.GetUserRoles(existingUser.UserId);
        }

        private async Task<string> UpdateDefaultRole(UserEntity existingUser, string roleId)
        {
            var roleCodes = existingUser.Roles.Where(rc => rc.RoleId == roleId && rc.IsDefault == true).ToList();

            var role = roleCodes.FirstOrDefault(r => r.RoleId == roleId);

            if (role != null)
            {
                var result = await _repository.UpdateDefaultRole(roleId, existingUser.UserId);

                if (!result.IsSuccess) throw new Exception(result.ResponseMessage).WithDisplayData(EnumValidationDisplay.Toastr);
            }

            return roleId;
        }

        private static LoginResponse MapLoginResponse(UserEntity existingUser, string token)
        {
            var expiryDate = DateTime.Now.AddDays(1);
            int timeStamp = DateUtil.ConvertToTimeStamp(expiryDate);

            var user = LazyMapper.Mapper.Map<UserResponse>(existingUser);

            return new LoginResponse
            {
                ResponseMessage = "Logged in successfully!",

                RoleCodes = [.. existingUser.Roles.Select(role => new UserRoleResponse
                {
                    RoleId = role.RoleId,
                    RoleCode = role.RoleCode,
                    RoleName = role.RoleName,
                    IsDefault = role.IsDefault
                })],

                Success = true,

                User = user,

                UserId = user.UserId,
                SessionToken = token,
                Email = user.Email!,
                Username = user.Username!,
                ExpireDate = expiryDate,
                Role = user.RoleName!,
                TimeStamp = timeStamp
            };
        }

        private void SetUserInCache(UserEntity existingUser, LoginResponse responseLogin, string password)
        {
            _caching.Set("Email", existingUser.Email, TimeSpan.FromDays(1));
            _caching.Set("PhoneNumber", existingUser.Email, TimeSpan.FromDays(1));
            _caching.Set("Token", responseLogin.SessionToken, TimeSpan.FromDays(1));
            _caching.Set("LoggedInUserId", existingUser.UserId, TimeSpan.FromDays(1));
            _caching.Set(responseLogin.SessionToken!, responseLogin, TimeSpan.FromDays(1));
            _caching.Set("Password", password, TimeSpan.FromDays(1));
        }
        #endregion
    }
}

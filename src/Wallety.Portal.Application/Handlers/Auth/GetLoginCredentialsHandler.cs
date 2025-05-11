using MediatR;
using Wallety.Portal.Application.Helpers;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Queries;
using Wallety.Portal.Application.Response.Auth;
using Wallety.Portal.Application.Response.User;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Services;

namespace Wallety.Portal.Application.Handlers.Auth
{
    public class GetLoginCredentialsHandler(
        ICachingInMemoryService caching,
        IUserRepository repository,
        IConfigurationService config) :
        IRequestHandler<GetLoginCredentialsQuery,
        LoginResponse>
    {
        private readonly IUserRepository _repository = repository;
        private readonly ICachingInMemoryService _caching = caching;
        private readonly IConfigurationService _config = config;

        public async Task<LoginResponse> Handle(GetLoginCredentialsQuery request, CancellationToken cancellationToken)
        {
            var token = _caching.Get<string>("Token");

            var loginResponse = _caching.Get<LoginResponse>(token)
                ?? throw new ArgumentException("The provided session token is invalid.");

            var user = await _repository.GetUserByEmail();

            loginResponse.User = LazyMapper.Mapper.Map<UserResponse>(user);
            loginResponse.RoleCodes = LazyMapper.Mapper.Map<List<UserRoleResponse>>(user?.Roles);
            loginResponse.Role = user?.RoleName;

            loginResponse.SessionToken = JwtTokenHelper.GenerateJwtToken(user!, user?.Roles.Where(r => r.IsDefault == true).FirstOrDefault()!, _config.SecretKey());

            return loginResponse;
        }
    }
}

using MediatR;
using Wallety.Portal.Application.Commands;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Response.General;
using Wallety.Portal.Core.Entity.User;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Services;

namespace Wallety.Portal.Application.Handlers.Auth
{
    public class LogoutQueryHandler(
        IUserRepository repository,
        ICachingInMemoryService caching) :
        IRequestHandler<CreateLogoutCommand, UpdateResponse>
    {
        private readonly IUserRepository _repository = repository;
        private readonly ICachingInMemoryService _caching = caching;

        public async Task<UpdateResponse> Handle(CreateLogoutCommand request, CancellationToken cancellationToken)
        {
            var item = new UserSessionEntity
            {
                SessionToken = _caching.Get<string?>("Token"),
                IsActive = false,
                IsAutoLogout = false
            };

            var response = await _repository.UpdateUserSession(item);

            if (!response.IsSuccess)
                throw new Exception(response.ResponseMessage);

            return LazyMapper.Mapper.Map<UpdateResponse>(response);

        }
    }
}

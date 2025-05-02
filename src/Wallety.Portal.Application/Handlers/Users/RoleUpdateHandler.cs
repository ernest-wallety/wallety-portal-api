using MediatR;
using Wallety.Portal.Application.Commands.General;
using Wallety.Portal.Application.Dto.User;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Response.General;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Requests.User;

namespace Wallety.Portal.Application.Handlers.Users
{
    public class RoleUpdateHandler(IUserRepository repository) :
        IRequestHandler<UpdateCommand<UserRoleChangeDTO, UpdateResponse>, UpdateResponse>
    {
        private readonly IUserRepository _repository = repository;

        public async Task<UpdateResponse> Handle(UpdateCommand<UserRoleChangeDTO, UpdateResponse> request, CancellationToken cancellationToken)
        {
            var dto = LazyMapper.Mapper.Map<UserRoleChangeModel>(request.Item);

            var response = await _repository.UpdateUserRole(dto);

            return LazyMapper.Mapper.Map<UpdateResponse>(response);
        }
    }
}

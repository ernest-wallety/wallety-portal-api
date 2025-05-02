using MediatR;
using Wallety.Portal.Application.Commands.General;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Response.General;
using Wallety.Portal.Core.Entity.User;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Results;

namespace Wallety.Portal.Application.Handlers.Users
{
    public class UpdateUserHandler(IUserRepository repository) : IRequestHandler<UpdateCommand<UserEntity, UpdateResponse>, UpdateResponse>
    {
        private readonly IUserRepository _repository = repository;

        public async Task<UpdateResponse> Handle(UpdateCommand<UserEntity, UpdateResponse> request, CancellationToken cancellationToken)
        {

            var result = (request.Item != null) ? await _repository.UpdateUser(request.Item) : new UpdateRecordResult() { };

            return LazyMapper.Mapper.Map<UpdateResponse>(result);
        }
    }
}

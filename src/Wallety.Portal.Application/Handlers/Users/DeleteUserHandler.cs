using MediatR;
using Wallety.Portal.Application.Commands.General;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Response.General;
using Wallety.Portal.Core.Entity.User;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Results;

namespace Wallety.Portal.Application.Handlers.Users
{
    public class DeleteUserHandler(
       IUserRepository repository) :
       IRequestHandler<DeleteCommand<UserEntity, DeleteResponse>, DeleteResponse>
    {
        private readonly IUserRepository _repository = repository;

        public async Task<DeleteResponse> Handle(DeleteCommand<UserEntity, DeleteResponse> request, CancellationToken cancellationToken)
        {
            var result = (request.Id != null && request.Id != 0) ? await _repository.DeleteUser((int)request.Id) : new DeleteRecordResult();

            return LazyMapper.Mapper.Map<DeleteResponse>(result);
        }
    }
}

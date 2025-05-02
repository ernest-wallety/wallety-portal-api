using MediatR;
using Wallety.Portal.Application.Commands.General;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Response.General;
using Wallety.Portal.Core.Entity.User;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Results;

namespace Wallety.Portal.Application.Handlers.Users
{
    public class CreateUserHandler(
        IUserRepository repository) :
        IRequestHandler<CreateCommand<UserEntity, CreateResponse>, CreateResponse>
    {
        private readonly IUserRepository _repository = repository;

        public async Task<CreateResponse> Handle(CreateCommand<UserEntity, CreateResponse> request, CancellationToken cancellationToken)
        {
            var result = (request.Item != null) ? await _repository.CreateUser(request.Item) : new CreateRecordResult();

            return LazyMapper.Mapper.Map<CreateResponse>(result);
        }
    }
}

using MediatR;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Queries.General;
using Wallety.Portal.Application.Response.User;
using Wallety.Portal.Core.Repository;

namespace Wallety.Portal.Application.Handlers.Users
{
    public class GetUserQueryHandler(IUserRepository repository) : IRequestHandler<ItemQuery<UserResponse>, UserResponse>
    {
        private readonly IUserRepository _repository = repository;

        public async Task<UserResponse> Handle(ItemQuery<UserResponse> request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserById(request.Id);

            return LazyMapper.Mapper.Map<UserResponse>(user);
        }
    }
}

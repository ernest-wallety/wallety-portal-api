using MediatR;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Queries.General;
using Wallety.Portal.Application.Response.User;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Application.Handlers.Users
{
    public class GetUsersHandler(
       IUserRepository repository) :
       IRequestHandler<ListQuery<UserResponse>, Pagination<UserResponse>>
    {
        private readonly IUserRepository _repository = repository;

        public async Task<Pagination<UserResponse>> Handle(ListQuery<UserResponse> request, CancellationToken cancellationToken)
        {
            var items = await _repository.GetUsers(request.Criteria);

            return LazyMapper.Mapper.Map<Pagination<UserResponse>>(items);
        }
    }
}

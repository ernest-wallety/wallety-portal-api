using System.Text.Json;
using MediatR;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Queries.General;
using Wallety.Portal.Application.Response.Customer;
using Wallety.Portal.Core.Entity.Customer;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Application.Handlers.Customer
{
    public class GetUnverifiedCustomersHandler(ICustomerRepository repository) :
        IRequestHandler<ListQuery<CustomerVerifyResponse>, Pagination<CustomerVerifyResponse>>
    {
        private readonly ICustomerRepository _repository = repository;

        public async Task<Pagination<CustomerVerifyResponse>> Handle(ListQuery<CustomerVerifyResponse> request, CancellationToken cancellationToken)
        {
            var items = await _repository.GetUnverifiedCustomers(request.Criteria);

            var mappedUsers = items.Items.Select(u =>
            {
                if (!string.IsNullOrEmpty(u.HistoryJson))
                    u.History = JsonSerializer.Deserialize<List<CustomerAccountVerificationHistoryEntity>>(u.HistoryJson) ?? [];

                return u;

            }).ToList();

            var response = LazyMapper.Mapper.Map<Pagination<CustomerVerifyResponse>>(items);

            return response;
        }
    }
}

using MediatR;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Queries.General;
using Wallety.Portal.Application.Response.Customer;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Application.Handlers.Customer
{
    public class GetCustomersHandler(ICustomerRepository repository) : IRequestHandler<ListStatsQuery<CustomersOverviewResponse>, CustomersOverviewResponse>
    {
        private readonly ICustomerRepository _repository = repository;

        public async Task<CustomersOverviewResponse> Handle(ListStatsQuery<CustomersOverviewResponse> request, CancellationToken cancellationToken)
        {
            var items = await _repository.GetCustomers(request.Criteria!);
            var response = LazyMapper.Mapper.Map<Pagination<CustomerResponse>>(items);

            // Calculate the total number of customers and their activity status
            var customers = await _repository.GetAllCustomers();
            int totalCustomers = customers.Items.Count;

            var totalPendingKYC = await _repository.GetRejectedUnverifiedCustomers();

            var activityCounts = new Dictionary<string, int>
            {
                { "Frequent", customers.Items.Count(x => x.LastActivityStatus == "Frequent") },
                { "Infrequent", customers.Items.Count(x => x.LastActivityStatus == "Infrequent") },
                { "Dormant", customers.Items.Count(x => x.LastActivityStatus == "Dormant") }
            };

            var activityPercentages = activityCounts.ToDictionary(
               kvp => kvp.Key,
               kvp => totalCustomers > 0 ? Math.Round((decimal)kvp.Value * 100m / totalCustomers, 2) : 0m
           );

            var activityData = new[]
                    {
                new
                {
                    Label = "Frequent Customers",
                    Count = activityCounts["Frequent"],
                    Percentage = activityPercentages["Frequent"]
                },
                new
                {
                    Label = "Infrequent Customers",
                    Count = activityCounts["Infrequent"],
                    Percentage = activityPercentages["Infrequent"]
                },
                new
                {
                    Label = "Dormant Customers",
                    Count = activityCounts["Dormant"],
                    Percentage = activityPercentages["Dormant"]
                }
            };

            return new CustomersOverviewResponse
            {
                Customers = response,
                TotalCustomers = totalCustomers,
                TotalBalance = customers.Items.Select(x => x.UsableBalance).Sum(),
                TotalPendingKYC = totalPendingKYC.Count,
                ActivityData = activityData
            };
        }
    }
}

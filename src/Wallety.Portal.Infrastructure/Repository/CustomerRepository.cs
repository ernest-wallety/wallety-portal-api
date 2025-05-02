using Wallety.Portal.Core.Entity.Customer;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Services;
using Wallety.Portal.Core.Specs;
using Wallety.Portal.Infrastructure.DbQueries;

namespace Wallety.Portal.Infrastructure.Repository
{
    public class CustomerRepository(IPgSqlSelector sqlContext) : ICustomerRepository
    {
        private readonly IPgSqlSelector _sqlContext = sqlContext;

        public async Task<Pagination<CustomerEntity>> GetCustomers(BaseListCriteria criteria)
        {
            var query = new UsersQuery(criteria).GetCustomerQuery();
            var items = await _sqlContext.SelectQuery<CustomerEntity>(query, criteria);

            return new Pagination<CustomerEntity>
            {
                PageIndex = criteria.PageIndex,
                PageSize = criteria.PageSize,
                Items = [.. items],
                Count = items.ToList().Count
            };
        }

        public async Task<Pagination<CustomerEntity>> GetAllCustomers()
        {
            var query = new UsersQuery(null).GetCustomerQuery();
            var items = await _sqlContext.SelectQuery<CustomerEntity>(query, null);

            return new Pagination<CustomerEntity>
            {
                Items = [.. items],
                Count = items.ToList().Count
            };
        }

        public async Task<Pagination<CustomerVerifyEntity>> GetUnverifiedCustomers(BaseListCriteria criteria)
        {
            var query = new CustomerVerificationQuery(criteria).Query();
            var items = await _sqlContext.SelectQuery<CustomerVerifyEntity>(query, criteria);

            return new Pagination<CustomerVerifyEntity>
            {
                PageIndex = criteria.PageIndex,
                PageSize = criteria.PageSize,
                Items = [.. items],
                Count = items.ToList().Count
            };
        }
    }
}

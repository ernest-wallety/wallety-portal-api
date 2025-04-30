using Wallety.Portal.Core.Entity.Customer;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Core.Repository
{
    public interface ICustomerRepository
    {
        Task<Pagination<CustomerEntity>> GetCustomers(BaseListCriteria criteria);
        Task<Pagination<CustomerVerifyEntity>> GetUnverifiedCustomers(BaseListCriteria criteria);
    }
}

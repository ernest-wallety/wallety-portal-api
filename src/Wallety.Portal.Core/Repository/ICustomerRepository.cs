using Wallety.Portal.Core.Entity.Customer;
using Wallety.Portal.Core.Requests.Customer;
using Wallety.Portal.Core.Results;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Core.Repository
{
    public interface ICustomerRepository
    {
        Task<Pagination<CustomerEntity>> GetCustomers(BaseListCriteria criteria);
        Task<Pagination<CustomerEntity>> GetAllCustomers();
        Task<Pagination<CustomerVerifyEntity>> GetUnverifiedCustomers(BaseListCriteria criteria);
        Task<CreateRecordResult> VerifyCustomerAccount(CustomerVerificationModel model);
    }
}

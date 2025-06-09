using Wallety.Portal.Core.Entity.Customer;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Requests.Customer;
using Wallety.Portal.Core.Results;
using Wallety.Portal.Core.Services;
using Wallety.Portal.Core.Specs;
using Wallety.Portal.Infrastructure.DbQueries;

namespace Wallety.Portal.Infrastructure.Repository
{
    public class CustomerRepository(
        IPgSqlSelector sqlContext,
        ICachingInMemoryService cachingInMemoryService) : ICustomerRepository
    {
        private readonly IPgSqlSelector _sqlContext = sqlContext;
        private readonly ICachingInMemoryService _cachingInMemoryService = cachingInMemoryService;

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
            var query = UsersQuery.GetCustomersQuery();
            var items = await _sqlContext.SelectQuery<CustomerEntity>(query, null);

            return new Pagination<CustomerEntity>
            {
                Items = [.. items],
                Count = items.ToList().Count
            };
        }

        public async Task<List<VerificationRejectReasonsEntity>> GetVerificationRejectReasons()
        {
            var query = CustomerVerificationQuery.GetVerificationReasonsQuery();
            var items = await _sqlContext.SelectQuery<VerificationRejectReasonsEntity>(query, null);

            return [.. items];
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

        public async Task<CreateRecordResult> VerifyCustomerAccount(CustomerVerificationModel model)
        {
            var parameters = new
            {
                p_result_message = default(string),
                p_is_error = default(bool),
                p_return_record_id = default(Guid),

                p_logged_in_user_id = _cachingInMemoryService.Get<string?>("LoggedInUserId"),
                p_customer_id = model.CustomerId,
                p_registration_status_id = Guid.Parse(model.RegistrationStatusId),
                p_verification_reject_reason_id = !string.IsNullOrEmpty(model.VerificationRejectReasonId) ? Guid.Parse(model.VerificationRejectReasonId) : (Guid?)null
            };

            var result = await _sqlContext.ExecuteStoredProcedureAsync<dynamic>(
                "customer_verify_account_insert",
                parameters
            );

            if (result?.p_is_error == true) return CreateRecordResult.Error(result?.p_result_message);

            return CreateRecordResult.Successs(result!.p_return_record_id, result!.p_result_message);
        }

        public async Task<List<CustomerVerifyEntity>> GetRejectedUnverifiedCustomers()
        {
            var query = CustomerVerificationQuery.GetRejectedUnverifiedCustomers();
            var items = await _sqlContext.SelectQuery<CustomerVerifyEntity>(query, null);

            return [.. items];
        }

        public async Task<UpdateRecordResult> UpdateFreezeAccount(Guid id)
        {
            var query = CustomerVerificationQuery.UpdateFreezeAccount();

            var result = await _sqlContext.ExecuteAsyncQuery(query, new { UserId = id });

            if (result == false)
                return UpdateRecordResult.Error("Failed to update freeze account status.");

            return UpdateRecordResult.Successs("Account freeze status updated successfully.");
        }
    }
}

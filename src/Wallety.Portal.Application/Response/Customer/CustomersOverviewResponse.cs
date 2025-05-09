using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Application.Response.Customer
{
    public class CustomersOverviewResponse
    {
        public Pagination<CustomerResponse> Customers { get; set; }
        public long TotalCustomers { get; set; }
        public decimal TotalBalance { get; set; }
        public int TotalPendingKYC { get; set; }
        public object ActivityData { get; set; }
    }
}

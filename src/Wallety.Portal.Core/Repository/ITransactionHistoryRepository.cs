using Wallety.Portal.Core.Entity;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Core.Repository
{
    public interface ITransactionHistoryRepository
    {
        Task<Pagination<TransactionHistoryEntity>> GetTransactions(BaseListCriteria criteria);
        Task<TransactionHistoryEntity> GetTransactionByReferenceQuery(string reference);
    }
}

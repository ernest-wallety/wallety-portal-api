using Wallety.Portal.Core.Entity;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Core.Repository
{
    public interface ITransactionHistoryRepository
    {
        Task<Pagination<TransactionHistoryEntity>> GetAllTransactions(BaseListCriteria criteria);
        Task<TransactionHistoryEntity> GetTransactionByReferenceQuery(string reference);
    }
}

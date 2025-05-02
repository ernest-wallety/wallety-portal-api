using Wallety.Portal.Core.Entity;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Services;
using Wallety.Portal.Core.Specs;
using Wallety.Portal.Infrastructure.DbQueries;

namespace Wallety.Portal.Infrastructure.Repository
{
    public class TransactionHistoryRepository(IPgSqlSelector sqlContext) : ITransactionHistoryRepository
    {
        private readonly IPgSqlSelector _sqlContext = sqlContext;

        public async Task<Pagination<TransactionHistoryEntity>> GetTransactions(BaseListCriteria criteria)
        {
            var query = new TransactionHistoryQuery(criteria).Query();

            var items = await _sqlContext.SelectQuery<TransactionHistoryEntity>(query, criteria);

            return new Pagination<TransactionHistoryEntity>
            {
                PageIndex = criteria.PageIndex,
                PageSize = criteria.PageSize,
                Items = [.. items],
                Count = items.ToList().Count
            };
        }

        public async Task<TransactionHistoryEntity> GetTransactionByReferenceQuery(string reference)
        {
            var query = new TransactionHistoryQuery(new BaseListCriteria()).GetTransactionByReferenceQuery();

            var item = await _sqlContext.SelectFirstOrDefaultQuery<TransactionHistoryEntity>(query, new { TransactionReference = reference });

            return item!;
        }
    }
}

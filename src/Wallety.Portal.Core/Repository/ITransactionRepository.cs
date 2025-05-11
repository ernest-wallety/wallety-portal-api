using Wallety.Portal.Core.Entity;
using Wallety.Portal.Core.Entity.User;
using Wallety.Portal.Core.Results;

namespace Wallety.Portal.Core.Repository
{
    public interface ITransactionRepository
    {
        Task<TransactionHistoryEntity> CreateCreditWalletTransaction(UserSessionEntity sessionEntity, UserEntity userEntity, decimal amount);

        Task<TransactionHistoryEntity> UpdateTransactionPayment(TransactionHistoryEntity TransactionHistoryId, UserSessionEntity sessionEntity);

        Task<WalletTransactionEntity> CreateWalletTransaction();

    }
}

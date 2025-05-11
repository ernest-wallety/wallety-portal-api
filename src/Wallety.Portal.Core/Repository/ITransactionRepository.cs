using Wallety.Portal.Core.Entity.User;
using Wallety.Portal.Core.Results;

namespace Wallety.Portal.Core.Repository
{
    public interface ITransactionRepository
    {
        Task<CreateRecordResult> CreateCreditWalletTransaction(UserSessionEntity sessionEntity, UserEntity userEntity, decimal amount);

    }
}

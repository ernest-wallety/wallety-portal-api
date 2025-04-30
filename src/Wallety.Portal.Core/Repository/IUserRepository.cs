using Wallety.Portal.Core.Entity.User;
using Wallety.Portal.Core.Requests.User;
using Wallety.Portal.Core.Results;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Core.Repository
{
    public interface IUserRepository
    {
        Task<Pagination<UserEntity>> GetAllUsers(BaseListCriteria criteria);

        Task<UserEntity?> GetUserById(Guid id);
        Task<UserEntity?> GetUserByEmail();
        Task<UserEntity?> GetUserByCellNumber();

        Task<DeleteRecordResult> DeleteUser(int id);

        Task<CreateRecordResult> CreateUser(UserEntity entity);

        Task<UpdateRecordResult> UpdateUser(UserEntity entity);
        Task<UpdateRecordResult> SoftDeleteUser(UserEntity entity);
        Task<UpdateRecordResult> RestoreUser(UserEntity entity);

        Task<UpdateRecordResult> UpdatePassword(PasswordResetModel model);
        Task<UpdateRecordResult> UpdateUserRole(UserRoleChangeModel model);

        Task<UpdateRecordResult> UpdateDefaultRole(string role);

        Task<UserEntity> GetUserRolesAndDefaultRoleAsync();
    }
}

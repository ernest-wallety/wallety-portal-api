using Wallety.Portal.Core.Entity.User;
using Wallety.Portal.Core.Requests.User;
using Wallety.Portal.Core.Results;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Core.Repository
{
    public interface IUserRepository
    {
        Task<Pagination<UserEntity>> GetUsers(BaseListCriteria criteria);

        Task<UserEntity?> GetUserById(Guid id);
        Task<UserEntity?> GetUserByEmail(string? email = null);
        Task<UserEntity?> GetUserByCellNumber();
        Task<DataList<UserRoleEntity>> GetUserRoles(string? id = null);
        Task<DataList<UserRoleEntity>> GetRoles();
        Task<DataList<UserSessionEntity>> GetUserSession(string id);

        Task<DeleteRecordResult> DeleteUser(int id);

        Task<CreateRecordResult> CreateUser(UserEntity entity);
        Task<CreateRecordResult> CreateUserSession(UserSessionEntity entity);
        Task<UpdateRecordResult> UpdateUserSession(UserSessionEntity entity);

        Task<UpdateRecordResult> UpdateUser(UserEntity entity);
        Task<UpdateRecordResult> SoftDeleteUser(UserEntity entity);
        Task<UpdateRecordResult> RestoreUser(UserEntity entity);

        Task<UpdateRecordResult> UpdatePassword(PasswordResetModel model);
        Task<UpdateRecordResult> UpdateUserRole(UserRoleChangeModel model);

        Task<UpdateRecordResult> UpdateDefaultRole(string roleId, string userId);
    }
}

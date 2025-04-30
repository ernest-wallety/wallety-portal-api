using System.Text.Json;
using Wallety.Portal.Core.Entity.User;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Requests.User;
using Wallety.Portal.Core.Results;
using Wallety.Portal.Core.Services;
using Wallety.Portal.Core.Specs;
using Wallety.Portal.Infrastructure.DbQueries;

namespace Wallety.Portal.Infrastructure.Repository
{
    public class UserRepository(
        IPgSqlSelector sqlContext,
        ICachingInMemoryService cachingInMemoryService) : IUserRepository
    {
        private readonly IPgSqlSelector _sqlContext = sqlContext;
        private readonly ICachingInMemoryService _cachingInMemoryService = cachingInMemoryService;

        public async Task<Pagination<UserEntity>> GetAllUsers(BaseListCriteria criteria)
        {
            var query = new UsersQuery(criteria).Query();
            var items = await _sqlContext.SelectQuery<UserEntity>(query, criteria);

            return new Pagination<UserEntity>
            {
                PageIndex = criteria.PageIndex,
                PageSize = criteria.PageSize,
                Items = [.. items],
                Count = items.ToList().Count
            };
        }

        public Task<UserEntity?> GetUserById(Guid id) => GetUserAsync(UsersQuery.GetUserByIdQuery(), new { Id = id.ToString() });

        public Task<UserEntity?> GetUserByCellNumber() => GetUserAsync(UsersQuery.GetUserByCellNumberQuery(), new { PhoneNumber = _cachingInMemoryService.Get<string>("PhoneNumber") });

        public Task<UserEntity?> GetUserByEmail() => GetUserAsync(UsersQuery.GetUserByEmailQuery(), new { Email = _cachingInMemoryService.Get<string>("Email") });

        public Task<DeleteRecordResult> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CreateRecordResult> CreateUser(UserEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateRecordResult> UpdateUser(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateRecordResult> SoftDeleteUser(UserEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateRecordResult> RestoreUser(UserEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateRecordResult> UpdatePassword(PasswordResetModel model)
        {
            var parameters = new
            {
                p_result_message = default(string),
                p_email = model.Email,
                p_new_password = model.NewPassword,
                p_user_id = model.UserId,
                p_otp_guid = model.OneTimePasswordGuid
            };

            var result = await _sqlContext.ExecuteStoredProcedureAsync<dynamic>(
                "update_user_password_and_otp",
                parameters
            );

            return UpdateRecordResult.Successs(result?.p_result_message);
        }

        public async Task<UpdateRecordResult> UpdateUserRole(UserRoleChangeModel model)
        {
            var parameters = new
            {
                p_result_message = default(string),

                p_name = model.Name,
                p_surname = model.Surname,
                p_email = model.Email,
                p_phone_number = model.PhoneNumber,
                p_role_code = model.RoleCode
            };

            var result = await _sqlContext.ExecuteStoredProcedureAsync<dynamic>(
                "user_role_update",
                parameters
            );

            return UpdateRecordResult.Successs(result?.p_result_message);
        }

        public async Task<UpdateRecordResult> UpdateDefaultRole(string roleName)
        {
            var userId = _cachingInMemoryService.Get<string>("UserId");

            var parameters = new
            {
                p_result_message = default(string),

                p_role_name = roleName,
                p_user_id = userId
            };

            var result = await _sqlContext.ExecuteStoredProcedureAsync<dynamic>(
               "default_role_update",
               parameters
           );

            return UpdateRecordResult.Successs(result?.p_result_message);
        }

        private async Task<UserEntity?> GetUserAsync(string query, object parameters)
        {
            var item = await _sqlContext.SelectFirstOrDefaultQuery<UserEntity>(query, parameters);

            if (item != null && !string.IsNullOrEmpty(item.UserRolesJson))
            {
                item.Roles = JsonSerializer.Deserialize<List<UserRoleEntity>>(item.UserRolesJson)!;
            }

            return item;
        }
    }
}

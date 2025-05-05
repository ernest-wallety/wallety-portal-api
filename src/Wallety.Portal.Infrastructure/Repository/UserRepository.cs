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

        public async Task<Pagination<UserEntity>> GetUsers(BaseListCriteria criteria)
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

        public Task<UserEntity?> GetUserByEmail(string? email) => GetUserAsync(UsersQuery.GetUserByEmailQuery(), new { Email = _cachingInMemoryService.Get<string>("Email") ?? email });

        public Task<UserEntity?> GetUserByCellNumber() => GetUserAsync(UsersQuery.GetUserByCellNumberQuery(), new { PhoneNumber = _cachingInMemoryService.Get<string>("PhoneNumber") });

        public async Task<DataList<UserRoleEntity>> GetUserRoles(string? id)
        {
            var query = UsersQuery.GetUserRolesQuery();
            var items = await _sqlContext.SelectQuery<UserRoleEntity>(query, new { UserId = _cachingInMemoryService.Get<string?>("LoggedInUserId") ?? id });

            return new DataList<UserRoleEntity> { Items = [.. items], Count = items.Count };
        }

        public async Task<DataList<UserRoleEntity>> GetRoles()
        {
            var query = UsersQuery.GetRolesQuery();
            var items = await _sqlContext.SelectQuery<UserRoleEntity>(query, null);

            return new DataList<UserRoleEntity> { Items = [.. items], Count = items.Count };

        }

        public async Task<DataList<UserSessionEntity>> GetUserSession(string id)
        {
            var query = UsersQuery.GetUserSessionQuery();
            var items = await _sqlContext.SelectQuery<UserSessionEntity>(query, new { UserId = _cachingInMemoryService.Get<string?>("LoggedInUserId") ?? id });

            return new DataList<UserSessionEntity> { Items = [.. items], Count = items.Count };
        }

        public Task<DeleteRecordResult> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateRecordResult> CreateUser(UserEntity entity)
        {
            var parameters = new
            {
                p_result_message = default(string),
                p_is_error = default(bool),
                p_return_record_id = default(Guid),

                p_name = entity.FirstName,
                p_surname = entity.Surname,
                p_phone_number = entity.PhoneNumber,
                p_phone_number_confirmed = entity.PhoneNumberConfirmed,
                p_username = entity.Username,
                p_email = entity.Email,
                p_password_hash = entity.PasswordHash,
                p_security_stamp = entity.SecurityStamp,
                p_role_id = entity.RoleId,
            };

            var result = await _sqlContext.ExecuteStoredProcedureAsync<dynamic>(
                "user_insert",
                parameters
            );

            if (result?.p_is_error == true) return CreateRecordResult.Error(result?.p_result_message);

            return CreateRecordResult.Successs(result!.p_return_record_id, result!.p_result_message);
        }

        public async Task<CreateRecordResult> CreateUserSession(UserSessionEntity entity)
        {
            var parameters = new
            {
                p_result_message = default(string),
                p_is_error = default(bool),
                p_return_record_id = default(Guid),

                p_session_token = entity.SessionToken,
                p_user_id = entity.UserId
            };

            var result = await _sqlContext.ExecuteStoredProcedureAsync<dynamic>(
                "user_session_insert",
                parameters
            );

            if (result?.p_is_error == true) return CreateRecordResult.Error(result?.p_result_message);

            return CreateRecordResult.Successs(result!.p_return_record_id, result!.p_result_message);
        }

        public async Task<UpdateRecordResult> UpdateUserSession(UserSessionEntity entity)
        {
            var parameters = new
            {
                p_result_message = default(string),
                p_is_error = default(bool),

                p_user_id = entity.SessionToken,
                p_is_auto_logout = entity.IsAutoLogout
            };

            var result = await _sqlContext.ExecuteStoredProcedureAsync<dynamic>(
                "user_session_update",
                parameters
            );

            if (result?.p_is_error == true) return UpdateRecordResult.Error(result?.p_result_message);

            return UpdateRecordResult.Successs(result?.p_result_message);
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
                p_is_error = default(bool),
                p_return_record_id = default(Guid),

                p_email = model.Email,
                p_new_password = model.NewPassword,
                p_user_id = model.UserId,
                p_otp_guid = model.OneTimePasswordGuid
            };

            var result = await _sqlContext.ExecuteStoredProcedureAsync<dynamic>(
                "user_password_and_otp_upsert",
                parameters
            );

            if (result?.p_is_error == true) return UpdateRecordResult.Error(result?.p_result_message);

            return UpdateRecordResult.Successs(result?.p_result_message);
        }

        public async Task<UpdateRecordResult> UpdateUserRole(UserRoleUpdateModel model)
        {
            var parameters = new
            {
                p_result_message = default(string),
                p_is_error = default(bool),

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

            if (result?.p_is_error == true) return UpdateRecordResult.Error(result?.p_result_message);

            return UpdateRecordResult.Successs(result?.p_result_message);
        }

        public async Task<UpdateRecordResult> UpdateDefaultRole(string roleId, string userId)
        {
            var parameters = new
            {
                p_result_message = default(string),
                p_is_error = default(bool),
                p_role_id = roleId,
                p_user_id = userId
            };

            var result = await _sqlContext.ExecuteStoredProcedureAsync<dynamic>(
               "default_role_update",
               parameters
           );

            if (result?.p_is_error == true) return UpdateRecordResult.Error(result?.p_result_message);

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

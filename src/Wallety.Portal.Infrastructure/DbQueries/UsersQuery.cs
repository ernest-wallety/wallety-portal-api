using Wallety.Portal.Core.Helpers;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Infrastructure.DbQueries
{
    public class UsersQuery : QueryBase
    {
        private readonly BaseListCriteria _criteria = new();

        public UsersQuery(BaseListCriteria criteria) : base(criteria)
        {
            if (!string.IsNullOrEmpty(criteria.SortField))
            {
                if (criteria.SortField == "1")
                    DefaultSortField = @"u.""Name""";
                else
                    DefaultSortField = $"{criteria.SortField.ReplaceSingleWithDoubleQuotes()}";
            }

            ApplySortExpression(criteria.SortAscending);

            _criteria = criteria;
        }

        public override string Query()
        {
            SetWhereClause(_criteria);

            var query = $@"

                {MainSelect()}

                WHERE 1=1 {_whereClause}        

                GROUP BY 
                    u.""Id"", 
                    c.""CountryId"", 
                    us.""Id"", 
                    us.""LastActiveTime"", 
                    role_info.""Id"", 
                    role_info.""Name"",
                    ua.""Id"",
                    w.""WalletId"",
                    t.""TransactionId"",
                    t.""TransactionDate""
                
                ORDER BY {_sortExpression}

                LIMIT {_criteria.PageSize} OFFSET {(_criteria.PageIndex - 1) * _criteria.PageSize}

            ";

            return query;
        }

        protected override void SetWhereClause(BaseListCriteria criteria)
        {
            // Call the base method to include default conditions
            base.SetWhereClause(criteria);

            // Append additional conditions specific to UserQuery
            if (!string.IsNullOrEmpty(criteria.Search))
            {
                _whereClause += @$" AND ( 
                    u.""Name"" ILIKE '%{criteria.Search}%'
                    OR u.""Surname"" ILIKE '%{criteria.Search}%'
                    OR u.""PhoneNumber"" ILIKE '%{criteria.Search}%'
                    OR (u.""Name"" || ' ' || u.""Surname"") ILIKE '%{criteria.Search}%'
                )";
            }
        }

        #region Select Queries
        public string GetCustomerQuery()
        {
            SetWhereClause(_criteria);

            var query = $@"

                {MainSelect()}

                WHERE 1=1 AND r.""RoleCode"" = 'WR03' {_whereClause}        

                GROUP BY 
                    u.""Id"", 
                    c.""CountryId"", 
                    us.""Id"", 
                    us.""LastActiveTime"", 
                    role_info.""Id"", 
                    role_info.""Name"",
                    ua.""Id"",
                    w.""WalletId"",
                    t.""TransactionId"",
                    t.""TransactionDate""
                
                ORDER BY {_sortExpression}

                LIMIT {_criteria.PageSize} OFFSET {(_criteria.PageIndex - 1) * _criteria.PageSize}
            ";

            return query;
        }

        public static string GetUserByEmailQuery()
        {
            var query = $@"

                {MainSelect()} 

                WHERE u.""Email"" = @Email

                GROUP BY 
                    u.""Id"", 
                    c.""CountryId"", 
                    us.""Id"", 
                    us.""LastActiveTime"", 
                    role_info.""Id"", 
                    role_info.""Name"",
                    ua.""Id"",
                    w.""WalletId"",
                    t.""TransactionId"",
                    t.""TransactionDate"";
            ";

            return query;
        }

        public static string GetUserByCellNumberQuery()
        {
            var query = $@"

                {MainSelect()}
                
                WHERE u.""PhoneNumber"" = @PhoneNumber

                GROUP BY 
                    u.""Id"", 
                    c.""CountryId"", 
                    us.""Id"", 
                    us.""LastActiveTime"", 
                    role_info.""Id"", 
                    role_info.""Name"",
                    ua.""Id"",
                    w.""WalletId"",
                    t.""TransactionId"",
                    t.""TransactionDate"";
            ";

            return query;
        }

        public static string GetUserByIdQuery()
        {
            var query = $@"

                {MainSelect()} 
                
                WHERE u.""Id"" = @Id

                GROUP BY 
                    u.""Id"", 
                    c.""CountryId"", 
                    us.""Id"", 
                    us.""LastActiveTime"", 
                    role_info.""Id"", 
                    role_info.""Name"",
                    ua.""Id"",
                    w.""WalletId"",
                    t.""TransactionId"",
                    t.""TransactionDate"";
            ";

            return query;
        }

        private static string MainSelect()
        {
            var query = $@"

                SELECT 
                
                    u.""Id"" AS UserId,
                    u.""Name"" AS FirstName,
                    u.""Surname"",
                    u.""IdNumber"",
                    u.""PassportNumber"",
                    u.""AccountNumber"",
                    u.""AccountCreationDate"",
                    u.""IsAccountActive"",
                    u.""VerifyAttempts"",
                    u.""IsFrozen"",
                    u.""IsVerified"",
                    u.""CountryId"",
                    u.""RegistrationStatusId"",
                    u.""UserName"",
                    u.""NormalizedUserName"",
                    u.""Email"",
                    u.""NormalizedEmail"",
                    u.""EmailConfirmed"",
                    u.""PasswordHash"",
                    u.""SecurityStamp"",
                    u.""ConcurrencyStamp"",
                    u.""PhoneNumber"",
                    u.""PhoneNumberConfirmed"",
                    u.""TwoFactorEnabled"",
                    u.""LockoutEnd"",
                    u.""LockoutEnabled"",
                    u.""AccessFailedCount"",
                    u.""IdentityImage"",
                    u.""WalletId"",
                    u.""PanicCode"",
                    u.""DisplayNumber"",

                    -- Select role information with priority for default role
                    role_info.""Id"" AS RoleId,
                    role_info.""Name"" AS RoleName,

                    -- Aggregate roles for user into JSON
                    COALESCE(
                        json_agg(
                            DISTINCT jsonb_build_object(
                                'RoleId', r.""Id"",
                                'RoleName', r.""Name"",
                                'RoleCode', r.""RoleCode"",
                                'IsDefault', ur.""IsDefault""
                            )
                        ) FILTER (WHERE r.""Id"" IS NOT NULL), NULL
                    ) AS UserRolesJson,

                    c.""CountryName"",
                    c.""Alpha3Code"",
                    c.""MobileCode"",
                    c.""MobileRegex"",
                    c.""TimeZone"",

                    us.""LastActiveTime"",

                    ua.""OneTimePasswordGuid"",
                    ua.""ProfileImage"",

                    -- Customer Fields
                    w.""WalletId"",
                    w.""UsableBalance"",

                    t.""TransactionId"",
                    t.""TransactionDate"",
                    get_date_description(t.""TransactionDate""::TIMESTAMP) AS LastActivityDescription,
                    CASE 
                        WHEN t.""TransactionDate"" IS NULL THEN 'bg-prog-1'
                        WHEN AGE(NOW(), t.""TransactionDate"") <= INTERVAL '14 days' THEN 'bg-prog-4'  -- Frequent
                        WHEN AGE(NOW(), t.""TransactionDate"") > INTERVAL '14 days' AND AGE(NOW(), t.""TransactionDate"") < INTERVAL '1 month' THEN 'bg-prog-2'  -- Infrequent
                        WHEN AGE(NOW(), t.""TransactionDate"") >= INTERVAL '1 month' THEN 'bg-prog-1'  -- Dormant
                        ELSE NULL
                    END AS LastActivityColour,
                    CASE 
                        WHEN t.""TransactionDate"" IS NULL THEN 'Dormant'
                        WHEN AGE(NOW(), t.""TransactionDate"") <= INTERVAL '14 days' THEN 'Frequent'
                        WHEN AGE(NOW(), t.""TransactionDate"") > INTERVAL '14 days' AND AGE(NOW(), t.""TransactionDate"") < INTERVAL '1 month' THEN 'Infrequent'
                        WHEN AGE(NOW(), t.""TransactionDate"") >= INTERVAL '1 month' THEN 'Dormant'
                        ELSE NULL
                    END AS LastActivityStatus

                FROM ""AspNetUsers"" u

                LEFT JOIN ""AspNetUserRoles"" ur ON u.""Id"" = ur.""UserId""
                LEFT JOIN ""AspNetRoles"" r ON ur.""RoleId"" = r.""Id""
                LEFT JOIN ""Countries"" c ON u.""CountryId"" = c.""CountryId""
                LEFT JOIN ""UserAuth"" ua ON u.""Id"" = ua.""WalletyUserId""
                LEFT JOIN ""Wallets"" w ON u.""WalletId"" = w.""WalletId""

                -- LATERAL JOIN to get the latest session
                LEFT JOIN LATERAL(
                    SELECT us.""Id"", us.""LastActiveTime""
                    FROM ""UserSessions"" us
                    WHERE us.""WalletyUserId"" = u.""Id""
                    ORDER BY us.""LastActiveTime"" DESC
                    LIMIT 1
                ) us ON TRUE

                -- LATERAL JOIN to select the default role(or the first role if none are default)
                LEFT JOIN LATERAL(
                    SELECT r.""Id"", r.""Name""
                    FROM ""AspNetUserRoles"" ur
                    JOIN ""AspNetRoles"" r ON ur.""RoleId"" = r.""Id""
                    WHERE ur.""UserId"" = u.""Id""
                    ORDER BY ur.""IsDefault"" DESC, r.""Id"" ASC-- Prioritize default role, fallback to first role
                    LIMIT 1
                ) role_info ON TRUE

                -- LATERAL JOIN to get the latest user transaction
                LEFT JOIN LATERAL(
                    SELECT tr.""TransactionId"", tr.""TransactionDate""
                    FROM ""WalletTransactions"" tr
                    WHERE tr.""WalletyUserId"" = u.""Id""
                    ORDER BY tr.""TransactionDate"" DESC
                    LIMIT 1
                ) t ON TRUE
            ";

            return query;
        }
        #endregion
    }
}

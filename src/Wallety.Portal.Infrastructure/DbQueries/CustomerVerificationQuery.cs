using Wallety.Portal.Core.Helpers;
using Wallety.Portal.Core.Helpers.Constants;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Infrastructure.DbQueries
{
    public class CustomerVerificationQuery : QueryBase
    {
        private readonly BaseListCriteria _userCriteria = new();

        public CustomerVerificationQuery(BaseListCriteria criteria) : base(criteria)
        {
            if (!string.IsNullOrEmpty(criteria.SortField))
            {
                if (criteria.SortField == "1")
                    DefaultSortField = @"u.""AccountCreationDate""";
                else
                    DefaultSortField = $"{criteria.SortField.ReplaceSingleWithDoubleQuotes()}";
            }

            ApplySortExpression(criteria.SortAscending);

            _userCriteria = criteria;
        }

        public override string Query()
        {
            SetWhereClause(_userCriteria);

            var query = $@"

                {MainSelect()}

                {_whereClause} 
                
                AND rs.""RegistrationStatusId"" NOT IN (
                    '{RegistrationStatusConstants.REJECTED}'
                )

                AND (u.""IdentityImage"" IS NOT NULL AND u.""IdentityImage"" != '')

                GROUP BY u.""Id"", c.""CountryId"", rs.""RegistrationStatusId""
                
                ORDER BY {_sortExpression}

                LIMIT {_userCriteria.PageSize} OFFSET {(_userCriteria.PageIndex - 1) * _userCriteria.PageSize}

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
        private static string MainSelect()
        {
            var query = $@"

                SELECT 

                    u.""Id"" AS CustomerId,
                    u.""Name"",
                    u.""Surname"",
                    u.""PhoneNumber"",
                    
                    c.""CountryId"",
                    c.""CountryName"" AS Country,

                    u.""PassportNumber"",
                    u.""IdNumber"",
                    u.""AccountCreationDate"",

                    MIN(h.""VerificationHistoryId""::TEXT) AS VerificationHistoryId,
                    MIN(h.""CustomerServiceAgent"") AS CustomerServiceAgent,

                    rs.""RegistrationStatusId"",
                    rs.""Status"" AS RegistrationStatus,

                    COALESCE(
                        json_agg(
                            DISTINCT jsonb_build_object(
                                'VerificationHistoryId', h.""VerificationHistoryId"",
                                'Status', rs.""Status"",
                                'RejectionReason', re.""RejectMessage"",
                                'UpdatedBy', h.""CustomerServiceAgent"",
                                'Date', h.""VerificationDate""
                            )
                        ) FILTER (WHERE h.""VerificationHistoryId"" IS NOT NULL), NULL
                    ) AS HistoryJson,

                    u.""IdentityImage""

                FROM ""AspNetUsers"" u

                LEFT JOIN ""Countries"" c ON u.""CountryId"" = c.""CountryId""
                LEFT JOIN ""CustomerRegistrationVerificationHistory"" h ON u.""Id"" = h.""WalletyUserId""
                LEFT JOIN ""RegistrationStatuses"" rs ON u.""RegistrationStatusId"" = rs.""RegistrationStatusId""
                LEFT JOIN ""VerificationRejectReasons"" re ON h.""VerificationRejectReasonId"" = re.""RejectReasonId""

                WHERE NOT u.""IsVerified""

                AND EXISTS (
                    SELECT 1  FROM ""AspNetUserRoles"" ur
                    LEFT JOIN ""AspNetRoles"" r ON ur.""RoleId"" = r.""Id""
                    WHERE 
                        ur.""UserId"" = u.""Id"" AND 
                        r.""Id"" = '{RoleConstants.CUSTOMER}' -- We only need customers
                )

            ";

            return query;
        }

        public static string GetUnverifiedCustomersQuery()
        {
            return $@"
                SELECT * FROM ""VerificationRejectReasons""
            ";
        }
        #endregion
    }
}

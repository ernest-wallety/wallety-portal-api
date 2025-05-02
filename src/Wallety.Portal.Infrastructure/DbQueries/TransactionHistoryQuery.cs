using Wallety.Portal.Core.Helpers;
using Wallety.Portal.Core.Helpers.Constants;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Infrastructure.DbQueries
{
    public class TransactionHistoryQuery : QueryBase
    {
        private readonly BaseListCriteria _criteria = new();

        public TransactionHistoryQuery(BaseListCriteria criteria) : base(criteria)
        {
            DefaultSortField = @"x.""TransactionDate""";

            if (!string.IsNullOrEmpty(criteria.SortField))
            {
                if (criteria.SortField == "1")
                    DefaultSortField = @"x.""TransactionDate""";
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

                {RankedTransactions()}

                {ListSelect()}

                WHERE x.rn = 1 -- Select only the latest record for each TransactionReference 
                
                {_whereClause}

                ORDER BY {_sortExpression} 
                
                LIMIT {_criteria.PageSize} OFFSET {(_criteria.PageIndex - 1) * _criteria.PageSize}

            ";

            return query;
        }

        #region Select Queries
        public string GetTransactionByReferenceQuery()
        {
            var query = $@"

                {RankedTransactions()}

                {GroupSelect()} 

                WHERE x.""TransactionReference"" = @TransactionReference

                GROUP BY x.""TransactionReference""
            ";

            return query;
        }
        #endregion

        protected override void SetWhereClause(BaseListCriteria criteria)
        {
            // Call the base method to include default conditions
            base.SetWhereClause(criteria);

            // Append additional conditions specific to UserQuery
            if (!string.IsNullOrEmpty(criteria.Search))
            {
                _whereClause += @$" AND ( 
                    x.""TransactionReference"" ILIKE '%{criteria.Search}%'
                    OR x.FirstName ILIKE '%{criteria.Search}%'
                    OR x.""Surname"" ILIKE '%{criteria.Search}%'
                    OR (x.FirstName || ' ' || x.""Surname"") ILIKE '%{criteria.Search}%'
                )";
            }
        }

        #region Helpers
        private static string ListSelect()
        {
            var query = $@"
                
                -- Select only the latest record for each TransactionReference (rank = 1)
                SELECT

                    x.TransactionHistoryId,
                    x.""TransactionDate"",
                    x.""TransactionReference"",
                    x.""UserSessionId"",
                    x.UserId,
                    x.FirstName,
                    x.""Surname"",
                    x.""Email"",
                    x.""Phonber"",
                    x.""Amount"",
                    x.""TransactionTypeId"",
                    x.""ProductId"",
                    x.ProductIconPath,
                    x.""BeneficiaryId"",

                    -- Transaction Description
                    CASE 
                        WHEN x.""TransactionTypeId"" in ('{TransactionTypeConstants.PURCHASE}', '{TransactionTypeConstants.PAY_FOR_ME}')
                        THEN 'Purchase of ' || COALESCE(x.""ProductDisplayName"", x.""ProductName"", '')

                        WHEN x.""TransactionTypeId"" in ('{TransactionTypeConstants.PLEASE_PAY_ME}', '{TransactionTypeConstants.TOP_ME_UP}')
                        THEN 'Request of money from ' || COALESCE(x.""PayerWhatsappNumber"", x.""Phonber"", '')

                        WHEN x.""TransactionTypeId"" = '{TransactionTypeConstants.WALLETY_DEPOSIT}'
                        THEN 'Top up of account balance' 
                        
                        WHEN x.""TransactionTypeId"" = '{TransactionTypeConstants.MONEY_TRANSFER}'
                        THEN 'Transfer of money to ' ||  COALESCE(x.""OnceOffBeneficiaryName"", x.""BeneficiaryName"") || ' (' || COALESCE(x.""OnceOffBeneficiaryWhatsappNumber"", x.""OnceOffBeneficiaryDisplayNumber"", x.""BeneficiaryWhatsappNumber"", '') || ')'

                        WHEN x.""TransactionTypeId"" = '{TransactionTypeConstants.CREDIT_WALLET}'
                        THEN 'Credit of account balance for ' || COALESCE(x.""OnceOffBeneficiaryWhatsappNumber"", x.""OnceOffBeneficiaryDisplayNumber"", '')

                    END AS TransactionDescription,

                    x.""WalletyMeRequestId"",
                    x.""TransactionProcessStatusId"",
                    x.TransactionStatus

                FROM RankedTransactions x
            ";

            return query;
        }

        private static string GroupSelect()
        {
            var query = $@"
                
                SELECT

                    x.""TransactionReference"",

                    COALESCE(
                        json_agg(
                            DISTINCT jsonb_build_object(
                                'TransactionHistoryId', x.TransactionHistoryId,                                

                                'TransactionReference', x.""TransactionReference"",

                                'UserSessionId', x.""UserSessionId"",
                                'UserId', x.UserId,
                                'FirstName', x.FirstName,
                                'Surname', x.""Surname"",
                                'Email', x.""Email"",
                                'UserName', x.""UserName"",
                                'Phonber', x.""Phonber"",

                                'Amount', x.""Amount"",

                                'TransactionTypeId', x.""TransactionTypeId"",
                                'TransactionTypeName', x.TransactionTypeName,

                                'PaymentOptionId', x.""PaymentOptionId"",
                                'PaymentOptionName', x.PaymentOptionName,
                                'PaymentOptionIconPath', x.PaymentOptionIconPath,

                                'ProductId', x.""ProductId"",
                                'ProductIconPath', x.ProductIconPath,
                                'ProductName', x.""ProductName"",
                                'ProductDisplayName', x.""ProductDisplayName"",

                                'BeneficiaryId', x.""BeneficiaryId"",
                                'BeneficiaryWhatsappNumber', x.""BeneficiaryWhatsappNumber"",
                                'BeneficiaryName', x.""BeneficiaryName"",
                                'OnceOffBeneficiaryName', x.""OnceOffBeneficiaryName"",
                                'OnceOffBeneficiaryWhatsappNumber', x.""OnceOffBeneficiaryWhatsappNumber"",
                                'OnceOffBeneficiaryDisplayNumber', x.""OnceOffBeneficiaryDisplayNumber"",

                                'PayerWhatsappNumber', x.""PayerWhatsappNumber"",

                                'TransactionDate', x.""TransactionDate"",
                                'TransactionMessage', x.""TransactionMessage"",
                                
                                'TransactionProcessStatusId', x.""TransactionProcessStatusId"",
                                'TransactionStatus', x.TransactionStatus,
                                
                                'IsComplete', x.""IsComplete""
                            )
                        ) FILTER (WHERE x.TransactionHistoryId IS NOT NULL), NULL
                    ) AS TransactionHistoryJson

                FROM RankedTransactions x
            ";

            return query;
        }

        private static string RankedTransactions()
        {
            var query = $@"
            
                WITH RankedTransactions AS (
                    SELECT
                        tr.""Id"" AS TransactionHistoryId,
                        tr.""UserSessionId"",
                        u.""Id"" AS UserId,
                        u.""Name"" AS FirstName,
                        u.""Surname"",
                        u.""Email"",
                        u.""UserName"",
                        u.""IdNumber"",
                        u.""PassportNumber"",
                        u.""Phonber"",
                        tr.""Amount"",
                        tr.""TransactionTypeId"",
                        ty.""Type"" AS TransactionTypeName,
                        ty.""Description"" AS TransactionTypeDescription,
                        tr.""TransactionReference"",
                        tr.""TransactionDate"",
                        tr.""TransactionMessage"",
                        tr.""PaymentOptionId"",
                        po.""Option"" AS PaymentOptionName,
                        po.""IconPath"" AS PaymentOptionIconPath,
                        tr.""BeneficiaryId"",
                        b.""BeneficiaryWhatsappNumber"",
                        b.""BeneficiaryName"",
                        tr.""OnceOffBeneficiaryName"",
                        tr.""OnceOffBeneficiaryWhatsappNumber"",
                        tr.""OnceOffBeneficiaryDisplayNumber"",
                        tr.""PayerWhatsappNumber"",
                        tr.""WalletyMeRequestId"",
                        w.""PayerDisplayNumber"" AS WalletyMeRequestPayerDisplayNumber,
                        tr.""PaymentRequestId"",
                        tr.""ProductId"",
                        p.""ProductName"",
                        p.""IconPath"" AS ProductIconPath,
                        tr.""ProductDisplayName"",
                        tr.""IsComplete"",
                        tr.""TransactionProcessStatusId"",
                        ts.""Status"" AS TransactionStatus,
                        
                        -- Window function to rank records within each group based on TransactionDate
                        ROW_NUMBER() OVER (PARTITION BY tr.""TransactionReference"" ORDER BY tr.""UserSessionId"", tr.""TransactionDate"" ASC) AS rn

                    FROM ""TransactionHistory"" tr
                    LEFT JOIN ""TransactionProcessStatuses"" ts ON tr.""TransactionProcessStatusId"" = ts.""Id""
                    LEFT JOIN ""TransactionTypeConstants"" ty ON tr.""TransactionTypeId"" = ty.""TransactionTypeId""
                    LEFT JOIN ""PaymentOptions"" po ON tr.""PaymentOptionId"" = po.""PaymentOptionId""
                    LEFT JOIN ""UserSessions"" us ON tr.""UserSessionId"" = us.""Id""
                    LEFT JOIN ""WalletyMeRequests"" w ON tr.""WalletyMeRequestId"" = w.""RequestId""
                    LEFT JOIN ""Beneficiaries"" b ON tr.""BeneficiaryId"" = b.""BeneficiaryId""
                    LEFT JOIN ""Products"" p ON tr.""ProductId"" = p.""ProductId""
                    LEFT JOIN ""AspNetUsers"" u ON us.""WalletyUserId"" = u.""Id""
                    LEFT JOIN ""PaymentRequests"" pr ON tr.""PaymentRequestId"" = pr.""PaymentRequestId""
                )
            ";

            return query;
        }
        #endregion
    }
}

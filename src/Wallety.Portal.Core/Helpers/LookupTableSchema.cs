namespace Wallety.Portal.Core.Helpers.Constants
{
    public class LookupTableSchema
    {
        public static readonly string CountryTable = @"""Countries""";
        public static readonly string CountryPrimaryKey = @"""CountryId""";
        public static readonly string CountryName = @"""CountryName""";

        public static readonly string RegistrationStatusTable = @"""RegistrationStatuses""";
        public static readonly string RegistrationStatusPrimaryKey = @"""RegistrationStatusId""";
        public static readonly string RegistrationStatusName = @"""Status""";

        public static readonly string VerificationRejectReasonsTable = @"""VerificationRejectReasons""";
        public static readonly string VerificationRejectReasonsPrimaryKey = @"""RejectReasonId""";
        public static readonly string VerificationRejectReasonsName = @"""Reason""";

        public static readonly string AspNetRolesTable = @"""AspNetRoles""";
        public static readonly string AspNetRolesPrimaryKey = @"""Id""";
        public static readonly string AspNetRolesName = @"""Name""";

        public static readonly string TransactionTypesTable = @"""TransactionTypes""";
        public static readonly string TransactionTypesPrimaryKey = @"""TransactionTypeId""";
        public static readonly string TransactionTypesName = @"""Type""";

        public static readonly string PaymentOptionsTable = @"""PaymentOptions""";
        public static readonly string PaymentOptionsPrimaryKey = @"""PaymentOptionId""";
        public static readonly string PaymentOptionsName = @"""Option""";

        public static readonly string ProductsTable = @"""Products""";
        public static readonly string ProductsPrimaryKey = @"""ProductId""";
        public static readonly string ProductsName = @"""DisplayName""";
    }
}

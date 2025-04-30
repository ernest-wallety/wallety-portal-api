using System.Text.Json.Serialization;

namespace Wallety.Portal.Core.Entity.Customer
{
    public class CustomerVerifyEntity
    {
        public string CustomerId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public Guid? CountryId { get; set; }
        public string Country { get; set; } = string.Empty;

        public string PassportNumber { get; set; } = string.Empty;
        public string IdNumber { get; set; } = string.Empty;
        public string AccountCreationDate { get; set; } = string.Empty;

        public string VerificationHistoryId { get; set; } = string.Empty;
        public string CustomerServiceAgent { get; set; } = string.Empty;

        public Guid? RegistrationStatusId { get; set; }
        public string RegistrationStatus { get; set; } = string.Empty;

        public List<CustomerAccountVerificationHistoryEntity>? History { get; set; }
        public string IdentityImage { get; set; } = string.Empty;

        [JsonIgnore]
        public string HistoryJson { get; set; } = string.Empty;
    }
}

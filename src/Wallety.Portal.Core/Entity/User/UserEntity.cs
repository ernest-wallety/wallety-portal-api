using System.Text.Json.Serialization;

namespace Wallety.Portal.Core.Entity.User
{
    public class UserEntity
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string IdNumber { get; set; }
        public string PassportNumber { get; set; }

        public string AccountNumber { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public bool IsAccountActive { get; set; }
        public bool CommunicationConsent { get; set; }
        public int VerifyAttempts { get; set; }
        public bool IsFrozen { get; set; }
        public bool IsVerified { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? RegistrationStatusId { get; set; }
        public string Username { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string IdentityImage { get; set; } = string.Empty;
        public string PanicCode { get; set; } = string.Empty;
        public string DisplayNumber { get; set; }

        public string? RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleCode { get; set; }

        public List<UserRoleEntity> Roles { get; set; }

        [JsonIgnore]
        public string UserRolesJson { get; set; }

        public string CountryName { get; set; }
        public string Alpha3Code { get; set; }
        public string MobileCode { get; set; }
        public string MobileRegex { get; set; }
        public string TimeZone { get; set; }

        public DateTime? LastActiveTime { get; set; }

        public string OneTimePasswordGuid { get; set; } = string.Empty;
        public string ProfileImage { get; set; } = string.Empty;

        public Guid? WalletId { get; set; }
        public DateTime WalletCreationDate { get; set; }
        public decimal UsableBalance { get; set; }
        public decimal PendingAmount { get; set; }
        public Int32 VoucherPin { get; set; }
    }
}

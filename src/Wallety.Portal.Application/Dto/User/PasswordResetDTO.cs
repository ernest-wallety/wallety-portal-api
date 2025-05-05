using Wallety.Portal.Core.Utils;

namespace Wallety.Portal.Application.Dto.User
{
    public class PasswordResetDTO
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? OldPassword { get; set; }

        public string NewPassword => PasswordUtil.GeneratePassword();

        public string? Salt { get; set; }
        public Guid? OneTimePasswordGuid { get; set; }
        public bool IsNewUser { get; set; }
    }
}

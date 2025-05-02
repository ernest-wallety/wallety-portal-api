using Wallety.Portal.Core.Utils;

namespace Wallety.Portal.Application.Dto.User
{
    public class PasswordResetDTO
    {
        private string? _newPassword;

        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? OldPassword { get; set; }

        public string? NewPassword
        {
            get => _newPassword ?? string.Empty;
            set => PasswordUtil.GeneratePassword();
        }

        public string? Salt { get; set; }
        public Guid? OneTimePasswordGuid { get; set; }
        public bool IsNewUser { get; set; }
    }
}

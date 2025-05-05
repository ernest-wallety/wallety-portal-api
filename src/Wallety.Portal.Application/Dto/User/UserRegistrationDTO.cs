using Wallety.Portal.Core.Utils;

namespace Wallety.Portal.Application.Dto.User
{
    public class UserRegistrationDTO
    {
        private string _name = string.Empty;
        private string _surname = string.Empty;
        private string _whatsappNumber = string.Empty;
        private string _email = string.Empty;

        public string Name
        {
            get => _name;
            set => _name = !string.IsNullOrWhiteSpace(value)
                ? value.Trim()
                : throw new ArgumentException("Name cannot be empty or whitespace.", nameof(Name));
        }

        public string Surname
        {
            get => _surname;
            set => _surname = !string.IsNullOrWhiteSpace(value)
                ? value.Trim()
                : throw new ArgumentException("Surname cannot be empty or whitespace.", nameof(Surname));
        }

        public string WhatsappNumber
        {
            get => _whatsappNumber;
            set => _whatsappNumber = !string.IsNullOrWhiteSpace(value)
                ? value.Trim()
                : throw new ArgumentException("WhatsApp number cannot be empty or whitespace.", nameof(WhatsappNumber));
        }

        public string Email
        {
            get => _email;
            set
            {
                var trimmedEmail = value?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(trimmedEmail))
                {
                    throw new ArgumentException("Email cannot be empty or whitespace.", nameof(Email));
                }
                if (!EmailValidator.IsValidEmail(trimmedEmail))
                {
                    throw new ArgumentException("Invalid email format.", nameof(Email));
                }
                _email = trimmedEmail;
            }
        }

        public string RoleId { get; set; }

        public UserRegistrationDTO(
            string name,
            string surname,
            string whatsappNumber,
            string email,
            string roleId)
        {
            Name = name;
            Surname = surname;
            WhatsappNumber = whatsappNumber;
            Email = email;
            RoleId = roleId;
        }

        public UserRegistrationDTO() { }
    }
}

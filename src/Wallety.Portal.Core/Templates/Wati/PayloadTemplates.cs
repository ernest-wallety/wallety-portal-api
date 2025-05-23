using System.Text.Json;
using Wallety.Portal.Core.Helpers;

namespace Wallety.Portal.Core.Templates.Wati
{
    public class PayloadTemplates
    {
        public static string SendConfirmationMessage(string message)
        {
            var payload = new
            {
                template_name = "response_message_v16",
                broadcast_name = "response_message_v16_broadcast",
                parameters = new[]
                    {
                        new { name = "notificationmessage", value = $"{message}" }
                    }
            };

            return JsonSerializer.Serialize(payload);
        }

        public static string SendEditAccountDetailsTemplate(string id)
        {
            var payload = new
            {
                template_name = "update_customer_details_v5",
                broadcast_name = "update_customer_details_v5_broadcast",
                parameters = new[]
                {
                    new { name = "key", value = $"{EncryptionHelper.Encrypt(id)}" }
                }
            };

            return JsonSerializer.Serialize(payload);
        }

        public static string SendSignUpMessage(string firstName, string surname)
        {
            var payload = new
            {
                template_name = "new_customer_welcome_v10",
                broadcast_name = "new_customer_welcome_v10_broadcast",
                parameters = new[]
                {
                    new { name = "firstname", value = $"{firstName}" },
                    new { name = "surname", value = $"{surname}" }
                }
            };

            return JsonSerializer.Serialize(payload);
        }

        public static string SendWalletySecure(string firstName)
        {
            var payload = new
            {
                template_name = "wallety_secure_v4",
                broadcast_name = "wallety_secure_v4_broadcast",
                parameters = new[]
                {
                    new { name = "name", value = $"{firstName}" }
                }
            };

            return JsonSerializer.Serialize(payload);
        }

        public static string SendLoginTemplate(string id)
        {
            var payload = new
            {
                template_name = "user_login_v5",
                broadcast_name = "user_login_v5_broadcast",
                parameters = new[]
                {
                    new { name = "loginkey", value = $"{EncryptionHelper.Encrypt(id)}" }
                }
            };

            return JsonSerializer.Serialize(payload);
        }
    }
}

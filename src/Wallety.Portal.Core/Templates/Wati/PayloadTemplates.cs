using System.Text.Json;

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
                    new { name = "key", value = $"{id}" }
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
    }
}

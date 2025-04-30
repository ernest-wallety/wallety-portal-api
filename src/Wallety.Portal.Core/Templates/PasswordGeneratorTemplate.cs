using Wallety.Portal.Core.Utils;

namespace Wallety.Portal.Core.Templates
{
    public static class PasswordGeneratorTemplate
    {
        private static readonly string Template = @"
        <div style=""font-size: 14px; font-family: Verdana, sans-serif; background-color: #f3f3f3; display: flex; justify-content: center; align-items: center; flex-direction: column; padding: 20px;"">
            <table style=""width: 100%; max-width: 600px; margin: 0 auto; border-radius: 5px; background-color: #fff; border: 1px solid #dedede;"">
                <tr style=""text-align: center;"">
                    <td style=""padding: 20px;"">
                        <img src=""https://wallety.cash/wp-content/uploads/2024/04/Wallety_Favicon-removebg-preview-1.png"" width=""100"" alt=""Logo"">
                    </td>
                </tr>

                <tr>
                    <td style=""padding: 20px; color: #2a2a2a;"">
                        <div style=""margin-bottom: 5vh;"">
                            <h1 style=""margin: 0; text-align: center;"">Reset Password</h1>
                        </div>

                        <div style=""margin-bottom: 5vh; text-align: center;"">
                            <p>We have generated a new password for your account. Please use the password below to log in.</p>
                        </div>

                        <div style=""margin-bottom: 5vh; text-align: center;"">
                            <div style=""display: inline-block; padding: 14px 20px; font-size: 18px; font-weight: bold; border-radius: 5px; background-color: #f3f3f3; border: 1px solid #dedede; color: #2a2a2a;"">
                                <span>{PASSWORD}</span>
                            </div>
                        </div>

                        <div style=""margin-bottom: 5vh; text-align: center;"">
                            <p>For security reasons, we recommend changing this password as soon as you log in.</p>
                        </div>
                    </td>
                </tr>

                <tr style=""text-align: center;"">
                    <td style=""padding: 10px; background-color: #f3f3f3;"">
                        <p>
                            This email was sent to <a href=""mailto:{EMAIL}"" style=""color: #2a2a2a;"">{EMAIL}</a>.
                        </p>
                    </td>
                </tr>
            </table>
        </div>";

        public static string GenerateHTML(string email, string password)
        {
            if (!EmailValidator.IsValidEmail(email)) throw new ArgumentException("Invalid email format.", nameof(email));

            return Template
                .Replace("{PASSWORD}", password)
                .Replace("{EMAIL}", email);
        }
    }
}

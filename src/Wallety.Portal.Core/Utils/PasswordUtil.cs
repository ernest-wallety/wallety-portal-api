using System.Security.Cryptography;
using System.Text;

namespace Wallety.Portal.Core.Utils
{
    public static class PasswordUtil
    {
        public static string GeneratePassword()
        {
            int length = 8;
            char[] UppercaseLetters = "ABCDEFGHJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] LowercaseLetters = "abcdefghijkmnopqrstuvwxyz".ToCharArray();
            char[] Digits = "0123456789".ToCharArray();
            char[] SpecialCharacters = "!@#$%^&*()-_=+[]{};:,.<>?/".ToCharArray();
            var allCharacters = UppercaseLetters.Concat(LowercaseLetters).Concat(Digits).Concat(SpecialCharacters).ToArray();
            var password = new StringBuilder(length);
            using (var rng = RandomNumberGenerator.Create())
            {
                password.Append(UppercaseLetters[RandomNumberGeneratorGetIndex(rng, UppercaseLetters.Length)]);
                password.Append(LowercaseLetters[RandomNumberGeneratorGetIndex(rng, LowercaseLetters.Length)]);
                password.Append(Digits[RandomNumberGeneratorGetIndex(rng, Digits.Length)]);
                password.Append(SpecialCharacters[RandomNumberGeneratorGetIndex(rng, SpecialCharacters.Length)]);
                for (int i = 4; i < length; i++)
                {
                    password.Append(allCharacters[RandomNumberGeneratorGetIndex(rng, allCharacters.Length)]);
                }
                return new string(password.ToString().OrderBy(_ => RandomNumberGeneratorGetIndex(rng, int.MaxValue)).ToArray());// Shuffle the result to ensure randomness
            }
        }
        private static int RandomNumberGeneratorGetIndex(RandomNumberGenerator rng, int max)
        {
            var randomBytes = new byte[4];
            rng.GetBytes(randomBytes);
            var randomNumber = BitConverter.ToUInt32(randomBytes, 0);
            return (int)(randomNumber % max);
        }
    }
}

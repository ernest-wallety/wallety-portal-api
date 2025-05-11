namespace Wallety.Portal.Core.Utils
{
    public class WalletUtil
    {
        private static readonly Random _random = new();

        public static string GenerateVoucherPin(int length = 16)
        {
            return string.Concat(
                Enumerable.Range(0, length).Select(_ => _random.Next(0, 10).ToString())
            );
        }
    }
}

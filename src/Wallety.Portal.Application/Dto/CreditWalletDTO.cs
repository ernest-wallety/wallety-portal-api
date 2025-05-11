namespace Wallety.Portal.Application.Dto
{
    public class CreditWalletDTO
    {
        private string _whatsappNumber;

        public string WhatsappNumber
        {
            get => _whatsappNumber;
            set => _whatsappNumber = value?.Trim();
        }

        public string RoleCode { get; set; }
        public decimal Amount { get; set; }
    }
}

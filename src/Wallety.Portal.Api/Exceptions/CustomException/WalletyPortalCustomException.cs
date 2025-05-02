namespace Wallety.Portal.Api.Exceptions.CustomException
{
    public class WalletyPortalCustomException : Exception
    {
        public string? AdditionalInfo { get; set; }
        public string? Type { get; set; }
        public string? Detail { get; set; }
        public string? Title { get; set; }
        public string? Instance { get; set; }
    }
}

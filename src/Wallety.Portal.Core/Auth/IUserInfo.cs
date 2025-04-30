namespace Wallety.Portal.Core.Auth
{
    public interface IUserInfo
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string SessionToken { get; set; }
        public DateTime ExpireDate { get; set; }
        public string? UserId { get; set; }
        public string Role { get; set; }
        public int TimeStamp { get; set; }
    }
}

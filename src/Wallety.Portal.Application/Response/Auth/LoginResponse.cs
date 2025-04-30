using System.Text.Json.Serialization;
using Wallety.Portal.Application.Response.User;
using Wallety.Portal.Core.Auth;

namespace Wallety.Portal.Application.Response.Auth
{
    public class LoginResponse : IUserInfo
    {
        public string? ResponseMessage { get; set; }

        public List<UserRoleResponse>? RoleCodes { get; set; }

        public bool Success { get; set; }

        public UserResponse? User { get; set; }

        [JsonIgnore]
        public string? UserId { get; set; }
        public string? SessionToken { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime ExpireDate { get; set; }
        public string? Role { get; set; }
        public int TimeStamp { get; set; }
    }
}

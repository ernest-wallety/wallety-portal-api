namespace Wallety.Portal.Core.Entity.User
{
    public class UserRoleEntity
    {
        public string? RoleId { get; set; }
        public string? RoleCode { get; set; }
        public string? RoleName { get; set; }
        public bool IsDefault { get; set; }
    }
}

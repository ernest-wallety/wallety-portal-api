namespace Wallety.Portal.Application.Response.Menu
{
    public class MenuItemResponse
    {
        public Guid ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleDescription { get; set; }
        public string ModuleIcon { get; set; }
        public string ModuleRoute { get; set; }
        public int ModuleSortOrder { get; set; }
        public bool ModuleIsActive { get; set; }
        public bool ModuleRequireAdmin { get; set; }
        public string ModuleSidebarClass { get; set; }

        public Guid ModuleItemId { get; set; }
        public string ModuleItemName { get; set; }
        public string ModuleItemDescription { get; set; }
        public string ModuleItemIcon { get; set; }
        public string ModuleItemRoute { get; set; }
        public int ModuleItemSortOrder { get; set; }
        public bool ModuleItemIsActive { get; set; }
        public bool ModuleItemRequireAdmin { get; set; }

        public string RoleName { get; set; }
        public string RoleCode { get; set; }

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
    }
}

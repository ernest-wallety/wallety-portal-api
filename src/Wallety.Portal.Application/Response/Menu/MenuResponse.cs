namespace Wallety.Portal.Application.Response.Menu
{
    public class MenuResponse
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

        public List<MenuItemResponse> ModuleItems { get; set; }
    }
}

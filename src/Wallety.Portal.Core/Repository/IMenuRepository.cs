using Wallety.Portal.Core.Entity.Menu;

namespace Wallety.Portal.Core.Repository
{
    public interface IMenuRepository
    {
        Task<List<MenuItemEntity>> GetMenus();
    }
}

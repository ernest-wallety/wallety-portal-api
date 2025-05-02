using Wallety.Portal.Core.Entity.Menu;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Core.Repository
{
    public interface IMenuRepository
    {
        Task<DataList<MenuItemEntity>> GetMenus();
    }
}

using Wallety.Portal.Core.Entity.Menu;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Services;
using Wallety.Portal.Core.Specs;
using Wallety.Portal.Infrastructure.DbQueries;

namespace Wallety.Portal.Infrastructure.Repository
{
    public class MenuRepository(
        IPgSqlSelector sqlContext,
        ICachingInMemoryService cachingInMemoryService) : IMenuRepository
    {
        private readonly IPgSqlSelector _sqlContext = sqlContext;
        private readonly ICachingInMemoryService _cachingInMemoryService = cachingInMemoryService;

        public async Task<List<MenuItemEntity>> GetMenus()
        {
            string? email = _cachingInMemoryService.Get<string>("Email");

            var query = new MenuQuery(new BaseListCriteria()).Query();

            var items = await _sqlContext.SelectQuery<MenuItemEntity>(
                query,
                new { email = email }
            );

            return [.. items];
        }
    }
}

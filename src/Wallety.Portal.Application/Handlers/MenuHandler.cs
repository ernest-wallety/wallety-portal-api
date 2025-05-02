using MediatR;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Queries.General;
using Wallety.Portal.Application.Response.Menu;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Application.Handlers
{
    public class MenuHandler(
        IMenuRepository menuRepository,
        IUserRepository userRepository) :
        IRequestHandler<ListAllQuery<MenuResponse>,
        DataList<MenuResponse>>
    {
        private readonly IMenuRepository _menuRepository = menuRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<DataList<MenuResponse>> Handle(ListAllQuery<MenuResponse> request, CancellationToken cancellationToken)
        {
            _ = await _userRepository.GetUserByEmail() ?? throw new KeyNotFoundException("User does not exist.");

            var menuList = await _menuRepository.GetMenus();

            var menuResponseList = LazyMapper.Mapper.Map<List<MenuItemResponse>>(menuList);

            var items = menuResponseList
                           .GroupBy(m => new
                           {
                               m.ModuleId,
                               m.ModuleName,
                               m.ModuleIcon,
                               m.ModuleSortOrder,
                               m.ModuleRoute,
                               m.ModuleSidebarClass
                           })
                           .Select(g => new MenuResponse
                           {
                               ModuleId = g.Key.ModuleId,
                               ModuleIcon = g.Key.ModuleIcon,
                               ModuleName = g.Key.ModuleName,
                               ModuleSortOrder = g.Key.ModuleSortOrder,
                               ModuleRoute = g.Key.ModuleRoute,
                               ModuleSidebarClass = g.Key.ModuleSidebarClass,

                               ModuleItems = g.Any(m => m.ModuleItemId != Guid.Empty)
                                   ? g.Where(m => m.ModuleItemId != Guid.Empty).ToList()
                                   : null!
                           })
                           .ToList();


            return new DataList<MenuResponse> { Items = items, Count = items.Count };
        }
    }
}

using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Infrastructure.DbQueries
{
    public class MenuQuery : QueryBase
    {
        public MenuQuery(BaseListCriteria criteria) : base(criteria)
        {
            this.DefaultSortField = "m.sort_order";
            this.ApplySortExpression(true);
        }

        public override string Query()
        {
            var query = $@"
                
                SELECT
                
                    m.id AS ModuleId,
                    m.name AS ModuleName,
                    m.description AS ModuleDescription,
                    m.icon AS ModuleIcon,
                    m.route AS ModuleRoute,
                    m.sort_order AS ModuleSortOrder,
                    m.is_active AS ModuleIsActive,
                    m.require_admin AS ModuleRequireAdmin,
                    m.sidebar_class AS ModuleSidebarClass,
                    
                    mm.id AS ModuleItemId,
                    mm.name AS ModuleItemName,
                    mm.description AS ModuleItemDescription,
                    mm.icon AS ModuleItemIcon,
                    mm.route AS ModuleItemRoute,
                    mm.sort_order AS ModuleItemSortOrder,
                    mm.is_active AS ModuleItemIsActive,
                    mm.require_admin AS ModuleItemRequireAdmin,

                    r.""Name"" AS RoleName,
                    r.""RoleCode"" AS RoleCode,
                    
                    u.""Id"" AS UserId,
                    u.""Name"" AS FirstName,
                    u.""Surname"" AS Surname

                FROM menu.role_menu_access rma
                
                LEFT JOIN menu.module m ON m.id = rma.module_id
                LEFT JOIN menu.module_item mm ON mm.module_id = m.id

                LEFT JOIN public.""AspNetRoles"" r ON rma.role_id = r.""Id""
                LEFT JOIN public.""AspNetUserRoles"" ur ON ur.""RoleId"" = r.""Id"" AND ur.""IsDefault"" = true
                LEFT JOIN public.""AspNetUsers"" u ON ur.""UserId"" = u.""Id""
                
                WHERE
                    m.is_active = true AND
                    u.""Email"" = @email
                
                ORDER BY {_sortExpression}

            ";

            return query;
        }
    }
}

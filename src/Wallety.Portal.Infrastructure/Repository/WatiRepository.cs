using Wallety.Portal.Core.Entity.Wati;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Services;
using Wallety.Portal.Core.Specs;
using Wallety.Portal.Infrastructure.DbQueries;

namespace Wallety.Portal.Infrastructure.Repository
{
    public class WatiRepository(IPgSqlSelector sqlContext) : IWatiRepository
    {
        private readonly IPgSqlSelector _sqlContext = sqlContext;

        public async Task<DataList<WatiTemplateEntity>> GetTemplates()
        {
            var query = new WatiQuery(new BaseListCriteria()).Query();

            var items = await _sqlContext.SelectQuery<WatiTemplateEntity>(query);

            return new DataList<WatiTemplateEntity> { Items = [.. items], Count = items.Count };
        }

        public async Task<WatiTemplateEntity> GetTemplate(string code)
        {
            var query = WatiQuery.GetTemplate();

            var item = await _sqlContext.SelectFirstOrDefaultQuery<WatiTemplateEntity>(
                query,
                new { Code = code }
            );

            return item!;
        }
    }
}

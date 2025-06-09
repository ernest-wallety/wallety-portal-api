using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Infrastructure.DbQueries
{
    public class WatiQuery : QueryBase
    {
        public WatiQuery(BaseListCriteria criteria) : base(criteria)
        {
            this.DefaultSortField = "m.created_at";
            this.ApplySortExpression(true);
        }

        public override string Query()
        {
            var query = $@"

                {MainSelect()} 

                ORDER BY {_sortExpression}
            ";

            return query;
        }

        #region Select Queries
        public static string GetTemplate()
        {
            var query = $@"

                {MainSelect()} 
            
                WHERE m.code = @Code
            ";

            return query;
        }

        private static string MainSelect()
        {
            var query = $@"
                SELECT 
                
                    id,
                    code,
                    name,
                    description,
                    json_payload AS JsonPayload,
                    created_at AS CreatedAt

                FROM wati.message_template m
            ";

            return query;
        }
        #endregion
    }
}

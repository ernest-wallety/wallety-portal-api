using Wallety.Portal.Core.Helpers;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Infrastructure.DbQueries
{
    public abstract class QueryBase(BaseListCriteria criteria, string defaultSortField = "1")
    {
        public BaseListCriteria Criteria = criteria;
        public string DefaultSortField = defaultSortField;

        protected static string? _sortExpression;
        protected static string? _whereClause;

        public abstract string Query();

        public void ApplySortExpression(bool? ascending = null)
        {
            string direction = "ASC";

            if (ascending == false) direction = "DESC NULLS LAST";

            _sortExpression = string.Format("{0} {1}", DefaultSortField, direction);
        }

        public static string GetQueryTemplate(string templateName)
        {
            return string.Empty;
        }

        /// <summary>
        /// Default implementation for setting the WHERE clause.
        /// Derived classes can override and append additional conditions.
        /// </summary>
        protected virtual void SetWhereClause(BaseListCriteria criteria)
        {
            _whereClause = criteria.LookupDeserializer();
        }
    }
}

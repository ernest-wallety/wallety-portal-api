using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Infrastructure.DbQueries
{
    public class LookupQuery(BaseListCriteria criteria) : QueryBase(criteria)
    {
        public override string Query()
        {
            var query = @"
            
                SELECT 
                    
                    a.{1} AS Id, 
                    a.{2} AS Name
                
                FROM {0} a
                
                ORDER BY a.{2};

            ";

            return query;
        }

        public static string ActiveQuery()
        {
            var query = @"
            
                SELECT 
                    
                    a.{1} AS Id, 
                    a.{2} AS Name
                
                FROM {0} a

                WHERE a.""IsActive"" = true
                
                ORDER BY a.{2};

            ";

            return query;
        }

        public static string CountryQuery()
        {
            var query = @"
            
                SELECT 
                                    
                    c.""CountryId"" AS Id,
                    c.""CountryName"" AS Name,
                    c.""FlagCode"" AS Icon,
                    c.""MobileCode"" AS Code,
                    c.""MobileRegex"" AS Description
                
                FROM ""Countries"" c

                WHERE c.""IsActive"" = true
                
                ORDER BY c.""CountryName"";

            ";

            return query;
        }
    }
}

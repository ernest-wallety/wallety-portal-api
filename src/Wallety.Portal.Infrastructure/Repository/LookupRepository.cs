using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Requests.Common;
using Wallety.Portal.Core.Services;
using Wallety.Portal.Core.Specs;
using Wallety.Portal.Infrastructure.DbQueries;

namespace Wallety.Portal.Infrastructure.Repository
{
    public class LookupRepository(IPgSqlSelector sqlContext) : ILookupRepository
    {
        private readonly IPgSqlSelector _sqlContext = sqlContext;

        public async Task<DataList<LookupModel>> GetLookup(LookupParams lparams)
        {
            var strQuery = new LookupQuery(new BaseListCriteria()).Query();

            var query = string.Format(
                strQuery,
                lparams.LookupTableName,
                lparams.LookupPrimaryKey,
                lparams.LookupName
            );

            var items = await _sqlContext.SelectQuery<LookupModel>(query);

            if (lparams.IncludeNoneOption == true) items = [.. items.Prepend(new LookupModel() { Id = null, Name = "None" })];


            return new DataList<LookupModel>
            {
                Items = [.. items],
                Count = items.ToList().Count
            };
        }

        public async Task<DataList<LookupModel>> GetActiveLookup(LookupParams lparams)
        {
            var strQuery = LookupQuery.ActiveQuery();

            var query = string.Format(
                strQuery,
                lparams.LookupTableName,
                lparams.LookupPrimaryKey,
                lparams.LookupName
            );

            var items = await _sqlContext.SelectQuery<LookupModel>(query);

            if (lparams.IncludeNoneOption == true) items = [.. items.Prepend(new LookupModel() { Id = null, Name = "None" })];

            return new DataList<LookupModel>
            {
                Items = [.. items],
                Count = items.ToList().Count
            };
        }

        public async Task<DataList<LookupModel>> GetCustomLookup(LookupParams lookupParams)
        {
            var strQuery = lookupParams.LookupTableName switch
            {
                var tableName when tableName!.Contains("Countries") => LookupQuery.CountryQuery(),
                // var tableName when tableName.Contains("Users") => LookupQuery.UserQuery(),
                // Add more cases for other queries here
                _ => throw new ArgumentException("Invalid lookup table name"),
            };


            var items = await _sqlContext.SelectQuery<LookupModel>(strQuery);

            if (lookupParams.IncludeNoneOption == true) items = [.. items.Prepend(new LookupModel() { Id = null, Name = "None" })];

            return new DataList<LookupModel>
            {
                Items = [.. items],
                Count = items.ToList().Count
            };
        }
    }
}

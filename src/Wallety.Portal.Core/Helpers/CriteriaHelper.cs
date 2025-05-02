using System.Text.Json;
using System.Web;
using Wallety.Portal.Core.Requests.Common;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Core.Helpers
{
    public static class CriteriaHelper
    {
        public static string LookupDeserializer(this BaseListCriteria criteria)
        {
            var _whereClause = string.Empty;

            var lookups = new List<LookupModel>();

            if (!string.IsNullOrEmpty(criteria.Lookups))
            {
                lookups = [.. JsonSerializer.Deserialize<List<LookupModel>>(HttpUtility.UrlDecode(criteria.Lookups))
                    .Where(x => x.Id != null)];

                foreach (var lookup in lookups)
                {
                    // Replace single quotes with double quotes in the Name using the extension method
                    var lookupName = lookup.Name.ReplaceSingleWithDoubleQuotes();

                    // select-single-lookups
                    if (lookup.Id != null)
                    {
                        _whereClause += $" AND ({lookupName} = {lookup.Id})";
                    }

                    // select-multi-lookups
                    if (lookup.IdArr != null && lookup.IdArr.Length != 0)
                    {
                        lookup.IdList = string.Join(",", lookup.IdArr);

                        _whereClause += $" and ({lookupName} IN ({lookup.IdList}))";
                    }
                }
            }

            var ranges = new List<DateRangeModel>();

            if (!string.IsNullOrEmpty(criteria.Ranges))
            {
                ranges = [.. JsonSerializer.Deserialize<List<DateRangeModel>>(HttpUtility.UrlDecode(criteria.Ranges))
                    .Where(x => x.IsOnChange == true)];
            }

            foreach (var range in ranges)
            {
                if (range.IsOnChange == true)
                {
                    // Replace single quotes with double quotes in the Name using the extension method
                    var rangeName = range.Name!.ReplaceSingleWithDoubleQuotes();

                    if (range.IsRange == true && (range.StartDate != null && range.EndDate != null) && (range.StartDate != range.EndDate))
                    {
                        _whereClause +=
                            $" AND ({rangeName} >= '{range.StartDate?.ToString("yyyy-MM-dd HH:mm:ss")}' AND {rangeName} <= '{range.EndDate?.ToString("yyyy-MM-dd HH:mm:ss")}')";
                    }
                    else if (range.IsRange == false && (range.StartDate != null))
                    {
                        _whereClause +=
                            $" AND ({rangeName} >= '{range.StartDate?.AddDays(1).ToString("yyyy-MM-dd 00:00:00")}' AND {rangeName} < '{range.StartDate?.AddDays(2).ToString("yyyy-MM-dd 00:00:00")}')";
                    }
                }
            }

            return _whereClause;
        }
    }
}

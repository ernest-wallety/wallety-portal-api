using MediatR;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Application.Queries.General
{
    public class ListStatsQuery<T>(BaseListCriteria criteria) : IRequest<T> where T : class
    {
        public BaseListCriteria Criteria { get; set; } = criteria;
    }
}

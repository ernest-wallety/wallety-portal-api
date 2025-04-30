using MediatR;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Application.Queries.General
{
    public class ListQuery<T>(BaseListCriteria criteria) : IRequest<Pagination<T>> where T : class
    {
        public BaseListCriteria Criteria { get; set; } = criteria;
    }
}

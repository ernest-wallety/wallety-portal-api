using MediatR;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Application.Queries.General
{
    public class LookupQuery<T>(LookupParams p) : IRequest<DataList<T>> where T : class
    {
        public LookupParams LookupParams { get; set; } = p;
    }
}

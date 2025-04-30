using MediatR;

namespace Wallety.Portal.Application.Queries.General
{
    public class ListAllQuery<T> : IRequest<IList<T>> where T : class { }
}

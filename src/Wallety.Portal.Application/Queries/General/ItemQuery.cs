using MediatR;

namespace Wallety.Portal.Application.Queries.General
{
    public class ItemQuery<T>(Guid id) : IRequest<T> where T : class
    {
        public Guid Id { get; set; } = id;
    }
}

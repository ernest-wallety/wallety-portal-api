using MediatR;

namespace Wallety.Portal.Application.Commands.General
{
    public class DeleteCommand<T, R>(int id) : IRequest<R> where R : class
    {
        public int? Id { get; set; } = id;
    }
}

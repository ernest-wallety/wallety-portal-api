using MediatR;

namespace Wallety.Portal.Application.Queries.General
{
    public class ItemGeneralQuery<T>(string? str) : IRequest<T> where T : class
    {
        public string? STR { get; set; } = str;
    }
}

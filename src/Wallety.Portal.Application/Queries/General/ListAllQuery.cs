using MediatR;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Application.Queries.General
{
    public class ListAllQuery<T>(string? str) : IRequest<DataList<T>> where T : class
    {
        public string? STR { get; set; } = str;
    }
}

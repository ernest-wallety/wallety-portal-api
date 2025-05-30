using System.Text.Json;
using MediatR;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Queries.General;
using Wallety.Portal.Application.Response;
using Wallety.Portal.Core.Repository;

namespace Wallety.Portal.Application.Handlers.Wati
{
    public class WatiTemplateHandler(IWatiRepository repository) : IRequestHandler<ItemGeneralQuery<WatiTemplateResponse>, WatiTemplateResponse>
    {
        private readonly IWatiRepository _repository = repository;

        public async Task<WatiTemplateResponse> Handle(ItemGeneralQuery<WatiTemplateResponse> request, CancellationToken cancellationToken)
        {
            var item = await _repository.GetTemplate(request.STR!) ?? throw new ArgumentException("Transaction reference cannot be null or empty.", nameof(request.STR));

            return LazyMapper.Mapper.Map<WatiTemplateResponse>(item);
        }
    }
}

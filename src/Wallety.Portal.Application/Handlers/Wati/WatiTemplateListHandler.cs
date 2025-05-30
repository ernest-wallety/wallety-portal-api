using MediatR;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Queries.General;
using Wallety.Portal.Application.Response;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Application.Handlers.Wati
{
    public class WatiTemplateListHandler(IWatiRepository repository) :
        IRequestHandler<ListAllQuery<WatiTemplateResponse>,
        DataList<WatiTemplateResponse>>
    {
        private readonly IWatiRepository _repository = repository;

        public async Task<DataList<WatiTemplateResponse>> Handle(ListAllQuery<WatiTemplateResponse> request, CancellationToken cancellationToken)
        {
            var items = await _repository.GetTemplates();

            return LazyMapper.Mapper.Map<DataList<WatiTemplateResponse>>(items);
        }
    }
}

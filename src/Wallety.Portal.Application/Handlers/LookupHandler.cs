using MediatR;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Queries.General;
using Wallety.Portal.Application.Response;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Application.Handlers
{
    public class LookupHandler(ILookupRepository repository) : IRequestHandler<LookupQuery<LookupResponse>, DataList<LookupResponse>>
    {
        private readonly ILookupRepository _repository = repository;

        public async Task<DataList<LookupResponse>> Handle(LookupQuery<LookupResponse> request, CancellationToken cancellationToken)
        {
            var items = request.LookupParams.IsActiveField
                ? await _repository.GetActiveLookup(request.LookupParams)
                : request.LookupParams.UseCustomQuery
                    ? await _repository.GetCustomLookup(request.LookupParams)
                    : await _repository.GetLookup(request.LookupParams);

            return LazyMapper.Mapper.Map<DataList<LookupResponse>>(items);
        }
    }
}

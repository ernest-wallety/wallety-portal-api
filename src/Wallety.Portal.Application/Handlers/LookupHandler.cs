using MediatR;
using Wallety.Portal.Application.Helpers;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Queries.General;
using Wallety.Portal.Application.Response;
using Wallety.Portal.Core.Helpers.Constants;
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

            if (request.LookupParams.LookupTableName == LookupTableSchema.RegistrationStatusTable)
                items = LookupHelper.RegistrationStatuses(items, request.LookupParams.IsFilter);
            else if (request.LookupParams.LookupTableName == LookupTableSchema.VerificationRejectReasonsTable)
                items = LookupHelper.VerificationRejectReasons(items);
            else if (request.LookupParams.LookupTableName == LookupTableSchema.AspNetRolesTable)
                items = LookupHelper.Roles(items, request.LookupParams.IsFilter);

            return LazyMapper.Mapper.Map<DataList<LookupResponse>>(items);
        }
    }
}

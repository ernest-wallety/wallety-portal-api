using Wallety.Portal.Core.Requests.Common;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Core.Repository
{
    public interface ILookupRepository
    {
        Task<DataList<LookupModel>> GetLookup(LookupParams lparams);
        Task<DataList<LookupModel>> GetActiveLookup(LookupParams lparams);
        Task<DataList<LookupModel>> GetCustomLookup(LookupParams lparams);
    }
}

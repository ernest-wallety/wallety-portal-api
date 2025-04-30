using Wallety.Portal.Core.Entity;
using Wallety.Portal.Core.Results;

namespace Wallety.Portal.Core.Repository
{
    public interface IOutboundMailRepository
    {
        Task<CreateRecordResult> CreateMessageLogRecord(MessageLogEntity entity);
    }
}

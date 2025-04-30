using Wallety.Portal.Core.Entity;
using Wallety.Portal.Core.Results;

namespace Wallety.Portal.Core.Services
{
    public interface IOutboundMailService
    {
        Task<CreateRecordResult> CreateMessageLogRecord(MessageLogEntity entity);
    }
}

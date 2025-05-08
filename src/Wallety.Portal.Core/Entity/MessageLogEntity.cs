using Wallety.Portal.Core.Helpers.Constants;

namespace Wallety.Portal.Core.Entity
{
    public class MessageLogEntity
    {
        public Guid? MessageLogId { get; set; }
        public Guid? MessageLogTypeId { get; set; }
        public string Subject { get; set; }
        public string ToField { get; set; }
        public string CcField { get; set; }
        public string BccField { get; set; }
        public string FromName { get; set; }
        public string Body { get; set; }
        public string TableName { get; set; } = "mail.message_log";
        public string User { get; set; } = "Mail Service API";
        public string SmtpCode { get; set; } = SmtpConstants.DEFAULT;
        public bool IsError { get; set; } = false;
        public Guid ReturnRecordId { get; set; }
        public string? ResultMessage { get; set; }
        public string? ObjectName { get; set; }
        public int? PrimaryKey { get; set; }
    }
}

using System.Net.Mail;
using Wallety.Portal.Core.Entity;
using Wallety.Portal.Core.Helpers.Constants;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Results;
using Wallety.Portal.Core.Services;
using Wallety.Portal.Infrastructure.DbQueries;

namespace Wallety.Portal.Infrastructure.Repository
{
    public class OutboundMailRepository(IPgSqlSelector sqlContext) : IOutboundMailRepository
    {
        private readonly IPgSqlSelector _sqlContext = sqlContext;

        public async Task<CreateRecordResult> CreateMessageLogRecord(MessageLogEntity entity)
        {
            var parameters = new
            {
                p_is_error = false,
                p_return_record_id = default(Guid),
                p_result_message = default(string),
                p_object_name = default(string),

                p_message_log_id = entity.MessageLogId,
                p_message_log_type_id = entity.MessageLogTypeId,
                p_table_name = "mail.message_log",
                p_primary_key = entity.PrimaryKey,
                p_subject = entity.Subject,
                p_user = "Mail Service API",
                p_smtp_code = SmtpConstants.DEFAULT,
                p_to_field = entity.ToField,
                p_cc_field = entity.CcField,
                p_bcc_field = entity.BccField,
                p_body = entity.Body,
                p_from_name = entity.FromName
            };

            var result = await _sqlContext.ExecuteStoredProcedureAsync<dynamic>(
                "mail.message_log_insert",
                parameters
            );

            if (result?.p_is_error == true) return CreateRecordResult.Error(result?.p_result_message);

            return CreateRecordResult.Successs(result!.p_return_record_id, result!.p_result_message);
        }

        public async Task<List<MessageLogTypeEntity>> GetMessageLogTypes()
        {
            var query = MailMessageQuery.GetMessageLogTypesQuery();
            var items = await _sqlContext.SelectQuery<MessageLogTypeEntity>(query, null);

            return [.. items];
        }
    }
}

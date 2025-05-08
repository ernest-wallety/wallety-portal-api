using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Infrastructure.DbQueries
{
    public class MailMessageQuery : QueryBase
    {
        public MailMessageQuery(BaseListCriteria criteria) : base(criteria)
        {
            this.DefaultSortField = "";
            this.ApplySortExpression(true);
        }

        public override string Query()
        {
            return "";
        }

        public static string CreateMessageLogQuery()
        {
            var query = $@"

                CALL mail.message_log_insert (
                    @p_is_error, @p_return_record_id, @p_result_message, @p_object_name,
                    @p_message_log_id, @p_table_name, @p_primary_key, @p_subject, @p_user, 
                    @p_smtp_code, @p_to_field, @p_cc_field, @p_bcc_field, @p_body, @p_from_name
                );
            ";

            return query;
        }

        #region Select Queries
        public static string GetMessageLogTypesQuery()
        {
            var query = $@"
                SELECT

                    id AS MessageLogTypeId,
                    code AS MessageLogTypeCode,
                    name AS MessageLogTypeName

                FROM mail.message_log_type
            ";

            return query;
        }
        #endregion
    }
}

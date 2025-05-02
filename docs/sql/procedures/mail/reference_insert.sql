CREATE OR REPLACE PROCEDURE mail.reference_insert(
	IN p_mail_message_id integer,
	IN p_reference_message_id text)
LANGUAGE plpgsql
AS $BODY$
-- =============================================
-- Author:      Nhlanhla Malaza
-- Create date: 2025-01-31
-- Description: Insert Reference Emails to Mails
-- =============================================
DECLARE
    v_now TIMESTAMP := NOW();
BEGIN
    -- Insert statement for procedure here
    INSERT INTO mail.reference (
        mail_message_id,
        reference_message_id,
        created_at
    )
    VALUES (
        p_mail_message_id,
        p_reference_message_id,
        v_now
    );
END;
$BODY$;
ALTER PROCEDURE mail.reference_insert(IN p_mail_message_id integer, IN p_reference_message_id text)
    OWNER TO postgres;

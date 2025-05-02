CREATE OR REPLACE PROCEDURE mail.message_insert(
	OUT p_new_mail_message_id uuid,
	IN p_mailbox_id uuid DEFAULT NULL::uuid,
	IN p_subject character varying DEFAULT NULL::character varying,
	IN p_text_plain text DEFAULT NULL::text,
	IN p_text_html text DEFAULT NULL::text,
	IN p_format_text_html text DEFAULT NULL::text,
	IN p_date_sent timestamp without time zone DEFAULT NULL::timestamp without time zone,
	IN p_cc_field character varying DEFAULT NULL::character varying,
	IN p_bcc_field character varying DEFAULT NULL::character varying,
	IN p_sent_to character varying DEFAULT NULL::character varying,
	IN p_sent_from character varying DEFAULT NULL::character varying,
	IN p_sent_from_display_name character varying DEFAULT NULL::character varying,
	IN p_sent_from_raw character varying DEFAULT NULL::character varying,
	IN p_imap_message_id character varying DEFAULT NULL::character varying,
	IN p_mime_version character varying DEFAULT NULL::character varying,
	IN p_return_path character varying DEFAULT NULL::character varying,
	IN p_logged_in_user character varying DEFAULT NULL::character varying)
LANGUAGE plpgsql
AS $BODY$
-- =============================================
-- Author:		Nhlanhla Malaza
-- Create date: YYYY-MM-DD
-- Description:	Inserts received mail messages from mailboxes
-- =============================================
DECLARE
    v_now TIMESTAMP := NOW();
BEGIN
    INSERT INTO mail.message (
        mailbox_id, 
        subject,
        text_plain,
        text_html,
        format_text_html,
        date_sent,
        cc_field,
        bcc_field,
        sent_to,
        sent_from,
        sent_from_display_name,
        sent_from_raw,
        imap_message_id,
        mime_version,
        return_path,
        created_by,
        created_at,
        is_deleted
    )
    VALUES (
        p_mailbox_id,
        p_subject,
        p_text_plain,
        p_text_html,
        p_format_text_html,
        p_date_sent,
        p_cc_field,
        p_bcc_field,
        p_sent_to,
        p_sent_from,
        p_sent_from_display_name,
        p_sent_from_raw,
        p_imap_message_id,
        p_mime_version,
        p_return_path,
        p_logged_in_user,
        v_now,
        FALSE
    )
    RETURNING id INTO STRICT p_new_mail_message_id;  -- Using STRICT to ensure a value is returned

EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Error inserting message into mail.message: %', SQLERRM;
END;
$BODY$;
ALTER PROCEDURE mail.message_insert(OUT p_new_mail_message_id uuid, IN p_mailbox_id uuid DEFAULT NULL::uuid, IN p_subject character varying DEFAULT NULL::character varying, IN p_text_plain text DEFAULT NULL::text, IN p_text_html text DEFAULT NULL::text, IN p_format_text_html text DEFAULT NULL::text, IN p_date_sent timestamp without time zone DEFAULT NULL::timestamp without time zone, IN p_cc_field character varying DEFAULT NULL::character varying, IN p_bcc_field character varying DEFAULT NULL::character varying, IN p_sent_to character varying DEFAULT NULL::character varying, IN p_sent_from character varying DEFAULT NULL::character varying, IN p_sent_from_display_name character varying DEFAULT NULL::character varying, IN p_sent_from_raw character varying DEFAULT NULL::character varying, IN p_imap_message_id character varying DEFAULT NULL::character varying, IN p_mime_version character varying DEFAULT NULL::character varying, IN p_return_path character varying DEFAULT NULL::character varying, IN p_logged_in_user character varying DEFAULT NULL::character varying)
    OWNER TO postgres;

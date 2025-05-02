CREATE OR REPLACE PROCEDURE mail.message_log_insert(
	OUT p_is_error boolean,
	OUT p_return_record_id uuid,
	OUT p_result_message text,
	OUT p_object_name character varying,
	IN p_message_log_id uuid DEFAULT NULL::uuid,
	IN p_table_name character varying DEFAULT NULL::character varying,
	IN p_primary_key integer DEFAULT NULL::integer,
	IN p_subject character varying DEFAULT NULL::character varying,
	IN p_user character varying DEFAULT NULL::character varying,
	IN p_smtp_code text DEFAULT NULL::text,
	IN p_to_field text DEFAULT NULL::text,
	IN p_cc_field text DEFAULT NULL::text,
	IN p_bcc_field text DEFAULT NULL::text,
	IN p_body text DEFAULT NULL::text,
	IN p_from_name character varying DEFAULT NULL::character varying)
LANGUAGE plpgsql
AS $BODY$
-- =============================================
-- Author:      Nhlanhla Malaza
-- Create date: 2025-02-11
-- Description: Insert outbound mails to database
-- =============================================
DECLARE
    v_message_log_header_id UUID;
BEGIN
    -- Set default object name
    p_object_name := 'mail.message_log_insert';

    -- Update existing message log
    IF p_message_log_id IS NOT NULL THEN
        UPDATE mail.message_log
        SET to_field = p_to_field,
            cc_field = p_cc_field,
            subject = p_subject,
            from_field = p_from_name,
            from_name = p_from_name,
            body = p_body,
            message_log_status_id = p_message_log_status_id
        WHERE id = p_message_log_id;

        p_is_error := FALSE;
        p_return_record_id := p_message_log_id;
        p_result_message := 'Update successful';
        RETURN;
    END IF;

    -- Insert new message log
    IF p_message_log_id IS NULL THEN
        -- Insert message log header
        INSERT INTO mail.message_log_header (
            table_name,
            primary_key,
            subject,
            created_at,
            created_by
        ) VALUES (
            p_table_name,
            p_primary_key,
            p_subject,
            CURRENT_TIMESTAMP,
            p_user
        ) RETURNING id INTO v_message_log_header_id;

        IF v_message_log_header_id IS NOT NULL THEN
            -- Insert message log
            INSERT INTO mail.message_log (
                message_log_header_id,
                to_field,
                cc_field,
                bcc_field,
                subject,
                body,
                message_log_type_id,
                message_log_status_id,
                created_at,
                created_by,
                smtp_id,
                from_name
            ) VALUES (
                v_message_log_header_id,
                p_to_field,
                p_cc_field,
                p_bcc_field,
                p_subject,
                p_body,
                (SELECT id FROM mail.message_log_type WHERE code = 'E'),
                (SELECT id FROM mail.message_log_status WHERE code = 'P'),
                CURRENT_TIMESTAMP,
                p_user,
                (SELECT id FROM mail.smtp_configuration WHERE code = p_smtp_code),
                p_from_name
            ) RETURNING id INTO p_message_log_id;

            p_is_error := FALSE;
            p_return_record_id := p_message_log_id;
            p_result_message := 'Send successful';
            RETURN;
        END IF;
    END IF;

    -- If we get here, something went wrong
    p_is_error := TRUE;
    p_return_record_id := NULL;
    p_result_message := 'Operation failed';
END;
$BODY$;
ALTER PROCEDURE mail.message_log_insert(OUT p_is_error boolean, OUT p_return_record_id uuid, OUT p_result_message text, OUT p_object_name character varying, IN p_message_log_id uuid DEFAULT NULL::uuid, IN p_table_name character varying DEFAULT NULL::character varying, IN p_primary_key integer DEFAULT NULL::integer, IN p_subject character varying DEFAULT NULL::character varying, IN p_user character varying DEFAULT NULL::character varying, IN p_smtp_code text DEFAULT NULL::text, IN p_to_field text DEFAULT NULL::text, IN p_cc_field text DEFAULT NULL::text, IN p_bcc_field text DEFAULT NULL::text, IN p_body text DEFAULT NULL::text, IN p_from_name character varying DEFAULT NULL::character varying)
    OWNER TO postgres;

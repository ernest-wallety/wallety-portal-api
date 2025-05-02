CREATE OR REPLACE PROCEDURE public.user_session_insert(
    OUT p_result_message text,
    OUT p_is_error boolean,
	OUT p_return_record_id uuid,

    IN p_session_token text DEFAULT NULL,
    IN p_user_id text DEFAULT NULL
)
LANGUAGE plpgsql
AS $BODY$
-- =============================================
-- Author:		Nhlanhla Malaza
-- Create date: 2025-05-01
-- Description:	Generate user session token record
-- =============================================
DECLARE
    v_now TIMESTAMP := NOW();
    v_bearer_token TEXT;
    v_hash_key TEXT;
BEGIN
    p_is_error := FALSE;
    v_bearer_token := 'Bearer ' || p_session_token;

    SELECT encode(digest(p_session_token, 'sha256'), 'hex') INTO v_hash_key;

    -- Insert session data
    INSERT INTO "UserSessions"
    ("Id", "WalletyUserId", "SessionToken", "BearerSessionToken", "StartTime", "SessionHashKey", "LastActiveTime", "IsActive")
    VALUES
    (uuid_generate_v4(), p_user_id, p_session_token, v_bearer_token, v_now, v_hash_key, v_now, TRUE)
    RETURNING "Id" INTO p_return_record_id;

    -- Success message
    p_result_message := 'User session created successfully.';

EXCEPTION
    WHEN OTHERS THEN
        p_result_message := 'Error creating user session: ' || SQLERRM;
        p_is_error := TRUE;
        p_return_record_id := NULL;
END;
$BODY$;

CREATE OR REPLACE PROCEDURE public.user_session_update(
	OUT p_result_message text,
	OUT p_is_error boolean,
	IN p_user_id text,
	IN p_is_auto_logout boolean)
LANGUAGE plpgsql
AS $BODY$
-- =============================================
-- Author:      Nhlanhla Malaza
-- Create date: 2025-05-01
-- Description: End user session
-- =============================================
DECLARE
    v_now TIMESTAMP := NOW();
    v_session_id uuid;
BEGIN
    p_is_error := FALSE;

    -- Get the latest active session
    SELECT "Id" INTO v_session_id FROM "UserSessions"
    WHERE "WalletyUserId" = p_user_id AND "IsActive" = TRUE
    ORDER BY "StartTime" DESC LIMIT 1;

    UPDATE "UserSessions"
    SET "IsActive" = FALSE,
        "EndTime" = v_now,
        "IsAutoLogout" = p_is_auto_logout
    WHERE "Id" = v_session_id;

    p_result_message := 'User session ended successfully.';

EXCEPTION
    WHEN OTHERS THEN
        p_result_message := 'Error ending user session: ' || SQLERRM;
        p_is_error := TRUE;
END;
$BODY$;

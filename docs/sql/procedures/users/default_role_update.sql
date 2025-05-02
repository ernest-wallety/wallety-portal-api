CREATE OR REPLACE PROCEDURE public.default_role_update(
	OUT p_result_message text,
	OUT p_is_error boolean,
    
	IN p_role_id text DEFAULT NULL::text,
	IN p_user_id text DEFAULT NULL::text)
LANGUAGE plpgsql
AS $BODY$
-- =============================================
-- Author:		Nhlanhla Malaza
-- Create date: 2025-04-30
-- Description:	Set defualt role for a user
-- =============================================
DECLARE
    v_now TIMESTAMP := NOW();
BEGIN
    p_is_error := FALSE;

    -- Update previous roles to NOT default
    UPDATE "AspNetUserRoles" 
    SET "IsDefault" = FALSE
    WHERE "UserId" = p_user_id;

    -- Set the new role as default
    UPDATE "AspNetUserRoles" 
    SET "IsDefault" = TRUE
    WHERE "UserId" = p_user_id AND "RoleId" = p_role_id;

    -- Success message
    p_result_message := 'Default Role Updated Successfully!';

EXCEPTION
    WHEN OTHERS THEN
        p_result_message := 'Error updating user role: ' || SQLERRM;
        p_is_error := TRUE;
END;
$BODY$;

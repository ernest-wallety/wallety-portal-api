CREATE OR REPLACE PROCEDURE public.user_role_update(
	OUT p_result_message text,
    OUT p_is_error boolean,

	IN p_name text DEFAULT NULL::text,
	IN p_surname text DEFAULT NULL::text,
	IN p_email text DEFAULT NULL::text,
	IN p_phone_number text DEFAULT NULL::text,
	IN p_role_code text DEFAULT NULL::text)
LANGUAGE plpgsql
AS $BODY$
-- =============================================
-- Author:		Nhlanhla Malaza
-- Create date: 2025-02-19
-- Description:	Changes users roles
-- =============================================
DECLARE
    v_now TIMESTAMP := NOW();
    v_user_id TEXT;
    v_role_id TEXT;
BEGIN
    p_is_error := FALSE;

    -- Fetch user ID
    SELECT "Id" INTO v_user_id FROM "AspNetUsers" WHERE "Email" = p_email;
    IF v_user_id IS NULL THEN
        p_result_message := 'Error: User not found';
        p_is_error := TRUE;
        RETURN;
    END IF;

    -- Fetch role ID
    SELECT "Id" INTO v_role_id FROM "AspNetRoles" WHERE "RoleCode" = p_role_code;
    IF v_role_id IS NULL THEN
        p_result_message := 'Error: Role not found';
        p_is_error := TRUE;
        RETURN;
    END IF;

    -- Update previous roles to NOT default
    UPDATE "AspNetUserRoles" 
    SET "IsDefault" = FALSE
    WHERE "UserId" = v_user_id;

    -- Set the new role as default
    UPDATE "AspNetUserRoles" 
    SET "IsDefault" = TRUE
    WHERE "UserId" = v_user_id AND "RoleId" = v_role_id;

    -- Update user information
    UPDATE "AspNetUsers" 
    SET 
        "Name" = COALESCE(p_name, "Name"),
        "Surname" = COALESCE(p_surname, "Surname"),
        "Email" = COALESCE(p_email, "Email"),
        "PhoneNumber" = COALESCE(p_phone_number, "PhoneNumber")
    WHERE "Id" = v_user_id;

    -- Success message
    p_result_message := 'Update Role Successfully';

EXCEPTION
    WHEN OTHERS THEN
        p_result_message := 'Error updating user role: ' || SQLERRM;
        p_is_error := TRUE;
END;
$BODY$;

CREATE OR REPLACE PROCEDURE public.user_insert(
	OUT p_result_message text,
	OUT p_is_error boolean,
	OUT p_return_record_id uuid,

	IN p_name text,
	IN p_surname text,
    IN p_phone_number text,
    IN p_phone_number_confirmed boolean,
    IN p_username text,
    IN p_email text,
    IN p_password_hash text,
    IN p_role_id text)
LANGUAGE plpgsql
AS $BODY$
-- =============================================
-- Author:      Nhlanhla Malaza
-- Create date: 2025-05-01
-- Description: Create new user
-- =============================================
DECLARE
    v_now TIMESTAMP := NOW();
BEGIN
    p_is_error := FALSE;

    INSERT INTO "AspNetUsers"
    ("Id", "Name", "Surname", "PhoneNumber", "PhoneNumberConfirmed", "UserName", "Email", "AccountCreationDate", "PasswordHash")
    VALUES
    (uuid_generate_v4(), p_name, p_surname, p_phone_number, p_phone_number_confirmed, p_username, p_email, v_now, p_password_hash)
    RETURNING "Id" INTO p_return_record_id;

    INSERT INTO "AspNetUserRoles"
    ("UserId", "RoleId", "IsDefault")
    VALUES
    (p_return_record_id, p_role_id, TRUE);

    p_result_message := 'User created successfully.';

EXCEPTION
    WHEN OTHERS THEN
        p_result_message := 'Error ending user session: ' || SQLERRM;
        p_is_error := TRUE;
END;
$BODY$;

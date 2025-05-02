CREATE OR REPLACE PROCEDURE public.user_password_and_otp_upsert(
	OUT p_result_message text,
	OUT p_is_error boolean,
	OUT p_return_record_id uuid,

	IN p_email text,
	IN p_new_password text,
	IN p_user_id text,
	IN p_otp_guid uuid)
LANGUAGE plpgsql
AS $BODY$
-- =============================================
-- Author:		Nhlanhla Malaza
-- Create date: 2025-03-05
-- Description:	Updates or inserts Password Hash and OPT guid
-- =============================================
BEGIN
    p_is_error := FALSE;

    -- Update the password in AspNetUsers
    UPDATE "AspNetUsers"
    SET "PasswordHash" = p_new_password
    WHERE "Email" = p_email;

    -- Check if the user exists in UserAuth
    IF EXISTS (SELECT 1 FROM "UserAuth" WHERE "WalletyUserId" = p_user_id) THEN
        -- Update existing record
        UPDATE "UserAuth"
        SET "OneTimePasswordGuid" = p_otp_guid
        WHERE "WalletyUserId" = p_user_id;

        p_result_message := 'Updated Successfully';
        p_return_record_id := NULL;
    ELSE
    
       -- Insert new record
        INSERT INTO "UserAuth" ("Id", "WalletyUserId", "OneTimePasswordGuid")
        VALUES (uuid_generate_v4(), p_user_id, p_otp_guid)
        RETURNING "Id" INTO p_return_record_id;

        p_result_message := 'Inserted Successfully';
    END IF;

EXCEPTION
    WHEN OTHERS THEN
        p_result_message := 'Error updating: ' || SQLERRM;
        p_is_error := TRUE;
        p_return_record_id := NULL;
END;
$BODY$;

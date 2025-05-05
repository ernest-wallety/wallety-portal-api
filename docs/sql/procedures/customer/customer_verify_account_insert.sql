CREATE OR REPLACE PROCEDURE public.customer_verify_account_insert(
	OUT p_result_message text,
	OUT p_is_error boolean,
    OUT p_return_record_id UUID,

    IN p_logged_in_user_id UUID,
	IN p_customer_id UUID,
	IN p_registration_status_id UUID,
    IN p_verification_reject_reason_id UUID)
LANGUAGE plpgsql
AS $BODY$
-- =============================================
-- Author:      Nhlanhla Malaza
-- Create date: 2025-05-01
-- Description: Customer verification
-- =============================================
DECLARE
    v_now TIMESTAMP := NOW();
    v_customer_id UUID;
    v_registration_status_id UUID;
    v_verification_reject_reason_id UUID;

    v_user_id UUID;
    v_first_name TEXT;
    v_surname TEXT;
    v_email TEXT;
    v_customer_service_agent TEXT;
BEGIN
    p_is_error := FALSE;

    -- Fetch Logged In User ID
    SELECT "Id" INTO v_user_id FROM "AspNetUsers" WHERE "Id" = p_logged_in_user_id;
    IF p_logged_in_user_id IS NULL OR v_user_id IS NULL THEN
        p_result_message := 'Error: Logged In user account not found';
        p_is_error := TRUE;
        RETURN;
    END IF;

    -- Fetch customer ID
    SELECT "Id" INTO v_customer_id FROM "AspNetUsers" WHERE "Id" = p_customer_id;
    IF p_customer_id IS NULL OR v_customer_id IS NULL THEN
        p_result_message := 'Error: Customer account not found';
        p_is_error := TRUE;
        RETURN;
    END IF;

    -- Fetch registration ID
    SELECT "RegistrationStatusId" INTO v_registration_status_id FROM "RegistrationStatuses" WHERE "Id" = p_registration_status_id;
    IF p_registration_status_id IS NULL OR v_registration_status_id IS NULL THEN
        p_result_message := 'Error: Registartion statuses not found';
        p_is_error := TRUE;
        RETURN;
    END IF;

    -- Fetch Verification Reject Reason ID
     SELECT "RejectReasonId" INTO v_verification_reject_reason_id FROM "VerificationRejectReasons" WHERE "Id" = p_verification_reject_reason_id;
    IF p_verification_reject_reason_id IS NULL OR v_verification_reject_reason_id IS NULL THEN
        p_result_message := 'Error:"Verification reject reasons not found';
        p_is_error := TRUE;
        RETURN;
    END IF;

    SELECT "Name", "Surname", "Email" INTO v_first_name, v_surname, v_email
    FROM "AspNetUsers" WHERE "Id" = p_logged_in_user_id;

    v_customer_service_agent := v_first_name || ' ' || v_surname || ' (' || v_email || ')';

    INSERT INTO "CustomerRegistrationVerificationHistory"
    ("VerificationHistoryId", "VerificationDate", "CustomerServiceAgent", "WalletyUserId", "RegistrationStatusId", "VerificationRejectReasonId")
    VALUES
    (uuid_generate_v4(), v_now, v_customer_service_agent, v_user_id, v_registration_status_id, v_verification_reject_reason_id)
    RETURNING "VerificationHistoryId" INTO p_return_record_id;

    IF p_registration_status_id != '3efef547-5bc3-4776-95c8-1958f4710725' THEN
        UPDATE "AspNetUsers" SET
            "IsVerified" = TRUE
        WHERE "Id" = p_customer_id;
    END IF;

    p_result_message := 'Success';

EXCEPTION
    WHEN OTHERS THEN
        p_result_message := 'Error ending user session: ' || SQLERRM;
        p_is_error := TRUE;
END;
$BODY$;

CREATE OR REPLACE FUNCTION public.get_date_description(
	p_date timestamp without time zone)
    RETURNS character varying
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
-- =============================================
-- Author:		Nhlanhla Malaza
-- Create date: 2025-04-01
-- Description:	Gets a description of a date how long ago it was
-- =============================================
DECLARE
    return_value VARCHAR(50);
    minutes INT;
    hours INT;
    days INT;
BEGIN
    IF p_date IS NULL THEN
        RETURN 'Never';
    END IF;

    minutes := EXTRACT(EPOCH FROM (NOW() - p_date)) / 60;
    hours := EXTRACT(EPOCH FROM (NOW() - p_date)) / 3600;
    days := EXTRACT(EPOCH FROM (NOW() - p_date)) / 86400;

    IF days < 1 THEN
        IF hours < 1 THEN
            return_value := minutes || ' min ago';
        ELSE
            return_value := hours || ' hrs ago';
        END IF;
    ELSE
        return_value := days || ' days ago';
    END IF;

    RETURN return_value;
END;
$BODY$;

CREATE FUNCTION public.grant_admin_menu_access()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF
AS $BODY$
BEGIN
    -- Automatically grant Admin access to any new menu item if require_admin is true
    IF EXISTS (SELECT 1 FROM "AspNetRoles" WHERE "NormalizedName" = 'ADMIN') THEN
        INSERT INTO menu.role_menu_access (role_id, module_id)
        VALUES (
            (SELECT "Id" FROM "AspNetRoles" WHERE "NormalizedName" = 'ADMIN'),
            NEW.id
        );
    END IF;
    RETURN NEW;
END;
$BODY$;

CREATE TABLE IF NOT EXISTS menu.module
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    description text COLLATE pg_catalog."default",
    icon character varying(255) COLLATE pg_catalog."default",
    route character varying(255) COLLATE pg_catalog."default",
    sort_order integer,
    created_at timestamp without time zone DEFAULT now(),
    created_by character varying(255) COLLATE pg_catalog."default",
    modified_at timestamp without time zone,
    modified_by character varying(255) COLLATE pg_catalog."default",
    is_active boolean DEFAULT true,
    require_admin boolean DEFAULT true,
    sidebar_class text COLLATE pg_catalog."default",
    CONSTRAINT module_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE menu.module
    OWNER to postgres;

-- Trigger: trigger_grant_admin_access
CREATE OR REPLACE TRIGGER trigger_grant_admin_access
    AFTER INSERT
    ON menu.module
    FOR EACH ROW
    EXECUTE FUNCTION public.grant_admin_menu_access();

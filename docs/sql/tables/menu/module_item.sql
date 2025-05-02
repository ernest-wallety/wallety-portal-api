CREATE TABLE IF NOT EXISTS menu.module_item
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    module_id uuid NOT NULL,
    name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    description text COLLATE pg_catalog."default",
    icon character varying(255) COLLATE pg_catalog."default",
    route character varying(255) COLLATE pg_catalog."default",
    sort_order integer,
    created_at timestamp without time zone DEFAULT now(),
    created_by character varying(255) COLLATE pg_catalog."default" DEFAULT 'System'::character varying,
    modified_at timestamp without time zone,
    modified_by character varying(255) COLLATE pg_catalog."default",
    is_active boolean DEFAULT true,
    require_admin boolean NOT NULL,
    CONSTRAINT module_item_pkey PRIMARY KEY (id),
    CONSTRAINT fk_module FOREIGN KEY (module_id)
        REFERENCES menu.module (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE menu.module_item
    OWNER to postgres;

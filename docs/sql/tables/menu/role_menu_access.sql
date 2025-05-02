CREATE TABLE IF NOT EXISTS menu.role_menu_access
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    role_id text COLLATE pg_catalog."default",
    module_id uuid,
    CONSTRAINT role_menu_access_pkey PRIMARY KEY (id),
    CONSTRAINT role_menu_access_unique UNIQUE (role_id, module_id),
    CONSTRAINT role_menu_access_module_id_fkey FOREIGN KEY (module_id)
        REFERENCES menu.module (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    CONSTRAINT role_menu_access_role_id_fkey FOREIGN KEY (role_id)
        REFERENCES public."AspNetRoles" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE menu.role_menu_access
    OWNER to postgres;

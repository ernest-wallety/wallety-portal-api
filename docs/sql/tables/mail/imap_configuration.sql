CREATE TABLE IF NOT EXISTS mail.imap_configuration
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    code character varying(50) COLLATE pg_catalog."default" NOT NULL,
    name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    imap character varying(255) COLLATE pg_catalog."default" NOT NULL,
    ssl boolean NOT NULL,
    port integer NOT NULL,
    username character varying(255) COLLATE pg_catalog."default" NOT NULL,
    password character varying(50) COLLATE pg_catalog."default" NOT NULL,
    is_active boolean DEFAULT true,
    created_at timestamp without time zone NOT NULL DEFAULT now(),
    created_by character varying(255) COLLATE pg_catalog."default" NOT NULL DEFAULT 'System'::character varying,
    modified_at timestamp without time zone,
    modified_by character varying(255) COLLATE pg_catalog."default",
    CONSTRAINT imap_configuration_pkey PRIMARY KEY (id),
    CONSTRAINT unique_code_imap UNIQUE (code)
)

TABLESPACE pg_default;

ALTER TABLE mail.imap_configuration
    OWNER to postgres;

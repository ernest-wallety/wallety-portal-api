CREATE TABLE IF NOT EXISTS mail.smtp_configuration
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    code character varying(20) COLLATE pg_catalog."default" NOT NULL,
    name character varying(200) COLLATE pg_catalog."default" NOT NULL,
    smtp character varying(200) COLLATE pg_catalog."default" NOT NULL,
    ssl boolean NOT NULL,
    port integer NOT NULL,
    email_address character varying(200) COLLATE pg_catalog."default" NOT NULL,
    username character varying(200) COLLATE pg_catalog."default" NOT NULL,
    password character varying(200) COLLATE pg_catalog."default" NOT NULL,
    from_name character varying(200) COLLATE pg_catalog."default" NOT NULL,
    is_active boolean DEFAULT true,
    created_at timestamp without time zone NOT NULL DEFAULT now(),
    created_by character varying(255) COLLATE pg_catalog."default" NOT NULL DEFAULT 'System'::character varying,
    modified_at timestamp without time zone,
    modified_by character varying(255) COLLATE pg_catalog."default",
    is_google boolean,
    is_outlook boolean,
    app_id character varying(500) COLLATE pg_catalog."default",
    tenant_id character varying(500) COLLATE pg_catalog."default",
    secret_id character varying(500) COLLATE pg_catalog."default",
    use_access_token boolean,
    access_token text COLLATE pg_catalog."default",
    code_challange text COLLATE pg_catalog."default",
    refresh_token text COLLATE pg_catalog."default",
    CONSTRAINT smtp_configuration_pkey PRIMARY KEY (id),
    CONSTRAINT unique_code_smtp UNIQUE (code)
)

TABLESPACE pg_default;

ALTER TABLE mail.smtp_configuration
    OWNER to postgres;

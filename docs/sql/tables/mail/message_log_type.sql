CREATE TABLE IF NOT EXISTS mail.message_log_type
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    code character varying(20) COLLATE pg_catalog."default" NOT NULL,
    name character varying(100) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT message_log_type_pkey PRIMARY KEY (id),
    CONSTRAINT unique_code_type UNIQUE (code)
)

TABLESPACE pg_default;

ALTER TABLE mail.message_log_type
    OWNER to postgres;

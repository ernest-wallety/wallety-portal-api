CREATE TABLE IF NOT EXISTS mail.message_log_header
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    table_name character varying(100) COLLATE pg_catalog."default",
    primary_key integer,
    subject character varying(200) COLLATE pg_catalog."default",
    created_at timestamp without time zone NOT NULL DEFAULT now(),
    created_by character varying(255) COLLATE pg_catalog."default" NOT NULL DEFAULT 'Mail Service Api'::character varying,
    modified_at timestamp without time zone,
    modified_by character varying(255) COLLATE pg_catalog."default",
    CONSTRAINT message_log_header_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE mail.message_log_header
    OWNER to postgres;

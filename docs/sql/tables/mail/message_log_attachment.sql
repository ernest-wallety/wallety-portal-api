CREATE TABLE IF NOT EXISTS mail.message_log_attachment
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    message_log_id uuid NOT NULL,
    attachment_url text COLLATE pg_catalog."default",
    file_name character varying(200) COLLATE pg_catalog."default",
    content_id character varying(200) COLLATE pg_catalog."default",
    is_inline_image boolean,
    is_processed boolean DEFAULT false,
    created_at timestamp without time zone NOT NULL DEFAULT now(),
    created_by character varying(255) COLLATE pg_catalog."default" NOT NULL DEFAULT 'Mail Service Api'::character varying,
    azure_uri character varying(500) COLLATE pg_catalog."default",
    azure_path character varying(500) COLLATE pg_catalog."default",
    extension character varying(50) COLLATE pg_catalog."default",
    file_size_kb numeric(19,6),
    attachment_data bytea,
    CONSTRAINT message_log_attachment_pkey PRIMARY KEY (id),
    CONSTRAINT fk_message_log_attachment FOREIGN KEY (message_log_id)
        REFERENCES mail.message_log (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE mail.message_log_attachment
    OWNER to postgres;

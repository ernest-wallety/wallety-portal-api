CREATE TABLE IF NOT EXISTS mail.error_log
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    message_log_id uuid,
    message_id uuid,
    mailbox_id uuid,
    smtp_id uuid,
    imap_id uuid,
    message text COLLATE pg_catalog."default",
    stack_trace text COLLATE pg_catalog."default",
    area character varying(200) COLLATE pg_catalog."default",
    function character varying(200) COLLATE pg_catalog."default",
    source text COLLATE pg_catalog."default",
    help_link character varying(500) COLLATE pg_catalog."default",
    engine character varying(50) COLLATE pg_catalog."default",
    step character varying(200) COLLATE pg_catalog."default",
    created_at timestamp without time zone NOT NULL DEFAULT now(),
    CONSTRAINT error_log_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE mail.error_log
    OWNER to postgres;

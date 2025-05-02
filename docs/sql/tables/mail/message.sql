CREATE TABLE IF NOT EXISTS mail.message
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    mailbox_id uuid,
    subject character varying(2000) COLLATE pg_catalog."default",
    text_plain text COLLATE pg_catalog."default",
    text_html text COLLATE pg_catalog."default",
    format_text_html text COLLATE pg_catalog."default",
    date_sent timestamp without time zone,
    cc_field text COLLATE pg_catalog."default",
    bcc_field text COLLATE pg_catalog."default",
    sent_to text COLLATE pg_catalog."default",
    sent_from character varying(500) COLLATE pg_catalog."default",
    sent_from_display_name character varying(500) COLLATE pg_catalog."default",
    sent_from_raw character varying(500) COLLATE pg_catalog."default",
    imap_message_id character varying(500) COLLATE pg_catalog."default",
    mime_version character varying(255) COLLATE pg_catalog."default",
    return_path text COLLATE pg_catalog."default",
    created_by character varying(255) COLLATE pg_catalog."default" NOT NULL DEFAULT 'System'::character varying,
    created_at timestamp without time zone NOT NULL DEFAULT now(),
    is_deleted boolean NOT NULL DEFAULT false,
    CONSTRAINT message_pkey PRIMARY KEY (id),
    CONSTRAINT fk_message_mailbox FOREIGN KEY (mailbox_id)
        REFERENCES mail.mailbox (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE mail.message
    OWNER to postgres;

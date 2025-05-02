CREATE TABLE IF NOT EXISTS mail.mailbox
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    name character varying(200) COLLATE pg_catalog."default" NOT NULL,
    smtp_id uuid,
    imap_id uuid,
    is_active boolean DEFAULT true,
    created_at timestamp without time zone NOT NULL DEFAULT now(),
    created_by character varying(255) COLLATE pg_catalog."default" NOT NULL DEFAULT 'System'::character varying,
    modified_at timestamp without time zone,
    modified_by character varying(255) COLLATE pg_catalog."default",
    CONSTRAINT mailbox_pkey PRIMARY KEY (id),
    CONSTRAINT fk_mailbox_imap FOREIGN KEY (imap_id)
        REFERENCES mail.imap_configuration (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE SET NULL,
    CONSTRAINT fk_mailbox_smtp FOREIGN KEY (smtp_id)
        REFERENCES mail.smtp_configuration (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE SET NULL
)

TABLESPACE pg_default;

ALTER TABLE mail.mailbox
    OWNER to postgres;

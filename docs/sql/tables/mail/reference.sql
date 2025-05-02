CREATE TABLE IF NOT EXISTS mail.reference
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    mail_message_id uuid,
    reference_message_id character varying(500) COLLATE pg_catalog."default" NOT NULL,
    created_at timestamp without time zone NOT NULL DEFAULT now(),
    CONSTRAINT reference_pkey PRIMARY KEY (id),
    CONSTRAINT fk_reference_mail_message FOREIGN KEY (mail_message_id)
        REFERENCES mail.message (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE mail.reference
    OWNER to postgres;

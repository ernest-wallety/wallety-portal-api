CREATE TABLE IF NOT EXISTS mail.in_reply_to
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    mail_message_id uuid,
    in_reply_to_message_id character varying(100) COLLATE pg_catalog."default" NOT NULL,
    created_at timestamp without time zone NOT NULL DEFAULT now(),
    CONSTRAINT in_reply_to_pkey PRIMARY KEY (id),
    CONSTRAINT fk_in_reply_to_mail_message FOREIGN KEY (mail_message_id)
        REFERENCES mail.message (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE mail.in_reply_to
    OWNER to postgres;

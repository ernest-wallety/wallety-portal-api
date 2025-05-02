CREATE TABLE IF NOT EXISTS mail.message_log
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    message_log_header_id uuid NOT NULL,
    message_log_type_id uuid NOT NULL,
    message_log_status_id uuid NOT NULL,
    smtp_id uuid NOT NULL,
    from_field character varying(200) COLLATE pg_catalog."default",
    from_name character varying(200) COLLATE pg_catalog."default",
    to_field text COLLATE pg_catalog."default",
    cc_field text COLLATE pg_catalog."default",
    bcc_field text COLLATE pg_catalog."default",
    subject character varying(1000) COLLATE pg_catalog."default",
    body text COLLATE pg_catalog."default",
    date_sent timestamp without time zone,
    status_message text COLLATE pg_catalog."default",
    created_at timestamp without time zone NOT NULL DEFAULT now(),
    created_by character varying(255) COLLATE pg_catalog."default" NOT NULL DEFAULT 'Mail Service Api'::character varying,
    CONSTRAINT message_log_pkey PRIMARY KEY (id),
    CONSTRAINT fk_message_log_header FOREIGN KEY (message_log_header_id)
        REFERENCES mail.message_log_header (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_message_log_smtp_id FOREIGN KEY (smtp_id)
        REFERENCES mail.smtp_configuration (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_message_log_status FOREIGN KEY (message_log_status_id)
        REFERENCES mail.message_log_status (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_message_log_type FOREIGN KEY (message_log_type_id)
        REFERENCES mail.message_log_type (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE mail.message_log
    OWNER to postgres;

-- Table: public.swb_installed

-- DROP TABLE public.swb_installed;

CREATE TABLE public.swb_installed
(
    pk_id serial NOT NULL DEFAULT nextval('swb_installed_pk_id_seq'::regclass),
    install_date date,
    install_pc_name character varying(200) COLLATE pg_catalog."default",
    optionpackages_count integer,
    install_username character varying COLLATE pg_catalog."default",
    CONSTRAINT swb_installed_pkey PRIMARY KEY (pk_id)
)

TABLESPACE pg_default;

ALTER TABLE public.swb_installed
    OWNER to postgres;
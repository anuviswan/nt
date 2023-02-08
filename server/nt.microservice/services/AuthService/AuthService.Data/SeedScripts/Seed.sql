-- SCHEMA: ntschema

DROP SCHEMA IF EXISTS ntschema CASCADE;

CREATE SCHEMA IF NOT EXISTS ntschema
    AUTHORIZATION postgres;
	
	

-- Table: ntschema.users

DROP TABLE IF EXISTS ntschema.users;

CREATE TABLE IF NOT EXISTS ntschema.users
(
    id uuid NOT NULL,
    username character varying(30) COLLATE pg_catalog."default",
    password character varying(50) COLLATE pg_catalog."default",
    CONSTRAINT users_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS ntschema.users
    OWNER to postgres;
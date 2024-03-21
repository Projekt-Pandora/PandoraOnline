CREATE TABLE public.accounts
(
    account_id bigserial,
    account_name text,
    account_mail text,
    account_password_hash text,
    account_password_salt text,
    account_create_date timestamp without time zone,
    account_change_date timestamp without time zone,
    PRIMARY KEY (account_id)
);
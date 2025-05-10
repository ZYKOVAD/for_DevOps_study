CREATE TABLE IF NOT EXISTS public.users
(
    user_id uuid NOT NULL PRIMARY KEY,
    name text  NOT NULL,
    age integer
);

CREATE TABLE IF NOT EXISTS public.processed_users
(
    user_id text NOT NULL PRIMARY KEY,
    name text  NOT NULL,
    age integer,
    processed_at text NOT NULL
);

#!/bin/bash

psql -v ON_ERROR_STOP=1 --username $POSTGRES_USER women_go_tech <<EOF
    CREATE SCHEMA IF NOT EXISTS wgt;
    CREATE TABLE wgt.products
    (
        product_id BIGSERIAL NOT NULL PRIMARY KEY,
        name varchar(200) NOT NULL,
        price decimal NOT NULL,
        quantity integer NOT NULL
    );
    INSERT into wgt.products (name, price, quantity)        
    VALUES ('Dress', 39.99, 1), ('Skirt', 15.99, 3), ('Jeans', 49.99, 5);
    ALTER SCHEMA wgt OWNER TO wgt_user;
    GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA wgt TO wgt_user;
    GRANT USAGE ON ALL SEQUENCES IN SCHEMA wgt TO wgt_user;
EOF

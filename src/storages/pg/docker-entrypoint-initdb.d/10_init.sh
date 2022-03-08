#!/bin/bash

psql -v ON_ERROR_STOP=1 --username $POSTGRES_USER <<EOF
    CREATE ROLE wgt_user WITH LOGIN ENCRYPTED PASSWORD 'wgt01WGT';
    CREATE DATABASE women_go_tech;
EOF

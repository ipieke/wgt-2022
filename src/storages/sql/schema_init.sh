#!/bin/bash
set -e

trap "kill 0" EXIT

function init_schema() {
    while ! /opt/mssql-tools/bin/sqlcmd \
        -S localhost -U sa -P wgt01WGT -Q 'SELECT 1' \
        >/dev/null 2>&1
    do
        echo "SQL server not ready yet..."
        sleep 0.1
    done

    pushd /opt/schema >/dev/null || exit

    echo "init.sql";
    /opt/mssql-tools/bin/sqlcmd -b \
        -S localhost -U sa -P wgt01WGT -i "init.sql"

    echo "Success"

    popd > /dev/null || exit
}

/opt/mssql/bin/sqlservr > /dev/null &
init_schema
exit

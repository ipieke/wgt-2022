FROM mcr.microsoft.com/mssql/server:2017-CU16-ubuntu
ENV ACCEPT_EULA Y
ENV SA_PASSWORD wgt01WGT

ADD schema_init.sh /usr/local/bin/schema_init.sh
RUN chmod +x /usr/local/bin/schema_init.sh

RUN mkdir /opt/schema
ADD init.sql /opt/schema/init.sql

RUN /usr/local/bin/schema_init.sh

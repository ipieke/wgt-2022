#!/bin/bash
set -e

docker-compose down --rmi local
docker-compose build sql
docker-compose build mongo
docker-compose build pg
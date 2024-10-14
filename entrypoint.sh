#!/bin/bash

# Inicia SQL Server en segundo plano
/opt/mssql/bin/sqlservr &

# Espera a que SQL Server est� completamente operativo
echo "Esperando a que SQL Server est� listo..."
sleep 20

# Ejecuta el script de inicializaci�n SQL
echo "Ejecutando el script init.sql..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "Password123" -d master -i /usr/src/app/init.sql

# Espera indefinidamente para mantener el contenedor en ejecuci�n
wait
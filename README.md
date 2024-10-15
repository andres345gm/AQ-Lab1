# AQ-Lab1

Este proyecto es una aplicación ASP.NET Core diseñada para gestionar la información de personas, profesiones, estudios y teléfonos. Se puede utilizar para registrar y administrar datos personales, profesionales y académicos.

## Estructura del Proyecto

El proyecto incluye los siguientes archivos y carpetas clave:

- **personapi-dotnet**: Carpeta principal donde se encuentra la solución de la aplicación.
- **docker-compose.yml**: Archivo que permite construir y ejecutar la aplicación en contenedores Docker.
- **Dockerfile**: Instrucciones para crear la imagen Docker del proyecto.
- **entrypoint.sh**: Script de entrada que automatiza la configuración del contenedor Docker.
- **init.sql**: Script de inicialización de la base de datos que configura las tablas necesarias al arrancar el contenedor.

## Ejecución con Docker

Para ejecutar la aplicación en contenedores Docker, utiliza el siguiente comando:

```bash
docker-compose up --build
```

Este comando construye la imagen y levanta los servicios en contenedores.

## Detalles Técnicos

- **entrypoint.sh**: Este archivo es responsable de iniciar SQL Server en segundo plano, esperar a que esté operativo y luego ejecutar el script `init.sql` para inicializar la base de datos. Finalmente, el contenedor permanece en ejecución indefinidamente.

- **init.sql**: Este script contiene las instrucciones SQL necesarias para configurar las tablas de la base de datos (persona, profesión, estudios, teléfono) al iniciar el contenedor.

### Inicialización de la Base de Datos

#### Flujo del Script `entrypoint.sh`:

1. Inicia SQL Server en segundo plano dentro del contenedor.
2. Espera un tiempo para asegurarse de que SQL Server esté operativo.
3. Ejecuta el script `init.sql` para crear la base de datos y las tablas necesarias.
4. Mantiene el contenedor en ejecución para que la aplicación siga funcionando.

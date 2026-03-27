# Inventario API
API RESTful para la gestion de inventario, desarrollada con .NET 9, Entity Framework Core y PostgreSQL. Incluye autenticacion mediante JWT.

## Tecnologías
- .NET 9
- PostgreSQL
- Entity Framework Core
- JWT
- Swagger (OpenAPI)

## Cómo ejecutar

Clonar el repositorio:
git clone https://github.com/secaja/inventario-api-dotnet.git
cd inventario-api-dotnet
Configurar la cadena de conexión en appsettings.json:
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=inventario;Username=postgres;Password=tu_password"
}

## Ejecutar migraciones:
 -dotnet ef database update

## Ejecutar el proyecto:
 -dotnet run

## Autenticacion

La API utiliza autenticación JWT.

Obtener token:
POST /auth/login
Credenciales:
Usuario: admin
Contraseña: 1234


## Endpoints
- POST /auth/login Genera Token JWT
- GET /productos/inventario  Lista los productos
- POST /productos/movimiento  Registra los movimientos en el inventario (entradas y salidas)

## Credenciales
usuario: admin
password: 1234

Proyecto desarrollado sin Docker, configuracion local.
Uso de Entity Framework Core sin procedimientos almacenados.
Datos iniciales pueden ser cargados manualmente.


## Desarrollado por Sebastian Cadavid
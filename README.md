# ecommerce-net

API REST de comercio electrónico desarrollada con ASP.NET Core y PostgreSQL. Actualmente incluye operaciones CRUD para productos y categorías, paginación/consulta de productos y modelos para usuarios, carritos y pedidos.

## Requisitos

- .NET 10 SDK
- PostgreSQL

## Configuración y ejecución

Configura `ConnectionStrings:DefaultConnection` con la cadena de conexión de PostgreSQL, por ejemplo mediante User Secrets:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=ecommerce;Username=postgres;Password=tu-clave"
```

Desde la carpeta del proyecto, aplica las migraciones y ejecuta la API:

```bash
dotnet ef database update
dotnet run
```

En entorno de desarrollo, Swagger está disponible en `/swagger`.

## Endpoints disponibles

- `api/products`: listar, consultar, crear, actualizar y eliminar productos.
- `api/categories`: listar, consultar, crear, actualizar y eliminar categorías.

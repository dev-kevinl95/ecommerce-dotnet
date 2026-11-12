# Primer Ecomerce en .net

## Lo que vamos a construir aqui

Un e-commerce donde los usuarios puedan:

    - Registrarse e iniciar sesión.
    - Consultar productos.
    - Buscar productos por categoría.
    - Agregar productos al carrito.
    - Modificar cantidades del carrito.
    - Realizar pedidos.
    - Consultar su historial de compras.

Y donde un administrador pueda:

    - Crear, editar y eliminar productos.
    - Crear categorías.
    - Gestionar el inventario.
    - Consultar y actualizar pedidos.

## Trabajaremos con lo siguiente

    -ASP.NET Core Web API: Backend y endpoints REST.
    -PSQL: Base de datos.
    -Entity Framework Core: ORM para trabajar con la base de datos.
    -Swagger / OpenAPI / Postman: Documentar y probar endpoints.

## Nuestras tablas para la primera version del proyecto

> Users: Usuarios registrados en al tienda.
>
> - Id
> - Name
> - Email
> - PasswordHash
> - Role
>
> Categories: Categorias de productos.
>
> - Id
> - Name
> - Description
>
> Products: Catalogo de productos.
>
> - Id
> - Name
> - Description
> - Price
> - Stock
> - CategoryId
>
> Carts: Carrito de compras y sus productos
>
> - Id
> - UserId
>
> CartItems
>
> - Id
> - CartId
> - ProductId
> - Quantity
>
> Orders: Pedidos realizados y sus detalles.
>
> - Id
> - UserId
> - Total
> - Status
> - CreatedAt
>
> OrderItems
>
> - Id
> - OrderId
> - ProductId
> - Quantity
> - UnitPrice
>
> Addresses: Direcciones de entrega
>
> - Id
> - UserId
> - Street
> - City
> - Country
>
> Payments: Informacion de los pagos.
>
> - Id
> - OrderId
> - Amount
> - Method
> - Status

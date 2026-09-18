# Sistema de Ventas — Prueba Técnica HKA

Aplicación web de gestión de ventas desarrollada con **.NET 8** en el backend y **HTML/CSS/JavaScript** en el frontend, implementando **Clean Architecture**.

---

## Arquitectura

El proyecto está estructurado en capas independientes:

SistemaVentas/
├── SistemaVentas.Domain/          → Entidades de negocio
├── SistemaVentas.Application/     → Lógica de negocio, DTOs e interfaces
├── SistemaVentas.Infrastructure/  → Base de datos, repositorios (SQLite + EF Core)
├── SistemaVentas.API/             → Controllers REST + Swagger + Frontend
│   └── wwwroot/
│       └── index.html             → Interfaz web (HTML/CSS/JS)
└── SistemaVentas.Tests/           → Pruebas unitarias (xUnit)

---

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Navegador web

---

## Cómo ejecutar

### 1. Clonar el repositorio

```bash
git clone <URL_DEL_REPOSITORIO>
cd SistemaVentas
```

### 2. Levantar el backend

```bash
dotnet run --project SistemaVentas.API
```

La base de datos SQLite se crea y migra automáticamente con datos de prueba precargados.

El servidor queda disponible en:
- API: `http://localhost:5282`
- Swagger: `http://localhost:5282/swagger`

### 3. Abrir el frontend

Con el backend corriendo, abre el navegador y ve a:

`http://localhost:5282`


El frontend carga automáticamente — no se requiere abrir ningún archivo manualmente.
---

## Ejecutar pruebas

```bash
dotnet test
```

**9 pruebas unitarias** cubriendo validaciones de negocio para Clientes y Productos.

---

## Funcionalidades

- **Clientes** — Crear, listar y eliminar clientes
- **Productos** — Crear, listar y eliminar productos con control de stock
- **Ventas** — Crear facturas seleccionando cliente y múltiples productos
- **Facturas** — Historial completo con detalle por factura

---

## Lógica de negocio

- El stock se descuenta automáticamente al registrar una venta
- No se permite vender más unidades de las disponibles en stock
- El total de la factura se calcula automáticamente sumando subtotales
- Las eliminaciones son lógicas — los registros no se borran físicamente

---

## Tecnologías

| Capa | Tecnología |
|---|---|
| Backend | .NET 8, C# |
| ORM | Entity Framework Core 8 |
| Base de datos | SQLite |
| Documentación API | Swagger / OpenAPI |
| Frontend | HTML5, CSS3, JavaScript |
| Pruebas | xUnit, EF Core InMemory |
| Arquitectura | Clean Architecture |
| Control de versiones | Git / GitHub |
# 🛒 Ecommerce - ASP.NET Core MVC (En Proceso de Desarrollo)

## 📌 Descripción

Este proyecto es una aplicación de comercio electrónico desarrollada con **ASP.NET Core MVC**, que permite la gestión de productos, carritos de compra y usuarios. 

## 🚀 Características

- Registro e inicio de sesión de usuarios con **Identity**.
- Gestión de productos con categorización.
- Carrito de compras con actualización en tiempo real.
- Seguridad con autenticación y autorización por roles.

Proximas Implementaciones:
- Notificaciones,Chats y Confirmacion de Pedidos a Timepo Real, con **SIGNAL AR**
- Recomendacion de Productos
- Productos con "Me Gusta"
- Ver Usuarios conectados
- En el Dashboard ver todas las Estadisticas
- Envio de correos electrónicos para confirmación de pedidos y mensaje por Telegram

## 🛠 Tecnologías Utilizadas

- **Framework:** ASP.NET Core MVC
- **Base de Datos:** SQL Server
- **ORM:** Entity Framework Core
- **Frontend:** Bootstrap + Razor Views
- **Autenticación:** Identity Framework

## 📂 Estructura del Proyecto

```
📦 Ecommerce
├── 📁 Ecommerce.Data
│   ├── Data
│   ├── Migrations
│
├── 📁 Ecommerce.Domain
│   ├── Interface
│   ├── Models
│
├── 📁 Ecommerce.Repository
│   ├── Repositories
│
├── 📁 Ecommerce.Services
│   ├── CartService
│   ├── CategoryService
│   ├── ProductService
│   ├── ReviewService
│   ├── EmailSender.cs
│
├── 📁 Ecommerce.Web
│   ├── Properties
│   ├── wwwroot
│   ├── Areas
│   ├── Filters
│   ├── Models
│   ├── Views
│   │   ├── Shared
│   │   ├── Home
│   │   ├── Cart
│   │   ├── Account
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── Program.cs
│   ├── Service.cs
│
├── ScaffoldingReadMe.txt
```

## 📜 Instalación y Configuración

1. Clonar el repositorio:
   ```sh
   git clone https://github.com/tu-usuario/Ecommerce.git
   cd Ecommerce
   ```
2. Configurar la cadena de conexión en `appsettings.json`:
   ```json
   "ConnectionStrings": {
      "PostgresConnection": "Host=localhost;Port=5432;Database=postgres
   }
   ```
3. Aplicar migraciones y crear la base de datos:
   ```sh
   dotnet ef database update
   ```
4. Ejecutar la aplicación:
   ```sh
   dotnet run
   ```

## 📌 Uso

- Acceder a la aplicación en `http://localhost:5000`.
- Registrar usuarios y realizar compras.
- Administrar productos y revisar pedidos.

## 📄 Licencia

Este proyecto es de uso educativo y no tiene licencia comercial.

---

🔹 **Desarrollado por:** Ihan Montalvan
📧 Contacto: ihanmontalvan@gmail.com

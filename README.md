# 🛒 TecnoStore

**TecnoStore** es una aplicación de e-commerce desarrollada con **.NET MAUI Blazor Hybrid** en el frontend y **ASP.NET Core Web API** en el backend.  
El proyecto está dividido en **tres capas principales**:

- **TecnoStoreMovil** → Aplicación móvil con Blazor Hybrid (UI + lógica cliente).
- **TecnoStoreMovil.Api** → API RESTful construida en ASP.NET Core + EF Core.
- **TecnoStoreMovil.Shared** → Librería compartida con Modelos y DTOs.

---

## ✨ Funcionalidades principales

- **Autenticación y Sesión**
  - Login de usuarios con roles (Cliente / Administrador).
  - Manejo de sesión en el dispositivo usando almacenamiento seguro.

- **Catálogo de Productos**
  - Listado de productos por categoría.
  - Búsqueda por nombre o descripción.
  - Visualización del detalle del producto.
  - Control de stock.

- **Carrito de Compras**
  - Carrito persistente por usuario.
  - Agregar, quitar o modificar cantidades de productos.
  - Vaciar carrito completo.
  - Confirmación de pedido a partir del carrito.

- **Pedidos**
  - Cuando un cliente confirma su carrito → se genera un **Pedido** en estado **Pendiente**.
  - Los administradores pueden **Aceptar** (descuenta stock) o **Rechazar** un pedido.
  - El cliente visualiza su historial en **Mis pedidos**, con filtros por estado:
    - Pendientes
    - Aprobados
    - Rechazados

- **Administración**
  - Gestión de usuarios (crear, editar, eliminar).
  - Gestión de categorías y productos.
  - Panel de pedidos con detalle de cada compra.

- **Interfaz**
  - Navbar dinámico según rol.
  - Pantallas responsivas con **Bootstrap 5**.
  - Footer uniforme con créditos y desarrollador.

---

## 👥 Roles de usuario

### 👤 Cliente
- Registrarse / iniciar sesión.
- Explorar productos y categorías.
- Agregar productos al carrito.
- Confirmar pedidos.
- Consultar su historial de pedidos con filtros por estado.

### 🛠️ Administrador
- Gestionar usuarios y roles.
- Crear/editar categorías y productos.
- Revisar pedidos de clientes.
- Aceptar o rechazar pedidos → cambiando su estado.

---

## 🗂️ Estructura del proyecto

TecnoStore.sln
│
├─ TecnoStoreMovil (Frontend - .NET MAUI Blazor Hybrid)
│ ├─ Pages/ (Pantallas: Login, Productos, Carrito, MisPedidos, etc.)
│ ├─ Components/ (Componentes UI: NavMenu, Footer, Cards)
│ └─ Services/ (Clientes Http para consumir la API)
│
├─ TecnoStoreMovil.Shared (DTOs y Modelos compartidos)
│ └─ Models/ (Usuario, Rol, Direccion, Producto, Carrito, Pedido, etc.)
│ └─ DTOs/ (UsuarioDto, ProductoDto, CarritoDto, PedidoDto, etc.)
│
└─ TecnoStoreMovil.Api (Backend - ASP.NET Core Web API)
├─ Controllers/ (Usuarios, Productos, Categorias, Carrito, Pedidos, etc.)
├─ Data/ (AppDbContext con EF Core)
├─ Services/ (Lógica de negocio e interfaces)
└─ Program.cs (Configuración: EF, Swagger, CORS, autenticación)


---

## 🔧 Tecnologías utilizadas

- **Frontend:** .NET MAUI Blazor Hybrid + Bootstrap 5 + Blazor Components
- **Backend:** ASP.NET Core 8 Web API + EF Core
- **Base de datos:** SQL Server
- **Autenticación:** JWT + sesiones en almacenamiento seguro
- **Control de dependencias:** Inyección de dependencias (DI)
- **Control de versiones:** GitHub

---

## 👨‍💻 Desarrollador

Proyecto desarrollado por:

**Gustavo Torres**  
Tecnicatura Superior en Desarrollo de Software (TSDS) – ITES 2025 




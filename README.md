# ✂️ BarberLegacy API - Sistema de Gestión de Barberías

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-14.0-239120?logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?logo=microsoft-sql-server&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=black)

API RESTful para la gestión completa de barberías: turnos, barberos, clientes y servicios con autenticación JWT.

> **🚧 En desarrollo:** Frontend MVC en construcción. Actualmente disponible solo API con documentación Swagger interactiva.

## ✨ Funcionalidades

- 🔐 **Autenticación JWT** con ASP.NET Core Identity
- 📅 **Gestión de Turnos** - CRUD completo con validaciones de disponibilidad
- 💈 **Administración de Barberos** - Perfiles, horarios y especialidades
- 👥 **Gestión de Clientes** - Registro y seguimiento de historial
- 🏪 **Multi-barbería** - Soporte para múltiples sucursales
- 📋 **Servicios** - Catálogo de cortes y tratamientos con precios
- 🔒 **Autorización basada en roles** - Client/Admin
- 📖 **Documentación Swagger** - Pruebas interactivas de todos los endpoints

## 🏗️ Arquitectura

**Backend:** .NET 10 • ASP.NET Core Web API • Entity Framework Core  
**Seguridad:** JWT • ASP.NET Identity • Authorization Policies  
**Base de Datos:** SQL Server • Code-First Migrations  
**Patrones:** Repository Pattern • Dependency Injection • DTOs con AutoMapper

```
┌────────────────────────────────┐
│   Controllers (API Endpoints)  │  ← REST API
├────────────────────────────────┤
│   Services (Business Logic)    │  ← Validaciones y reglas
├────────────────────────────────┤
│   Repositories (Data Access)   │  ← Entity Framework Core
├────────────────────────────────┤
│   Entities & DTOs              │  ← Modelos de dominio
└────────────────────────────────┘
```

## 🚀 Instalación y Pruebas

```bash
# 1. Clonar repositorio
git clone https://github.com/marianovitali/BarberLegacy.git
cd BarberLegacy

# 2. Configurar connection string
# Editar appsettings.json en BarberLegacy.Api
# "DefaultConnection": "Server=.;Database=BarberLegacyDB;Trusted_Connection=True;..."

# 3. Aplicar migraciones
cd BarberLegacy.Api
dotnet ef database update

# 4. Ejecutar API
dotnet run

# 5. Abrir Swagger
# Navegar a: https://localhost:7XXX/swagger
```

**Requisitos:** .NET 10 SDK • SQL Server 2019+ 

## 📡 Endpoints Principales

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `POST` | `/api/auth/register` | Registro de usuarios |
| `POST` | `/api/auth/login` | Login con JWT |
| `GET` | `/api/appointments` | Listar turnos |
| `POST` | `/api/appointments` | Crear turno |
| `GET` | `/api/barbers` | Listar barberos |
| `GET` | `/api/services` | Listar servicios |
| `GET` | `/api/barbershops` | Listar barberías |

**📖 Documentación completa:** Disponible en `/swagger` al ejecutar la API.

## 🛠️ Características Técnicas

- ✅ **ASP.NET Core Identity** - Sistema completo de usuarios, roles y claims
- ✅ **Autenticación JWT** - Tokens con validación de issuer, audience y lifetime
- ✅ **Entity Framework Core** - Code-First con migraciones automáticas
- ✅ **Relaciones complejas** - FK con restricciones y navegación bidireccional
- ✅ **Repository Pattern** - Abstracción de acceso a datos con interfaces
- ✅ **Operaciones asíncronas** - Async/Await en toda la aplicación
- ✅ **AutoMapper** - Mapeo automático entre entidades y DTOs
- ✅ **Data Seeding** - Inicialización de roles y usuario administrador
- ✅ **Soft Delete** - Borrado lógico para mantener integridad de datos
- ✅ **Swagger personalizado** - Documentación con soporte JWT integrado
- ✅ **CORS configurado** - Preparado para integración con frontend
- ✅ **Health Checks** - Endpoint de monitoreo de base de datos

## 👨‍💻 Autor

**Mariano Vitali** - Desarrollador .NET

- GitHub: [@marianovitali](https://github.com/marianovitali)
- LinkedIn: [Mariano Vitali](https://linkedin.com/in/mariano-vitali-61b50b356/)
- Email: marianojuanvitali@gmail.com

---

⭐ **Demo interactiva disponible en Swagger** - Todos los endpoints pueden probarse sin necesidad de frontend.

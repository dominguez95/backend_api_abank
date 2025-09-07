# 🏦 Backend API - User Management System

Sistema de gestión de usuarios desarrollado en .NET 8 con PostgreSQL, implementando Clean Architecture y autenticación JWT.

## 📋 Tabla de Contenidos

- [📖 Descripción](#descripcion)
- [🏗️ Arquitectura](#arquitectura)
- [� Base de Datos](#base-de-datos)
- [�🛠️ Tecnologías](#tecnologias)
- [📋 Requisitos](#requisitos)
- [🚀 Instalación y Configuración](#instalacion)
  - [1. Clonar el repositorio](#1-clonar-el-repositorio)
  - [2. Configurar Docker](#2-configurar-docker)
  - [3. Levantar el proyecto](#3-levantar-el-proyecto)
- [🌐 Endpoints Disponibles](#endpoints)
- [🔧 Configuración Avanzada](#configuracion)
  - [Variables de Entorno](#variables-de-entorno)
  - [Base de Datos](#base-de-datos)
- [📊 Estructura del Proyecto](#estructura)
- [🧪 Testing](#testing)
- [📱 Uso de la API](#uso)
  - [1. Autenticación](#1-autenticación)
  - [2. Gestión de Usuarios](#2-gestión-de-usuarios)
- [🐛 Troubleshooting](#troubleshooting)
- [🤝 Contribución](#contribucion)
- [📄 Licencia](#licencia)

## 📖 Descripción {#descripcion}

Este proyecto es una API REST para la gestión de usuarios que incluye:

- ✅ Autenticación JWT
- ✅ CRUD completo de usuarios
- ✅ Validación de datos con FluentValidation
- ✅ Paginación de resultados
- ✅ Clean Architecture
- ✅ Documentación con Swagger
- ✅ Base de datos PostgreSQL
- ✅ Contenedores Docker
- ✅ Datos de prueba con Faker

## 🏗️ Arquitectura {#arquitectura}

El proyecto sigue los principios de **Clean Architecture** organizado en las siguientes capas:

```
📁 backend/
├── 🎯 WebApi/          # Capa de presentación (Controllers, Middleware)
├── 🔧 Application/     # Casos de uso, Commands, Queries, DTOs
├── 🏢 Domain/          # Entidades, Repositorios, Excepciones
└── 🗄️ Infrastructure/  # Implementación de repositorios, contexto de BD
```

## 💾 Base de Datos {#base-de-datos}

### 🗄️ Configuración

- **Motor**: PostgreSQL 16
- **Schema**: `auth`
- **Puerto**: 5432
- **Base de datos**: `user_management`
- **Usuario**: `admin`
- **Contraseña**: `admin123`

### 📊 Estructura de Scripts

```
📁 bd/
├── 📀 01_init.sql    # Inicialización completa de la base de datos
│   ├── Schema 'auth'
│   ├── Extensión UUID (pgcrypto)
│   ├── Tabla 'users'
│   ├── Trigger para timestamps
│   └── 2 usuarios predefinidos
└── 🌱 02_seed.py     # Script Python para datos de prueba
    ├── Conexión automática a PostgreSQL
    ├── Generación con Faker
    └── 25 usuarios adicionales
```

### 🔧 Tabla Users

```sql
CREATE TABLE auth.users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    nombres VARCHAR(255) NOT NULL,
    apellidos VARCHAR(255) NOT NULL,
    fecha_nacimiento DATE NOT NULL,
    direccion TEXT NOT NULL,
    password VARCHAR(120) NOT NULL,
    telefono CHAR(8) NOT NULL,
    email VARCHAR(150) NOT NULL,
    estado CHAR(1) NOT NULL DEFAULT 'A',
    fecha_creacion TIMESTAMP NULL,
    fecha_modificacion TIMESTAMP NULL,
    CONSTRAINT chk_estado CHECK (estado IN ('A','I'))
);
```

### ⚡ Características

- ✅ **IDs UUID**: Generación automática con `gen_random_uuid()`
- ✅ **Triggers**: Actualizacion automática de timestamps
- ✅ **Validaciones**: Constraint para estados válidos ('A', 'I')
- ✅ **Índices**: Optimización para consultas frecuentes
- ✅ **Datos iniciales**: 2 usuarios + 25 generados con Faker

## 🛠️ Tecnologías {#tecnologias}

### Backend

- **Framework**: .NET 8.0
- **Base de Datos**: PostgreSQL 16
- **ORM**: Dapper
- **Autenticación**: JWT Bearer Token
- **Validación**: FluentValidation
- **Mediator**: MediatR
- **Documentación**: Swagger/OpenAPI

### DevOps

- **Contenedores**: Docker & Docker Compose
- **Base de Datos**: PostgreSQL con inicialización automática
- **Datos de Prueba**: Python con Faker

## 📋 Requisitos {#requisitos}

- 🐳 **Docker Desktop** instalado y ejecutándose
- 🛠️ **Docker Compose** (incluido con Docker Desktop)
- 🌐 **Navegador web** para acceder a Swagger

> **Nota**: No necesitas tener .NET o PostgreSQL instalados localmente, todo se ejecuta en contenedores.

## 🚀 Instalación y Configuración {#instalacion}

### 1. Clonar el repositorio

```bash
git clone <url-del-repositorio>
cd backend_test_abank
```

### 2. Configurar Docker

Asegúrate de que Docker Desktop esté ejecutándose en tu sistema.

### 3. Levantar el proyecto

```bash
# Iniciar contenedor en segundo plano
docker-compose up --build -d

# Construir y levantar todos los servicios
docker-compose up --build

```

**¡Eso es todo!** 🎉 El proyecto estará disponible en:

- 🌐 **API**: http://localhost:8080
- 📖 **Swagger**: http://localhost:8080/swagger
- 🗄️ **Base de Datos**: localhost:5432

## 🌐 Endpoints Disponibles {#endpoints}

### 🔐 Autenticación

- `POST /api/v1/Auth/login` - Iniciar sesión

### 👥 Gestión de Usuarios

- `GET /api/v1/Users` - Listar usuarios (paginado)
- `GET /api/v1/Users/{id}` - Obtener usuario por ID
- `POST /api/v1/Users` - Crear nuevo usuario
- `PUT /api/v1/Users/{id}` - Actualizar usuario
- `DELETE /api/v1/Users/{id}` - Eliminar usuario

> 💡 **Tip**: Visita http://localhost:8080/swagger para probar todos los endpoints interactivamente.

## 🔧 Configuración Avanzada {#configuracion}

### Variables de Entorno

El proyecto utiliza las siguientes variables de entorno (configuradas automáticamente):

```env
# Base de Datos
DB_HOST=db
DB_PORT=5432
DB_NAME=user_management
DB_USER=admin
DB_PASS=admin123

# API
ASPNETCORE_URLS=http://+:8080
ASPNETCORE_ENVIRONMENT=Development
```

### Base de Datos

- **🗄️ Motor**: PostgreSQL 16
- **📊 Schema**: `auth`
- **👤 Usuarios por defecto**: 2 usuarios predefinidos + 26 generados automáticamente
- **🔐 Contraseña**: `$2a$11$xOt84VX.8Z.maYrEWq6mO.XLXYRL5/rmpnTdg5ppn54bA6d6juLJG` (hasheada)

## 📊 Estructura del Proyecto {#estructura}

```
📦 backend_test_abank/
├── 🐳 docker-compose.yml
├── 🗄️ bd/
│   ├── 01_init.sql          # Script de inicialización de BD
│   ├── 02_seed.py           # Generador de datos de prueba
│   └── Dockerfile           # Imagen para el seeder
└── 🎯 backend/
    ├── 🌐 WebApi/           # Controllers, Program.cs, Swagger
    ├── 🔧 Application/      # Commands, Handlers, DTOs, Validators
    ├── 🏢 Domain/           # Entidades, Interfaces, Excepciones
    └── 🗄️ Infrastructure/   # Repositorios, DapperContext
```

## 🧪 Testing {#testing}

### Usuarios de Prueba

```json
{
  "telefono": "prueba",
  "password": "123456789"
}
```

```json
{
  "telefono": "prueba1",
  "password": "123456789"
}
```

### Datos Generados

- **👥 Usuarios**: 25 registros totales (2 predefinidos + 25 generados)
- **📊 Estados**: 'A' (Activo) e 'I' (Inactivo) distribuidos aleatoriamente
- **📅 Edades**: Mayores de 18 años

## 📱 Uso de la API {#uso}

### 1. Autenticación

```bash
# Obtener token JWT
curl -X POST "http://localhost:8080/api/v1/Auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "telefono": "prueba",
    "password": "123456789"
  }'
```

### 2. Gestión de Usuarios

```bash
# Listar usuarios (requiere token)
curl -X GET "http://localhost:8080/api/v1/Users" \
  -H "Authorization: Bearer {tu-token-jwt}"
```

> 💡 **Recomendación**: Usa Swagger UI en http://localhost:8080/swagger para una experiencia más amigable.

## 🐛 Troubleshooting {#troubleshooting}

### ❌ Problema: El contenedor no se levanta

**Solución**: Verificar que Docker Desktop esté ejecutándose:

```bash
docker --version
docker-compose --version
```

### ❌ Problema: Puerto 8080 ocupado

**Solución**: Cambiar el puerto en `docker-compose.yml`:

```yaml
ports:
  - "8081:8080" # Cambiar primer número
```

### ❌ Problema: Base de datos con muchos registros

**Solución**: Limpiar y reiniciar:

```bash
# Eliminar todo (contenedores, imágenes, volúmenes)
docker-compose down --volumes --rmi all

# Volver a levantar
docker-compose up --build
```

### ❌ Problema: Error de autenticación

**Verificar**:

- Usar las credenciales correctas
- Incluir el token en el header: `Authorization: Bearer {token}`
- Token no expirado (24 horas de validez)

## 🤝 Contribución {#contribucion}

1. 🍴 Fork el proyecto
2. 🌿 Crea una rama para tu feature (`git checkout -b feature/nueva-funcionalidad`)
3. 💾 Commit tus cambios (`git commit -m 'Agregar nueva funcionalidad'`)
4. 📤 Push a la rama (`git push origin feature/nueva-funcionalidad`)
5. 🔄 Abre un Pull Request

## 📄 Licencia {#licencia}

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

---

⭐ **¿Te fue útil este proyecto?** ¡Dale una estrella en GitHub!

📧 **¿Tienes preguntas?** Abre un issue en el repositorio.

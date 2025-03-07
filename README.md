# 🚀 **Sistema Gestor de Proyectos**

## 📌 **Índice**
- [🚀 **Sistema Gestor de Proyectos**](#-sistema-gestor-de-proyectos)
  - [📌 **Índice**](#-índice)
  - [👨‍💻 **Equipo de Desarrollo**](#-equipo-de-desarrollo)
  - [📜 **Descripción**](#-descripción)
  - [📂 **Estructura del Proyecto**](#-estructura-del-proyecto)
    - [📦 `Rush.Application` - *Lógica de aplicación y reglas de negocio.*](#-rushapplication---lógica-de-aplicación-y-reglas-de-negocio)
    - [📦 `Rush.Domain` - *Definición de entidades y modelos del dominio.*](#-rushdomain---definición-de-entidades-y-modelos-del-dominio)
    - [📦 `Rush.Infrastructure` - *Acceso a datos y configuraciones.*](#-rushinfrastructure---acceso-a-datos-y-configuraciones)
    - [📦 `Rush.WebAPI` - *Proyecto principal que expone la API.*](#-rushwebapi---proyecto-principal-que-expone-la-api)
  - [🖥️ **Tecnologías Utilizadas**](#️-tecnologías-utilizadas)
    - [Backend ⚙️](#backend-️)
    - [Frontend 🎨](#frontend-)
    - [Base de Datos 🗄](#base-de-datos-)
    - [Seguridad 🔒](#seguridad-)
    - [Arquitectura y Patrones 🏗](#arquitectura-y-patrones-)
  - [⚙️ **Guía para Configurar el Proyecto**](#️-guía-para-configurar-el-proyecto)
    - [1️⃣ Clonar el Repositorio](#1️⃣-clonar-el-repositorio)
    - [2️⃣ Configurar la Rama de Trabajo](#2️⃣-configurar-la-rama-de-trabajo)
    - [3️⃣ Mantener la Rama Actualizada](#3️⃣-mantener-la-rama-actualizada)
    - [4️⃣ Subir Cambios y Crear un Pull Request](#4️⃣-subir-cambios-y-crear-un-pull-request)
    - [5️⃣ Configurar la Conexión a SQL Server](#5️⃣-configurar-la-conexión-a-sql-server)
    - [6️⃣ Generar y Aplicar Migraciones](#6️⃣-generar-y-aplicar-migraciones)
    - [7️⃣ Verificar y Ejecutar el Proyecto](#7️⃣-verificar-y-ejecutar-el-proyecto)
    - [✨ **¡Listo para comenzar!** 💪😎](#-listo-para-comenzar-)

---

## 👨‍💻 **Equipo de Desarrollo**

Los genios detrás de este proyecto:

- **Luis Alberto Gomez Broca** (`22393177`) - 💡 *Project Manager*
- **Carlos Francisco Valier Sanchez** (`22393231`) - 🎨 *Desarrollador UX/UI*
- **Misael De Jesus Rosado Paat** (`22393270` - `Misarosa24`) - 🔧 *Desarrollador Frontend*
- **Juan Diego Mendoza Gutierrez** (`22393123` - `SublimeDevv` - `juanmen1404@gmail.com`) - 🚀 *Desarrollador Backend*
- **Fernando Alberto Villafaña Alfonseca** (`DEADMOUS3` - `fferando.villafanalfonseca@gmail.com` - `22394162`) - ✏️ *Analista QA*

---

## 📜 **Descripción**

La gestión de proyectos es clave para el éxito empresarial, ya que permite cumplir objetivos dentro de los plazos y optimizar recursos. Un sistema adecuado facilita la organización de tareas, asignación de responsabilidades y seguimiento del progreso, mejorando la colaboración y eficiencia.

Según el gerente de *Deloitte Consulting*, *Rodríguez Rodrigo*, una gestión clara del alcance es esencial para evitar fallas en los proyectos. Los problemas comunes incluyen:
- Falta de herramientas adecuadas
- Comunicación deficiente
- Dificultades en el registro de horas
- Poca visibilidad del avance
- Retrasos por falta de sincronización

Para solucionarlos, se propone una **plataforma centralizada** que optimice la gestión de proyectos en una sola aplicación.

---

## 📂 **Estructura del Proyecto**

### 📦 `Rush.Application` - *Lógica de aplicación y reglas de negocio.*
- 📂 `Extensions`
- 📂 `Interfaces`
- 📂 `Mappings`
- 📂 `Services`

### 📦 `Rush.Domain` - *Definición de entidades y modelos del dominio.*
- 📂 `Common`
- 📂 `DTO`
- 📂 `Entities`

### 📦 `Rush.Infrastructure` - *Acceso a datos y configuraciones.*
- 📂 `Interfaces`
- 📂 `Migrations`
- 📂 `Repositories`
- 📄 `ApplicationDbContext.cs`

### 📦 `Rush.WebAPI` - *Proyecto principal que expone la API.*
- 📂 `wwwroot`
- 📂 `Controllers`
- 📂 `Logs`
- 📄 `Program.cs`

---

## 🖥️ **Tecnologías Utilizadas**

### Backend ⚙️
- ![ASP.NET](https://img.shields.io/badge/.NET_Core_8-512BD4?style=flat&logo=dotnet&logoColor=white) **ASP.NET Core 8**
- 🛡 **FluentValidation** - Validación de datos
- 🔄 **AutoMapper** - Mapeo de modelos
- 🗄 **Entity Framework Core** - ORM para bases de datos
- ⚡ **Dapper** - Consultas SQL optimizadas
- 🔑 **ASP.NET Core Identity** - Autenticación y autorización
- 🔐 **JWT (JSON Web Tokens)** - Seguridad de endpoints
- 📜 **Serilog** - Monitoreo y registros

### Frontend 🎨
- ![VueJS](https://img.shields.io/badge/Vue_3-4FC08D?style=flat&logo=vue.js&logoColor=white) **Vue 3**

### Base de Datos 🗄
- 🏛 **SQL Server**
- 📑 **Diccionario de Datos** - Documentación de datos

### Seguridad 🔒
- **Roles y permisos en ASP.NET Core Identity**
- **Protección de endpoints con JWT**

### Arquitectura y Patrones 🏗
- **Repository Pattern** - Abstracción de acceso a datos
- **Generic Base Repository** - Reutilización de operaciones comunes
- **Adaptadores para servicios externos**

---

## ⚙️ **Guía para Configurar el Proyecto**

### 1️⃣ Clonar el Repositorio
```bash
# Clonar con SSH
git clone git@github.com:SublimeDevv/rush-project.git

# Clonar con HTTPS
git clone https://github.com/SublimeDevv/rush-project.git
```

### 2️⃣ Configurar la Rama de Trabajo
```bash
git checkout develop
```
🔹 Si la rama `develop` no aparece:
```bash
git fetch
```

🔹 Para crear una nueva rama:
```bash
git checkout -b feature/login
```

### 3️⃣ Mantener la Rama Actualizada
```bash
git checkout develop
git pull origin develop
git checkout feature/login
git merge develop
```

### 4️⃣ Subir Cambios y Crear un Pull Request
```bash
git push origin feature/login
```
Luego, crea un **Pull Request** en GitHub hacia `develop`.

### 5️⃣ Configurar la Conexión a SQL Server
Crea un archivo de configuración en la carpeta del proyecto y establece la cadena de conexión adecuada.

### 6️⃣ Generar y Aplicar Migraciones
```powershell
# Generar una nueva migración
Add-Migration NombreDeLaMigracion

# Aplicar migraciones existentes
Update-Database
```

### 7️⃣ Verificar y Ejecutar el Proyecto
Asegúrate de que todo esté bien y ejecuta el proyecto. 🎉

---

### ✨ **¡Listo para comenzar!** 💪😎


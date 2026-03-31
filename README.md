#  Task Management API

##  Descripción

API REST desarrollada en ASP.NET Core para la gestión de tareas tipo Kanban.  
Permite administrar proyectos, usuarios y tareas con soporte de tiempo real mediante SignalR.

---

##  Tecnologías

- ASP.NET Core Web API
- Entity Framework Core
- SignalR
- Clean Architecture

---

##  Arquitectura

El sistema está basado en **Clean Architecture**, separando responsabilidades en capas:

- **Domain** → entidades y contratos
- **Application** → casos de uso (lógica de negocio)
- **Infrastructure** → acceso a datos
- **Presentation** → controllers (API)

---

##  Modelo C4

Se definió la arquitectura utilizando el modelo C4 para representar el sistema en distintos niveles.

 Ver detalle: `https://github.com/LizethOrellana/TodoApp.Api/blob/main/docs/c4.md`

---

##  Metodología

El desarrollo se realizó siguiendo un enfoque basado en Scrum.

 Ver detalle: `https://github.com/LizethOrellana/TodoApp.Api/blob/main/docs/scrum.md`

---

##  Funcionalidades

- Gestión de proyectos
- Gestión de usuarios
- CRUD de tareas
- Estados: Pending, InProcess, Done
- Soft delete de tareas
- Restauración de tareas
- Drag & Drop (Kanban)
- Tiempo real con SignalR

---

##  Flujo del sistema

Controller → UseCase → Repository → Database

---

##  Tiempo real

Se utiliza SignalR para notificar cambios en tareas en tiempo real a los clientes.

---

##  Cliente móvil

Este backend es consumido por una aplicación móvil en Flutter.

---

##  Conclusión

La arquitectura implementada permite escalabilidad, mantenibilidad y separación clara de responsabilidades.

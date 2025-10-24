# API de Tareas - .NET 8 con Clean Architecture

API RESTful para gestionar tareas, construida con .NET 8 siguiendo principios de Clean Architecture.

## 📁 Estructura del Proyecto

```
TareasApi/
├── Domain/              # Entidades y lógica de dominio
│   └── Tarea.cs         # Entidad Tarea con validaciones
├── DataAccess/          # Acceso a datos y repositorios
│   ├── ITareaRepository.cs
│   ├── TareaRepository.cs
│   └── TareaDbContext.cs
├── BusinessLogic/       # Lógica de negocio y servicios
│   ├── ITareaService.cs
│   └── TareaService.cs
├── DTOs/                # Data Transfer Objects
│   ├── TareaDto.cs
│   └── CrearTareaDto.cs
└── WebApi/              # Controladores y configuración
    ├── Controllers/
    │   └── TareasController.cs
    └── Program.cs
```

## 🏗️ Principios de Clean Architecture

### 1. **Capa de Dominio (Domain)**
- Entidades con lógica de negocio encapsulada
- Sin dependencias externas
- Validaciones en los constructores y métodos
- Setters privados para mantener el estado consistente

### 2. **Capa de Acceso a Datos (DataAccess)**
- Patrón Repository
- Entity Framework Core con InMemory Database
- Interfaces para inyección de dependencias
- Datos de prueba (seed data)

### 3. **Capa de Lógica de Negocio (BusinessLogic)**
- Servicios que orquestan las operaciones
- Manejo de reglas de negocio
- Validaciones de alto nivel

### 4. **Capa de DTOs**
- Objetos para transferencia de datos
- Mapeo desde/hacia entidades de dominio
- Separación entre modelo de dominio y API

### 5. **Capa de API (WebApi)**
- Controladores RESTful
- Manejo de errores HTTP
- Configuración de CORS para Angular
- Documentación con Swagger

## 🚀 Endpoints de la API

### Obtener todas las tareas
```http
GET /api/tareas
```

**Respuesta:**
```json
[
  {
    "id": 1,
    "titulo": "Aprender Angular",
    "descripcion": "Estudiar los conceptos básicos...",
    "completada": false,
    "fechaCreacion": "2025-01-15T00:00:00Z"
  }
]
```

### Obtener una tarea por ID
```http
GET /api/tareas/{id}
```

### Crear una nueva tarea
```http
POST /api/tareas
Content-Type: application/json

{
  "titulo": "Nueva tarea",
  "descripcion": "Descripción de la tarea"
}
```

### Actualizar una tarea
```http
PUT /api/tareas/{id}
Content-Type: application/json

{
  "titulo": "Tarea actualizada",
  "descripcion": "Nueva descripción"
}
```

### Cambiar estado (completada/pendiente)
```http
PATCH /api/tareas/{id}/cambiar-estado
```

### Eliminar una tarea
```http
DELETE /api/tareas/{id}
```

## 🔧 Ejecutar el Proyecto

### Prerrequisitos
- .NET 8 SDK

### Pasos

1. **Restaurar paquetes**
```bash
cd TareasApi
dotnet restore
```

2. **Compilar la solución**
```bash
dotnet build
```

3. **Ejecutar la API**
```bash
cd WebApi
dotnet run
```

La API estará disponible en:
- **HTTPS**: https://localhost:7XXX
- **HTTP**: http://localhost:5XXX
- **Swagger**: https://localhost:7XXX/swagger

## 🔗 Conexión con Angular

La API está configurada con CORS para aceptar peticiones desde `http://localhost:4200` (puerto por defecto de Angular).

Para conectar desde Angular, actualiza el servicio de tareas para usar la URL de la API:

```typescript
export class TaskService {
  private apiUrl = 'https://localhost:7XXX/api/tareas';  // Actualizar con el puerto correcto

  constructor(private http: HttpClient) { }

  getTasks(): Observable<Task[]> {
    return this.http.get<any[]>(this.apiUrl).pipe(
      map(tasksAPI => tasksAPI.map(taskAPI => this.adaptTask(taskAPI)))
    );
  }

  // ... otros métodos
}
```

## 📚 Conceptos Clave para Estudiantes

### Domain-Driven Design (DDD)
- Las entidades contienen la lógica de negocio
- Los setters son privados para controlar el estado
- Los métodos de comportamiento modifican el estado de forma controlada

### Patrón Repository
- Abstracción del acceso a datos
- Permite cambiar la implementación sin afectar la lógica de negocio
- Facilita el testing con mocks

### Inyección de Dependencias
- Los servicios reciben sus dependencias por constructor
- ASP.NET Core maneja automáticamente el ciclo de vida
- Facilita el testing y reduce el acoplamiento

### Separación de Responsabilidades
- Cada capa tiene una responsabilidad específica
- Los cambios en una capa no afectan a las demás
- Código más mantenible y testeable

## 🎓 Puntos de Aprendizaje

1. **Clean Architecture**: Organización del código en capas
2. **Entity Framework Core**: ORM para .NET
3. **Patrón Repository**: Abstracción del acceso a datos
4. **Async/Await**: Programación asíncrona en C#
5. **RESTful API**: Diseño de APIs HTTP
6. **CORS**: Configuración para comunicación con SPA
7. **Swagger**: Documentación automática de APIs

## 🔄 Flujo de una Petición

1. **Cliente** → Petición HTTP al endpoint
2. **Controller** → Recibe la petición, valida datos
3. **Service** → Ejecuta la lógica de negocio
4. **Repository** → Accede a los datos
5. **DbContext** → Interactúa con la base de datos
6. **Repository** → Retorna entidades de dominio
7. **Service** → Procesa y retorna al controller
8. **Controller** → Mapea a DTO y responde al cliente

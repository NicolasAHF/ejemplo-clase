# Guía - Routing y Servicios en Angular

Esta guía está diseñada para enseñar **Routing** y **Servicios** en Angular de forma progresiva.


---

## 🎯 PARTE 1: Angular Material (Introducción Rápida)

**Objetivo**: Mostrar cómo agregar Angular Material sin entrar en detalles de componentes.

### Paso 1.1: Crear proyecto nuevo

```bash
ng new gestor-peliculas --routing --style=css
cd gestor-peliculas
```

### Paso 1.2: Instalar Angular Material

```bash
ng add @angular/material
```

Seleccionar:
- Theme: Indigo/Pink
- Typography: Yes
- Animations: Enabled


- Angular Material nos da componentes UI listos para usar
- No vamos a profundizar en los componentes, solo los usaremos
- La documentación está en material.angular.io

---

## 🛣️ PARTE 2: ROUTING (Desde Cero)

### Paso 2.1: Crear el modelo de datos

Antes de crear componentes, necesitamos definir qué datos vamos a manejar.

```bash
ng generate interface models/pelicula
```

**Archivo**: `src/app/models/pelicula.ts`

```typescript
export interface Pelicula {
  id: number;
  titulo: string;
  director: string;
  anio: number;
  genero: string;
  vista: boolean;
  calificacion: number;
}
```

- Una interfaz define la estructura de nuestros datos
- TypeScript nos ayuda a evitar errores con el tipado fuerte

---

### Paso 2.2: Crear componentes para las rutas

```bash
ng generate component components/home
ng generate component components/pelicula-list
ng generate component components/pelicula-detail
```

Vamos a tener 3 páginas:
- Home (página principal)
- Pelicula List (lista de películas)
- Pelicula Detail (detalle de una película)

---

### Paso 2.3: Configurar las rutas

**Archivo**: `src/app/app.routes.ts`

```typescript
import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { PeliculaListComponent } from './components/pelicula-list/pelicula-list.component';
import { PeliculaDetailComponent } from './components/pelicula-detail/pelicula-detail.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'peliculas', component: PeliculaListComponent },
  { path: 'pelicula/:id', component: PeliculaDetailComponent },
  { path: '**', redirectTo: '' }
];
```

1. `{ path: '', component: HomeComponent }`
   - Ruta raíz: `http://localhost:4200/`
   - Muestra el HomeComponent

2. `{ path: 'peliculas', component: PeliculaListComponent }`
   - URL: `http://localhost:4200/peliculas`
   - Muestra la lista de películas

3. `{ path: 'pelicula/:id', component: PeliculaDetailComponent }`
   - URL: `http://localhost:4200/pelicula/1`
   - `:id` es un parámetro dinámico
   - Muestra el detalle de la película con ese ID

4. `{ path: '**', redirectTo: '' }`
   - Ruta comodín para URLs no encontradas
   - Redirige al home

---

### Paso 2.4: Configurar el router-outlet

**Archivo**: `src/app/app.component.html`

```html
<nav>
  <a routerLink="/">Inicio</a>
  <a routerLink="/peliculas">Películas</a>
</nav>

<router-outlet />
```

- `routerLink` crea enlaces que funcionan con el router de Angular
- `<router-outlet />` es donde se muestran los componentes según la ruta

**Archivo**: `src/app/app.component.ts` (agregar imports)

```typescript
import { Component } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Gestor de Películas';
}
```

---

### Paso 2.5: Implementar el componente Home

**Archivo**: `src/app/components/home/home.component.html`

```html
<div>
  <h1>Bienvenido a Mi Colección de Películas</h1>
  <p>Usa el menú de navegación para explorar las películas.</p>
  <a routerLink="/peliculas">Ver catálogo de películas</a>
</div>
```

**Archivo**: `src/app/components/home/home.component.ts`

```typescript
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent { }
```

---

### Paso 2.6: Implementar lista de películas con datos hardcoded

Por ahora usaremos datos directamente en el componente.

**Archivo**: `src/app/components/pelicula-list/pelicula-list.component.ts`

```typescript
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Pelicula } from '../../models/pelicula';

@Component({
  selector: 'app-pelicula-list',
  templateUrl: './pelicula-list.component.html',
  styleUrl: './pelicula-list.component.css'
})
export class PeliculaListComponent implements OnInit {
  peliculas: Pelicula[] = [
    {
      id: 1,
      titulo: 'El Padrino',
      director: 'Francis Ford Coppola',
      anio: 1972,
      genero: 'Drama',
      vista: false,
      calificacion: 9.2
    },
    {
      id: 2,
      titulo: 'Pulp Fiction',
      director: 'Quentin Tarantino',
      anio: 1994,
      genero: 'Crimen',
      vista: true,
      calificacion: 8.9
    },
    {
      id: 3,
      titulo: 'Inception',
      director: 'Christopher Nolan',
      anio: 2010,
      genero: 'Ciencia Ficción',
      vista: false,
      calificacion: 8.8
    }
  ];

  // Inyectamos el Router para navegación programática
  constructor(private router: Router) { }

  ngOnInit(): void {
    // Por ahora no hacemos nada aquí
  }

  // Método para navegar al detalle
  verDetalle(id: number): void {
    this.router.navigate(['/pelicula', id]);
  }

  // Método para marcar/desmarcar como vista
  toggleVista(id: number): void {
    const pelicula = this.peliculas.find(p => p.id === id);
    if (pelicula) {
      pelicula.vista = !pelicula.vista;
    }
  }
}
```

**HTML básico**:

**Archivo**: `src/app/components/pelicula-list/pelicula-list.component.html`

```html
<div>
  <h1>Catálogo de Películas</h1>
  @for (pelicula of peliculas; track pelicula.id) {
    <div>
      <h3>{{ pelicula.titulo }}</h3>
      <p>Director: {{ pelicula.director }}</p>
      <p>Año: {{ pelicula.anio }}</p>
      <p>Género: {{ pelicula.genero }}</p>
      <p>Calificación: {{ pelicula.calificacion }}/10</p>
      <p>Estado: {{ pelicula.vista ? 'Vista' : 'No vista' }}</p>
      <button (click)="toggleVista(pelicula.id)">
        {{ pelicula.vista ? 'Marcar como no vista' : 'Marcar como vista' }}
      </button>
      <button (click)="verDetalle(pelicula.id)">Ver Detalle</button>
    </div>
  }
</div>
```

**Explicar**:
- `@for` es la nueva sintaxis de Angular para bucles (reemplaza `*ngFor`)
- `track pelicula.id` ayuda a Angular a optimizar el renderizado
- `this.router.navigate(['/pelicula', id])` navega a la ruta `/pelicula/1` (por ejemplo)
- Es útil cuando la navegación depende de lógica

---

### Paso 2.7: Leer parámetros de ruta

El componente de detalle necesita saber QUÉ película mostrar.

**Archivo**: `src/app/components/pelicula-detail/pelicula-detail.component.ts`

```typescript
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Pelicula } from '../../models/pelicula';

@Component({
  selector: 'app-pelicula-detail',
  templateUrl: './pelicula-detail.component.html',
  styleUrl: './pelicula-detail.component.css'
})
export class PeliculaDetailComponent implements OnInit {
  pelicula: Pelicula | undefined;

  // Datos hardcoded (los mismos que en la lista)
  private peliculas: Pelicula[] = [
    {
      id: 1,
      titulo: 'El Padrino',
      director: 'Francis Ford Coppola',
      anio: 1972,
      genero: 'Drama',
      vista: false,
      calificacion: 9.2
    },
    {
      id: 2,
      titulo: 'Pulp Fiction',
      director: 'Quentin Tarantino',
      anio: 1994,
      genero: 'Crimen',
      vista: true,
      calificacion: 8.9
    },
    {
      id: 3,
      titulo: 'Inception',
      director: 'Christopher Nolan',
      anio: 2010,
      genero: 'Ciencia Ficción',
      vista: false,
      calificacion: 8.8
    }
  ];

  constructor(
    private route: ActivatedRoute,      // Para leer parámetros
    private router: Router               // Para navegar
  ) { }

  ngOnInit(): void {
    // Leer el parámetro 'id' de la URL
    const id = Number(this.route.snapshot.paramMap.get('id'));

    // Buscar la película en el array
    this.pelicula = this.peliculas.find(p => p.id === id);
  }

  toggleVista(): void {
    if (this.pelicula) {
      this.pelicula.vista = !this.pelicula.vista;
    }
  }

  volver(): void {
    this.router.navigate(['/peliculas']);
  }
}
```

**HTML**:

```html
<div>
  @if (pelicula) {
    <h1>{{ pelicula.titulo }}</h1>
    <p>Director: {{ pelicula.director }}</p>
    <p>Año de estreno: {{ pelicula.anio }}</p>
    <p>Género: {{ pelicula.genero }}</p>
    <p>Calificación: {{ pelicula.calificacion }}/10</p>
    <p>Estado: {{ pelicula.vista ? 'Vista' : 'No vista' }}</p>

    <button (click)="toggleVista()">
      {{ pelicula.vista ? 'Marcar como no vista' : 'Marcar como vista' }}
    </button>
    <button (click)="volver()">Volver a la lista</button>
  } @else {
    <h2>Película no encontrada</h2>
    <button (click)="volver()">Volver a la lista</button>
  }
</div>
```

**Explicar paso por paso**:

1. **ActivatedRoute**:
   ```typescript
   const id = Number(this.route.snapshot.paramMap.get('id'));
   ```
   - `snapshot` es una foto del estado actual de la ruta
   - `paramMap.get('id')` obtiene el valor del parámetro `:id`
   - `Number()` convierte el string a número

2. **Control flow @if/@else**:
   - Muestra contenido diferente si la película existe o no
   - Reemplaza `*ngIf`

**Problema actual**: Los datos están duplicados en ambos componentes. Esto lo resolveremos con Servicios.

---

## 🔧 PARTE 3: SERVICIOS (Desde Cero)

**Problema identificado**: En la Parte 2 vimos que tenemos los datos duplicados en `PeliculaListComponent` y `PeliculaDetailComponent`. Esto no es una buena práctica.

**Solución**: Usar un **Servicio** para centralizar los datos y la lógica.

---

### Paso 3.1: Crear el servicio VACÍO

```bash
ng generate service services/pelicula
```

**Archivo**: `src/app/services/pelicula.service.ts` (inicial)

```typescript
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PeliculaService {

  constructor() { }
}
```

- `@Injectable()` indica que esta clase puede ser inyectada
- `providedIn: 'root'` significa que habrá UNA sola instancia en toda la app (Singleton)
- El servicio es el lugar donde manejamos la lógica de negocio y datos

---

### Paso 3.2: Agregar datos en memoria (hardcoded)

Moveremos los datos de los componentes al servicio.

**Archivo**: `src/app/services/pelicula.service.ts`

```typescript
import { Injectable } from '@angular/core';
import { Pelicula } from '../models/pelicula';

@Injectable({
  providedIn: 'root'
})
export class PeliculaService {
  // Array privado con datos de ejemplo
  private peliculas: Pelicula[] = [
    {
      id: 1,
      titulo: 'El Padrino',
      director: 'Francis Ford Coppola',
      anio: 1972,
      genero: 'Drama',
      vista: false,
      calificacion: 9.2
    },
    {
      id: 2,
      titulo: 'Pulp Fiction',
      director: 'Quentin Tarantino',
      anio: 1994,
      genero: 'Crimen',
      vista: true,
      calificacion: 8.9
    },
    {
      id: 3,
      titulo: 'Inception',
      director: 'Christopher Nolan',
      anio: 2010,
      genero: 'Ciencia Ficción',
      vista: false,
      calificacion: 8.8
    }
  ];

  constructor() { }
}
```

- `private peliculas` significa que solo el servicio puede acceder directamente
- Los datos están hardcoded por ahora (más tarde usaremos una API)

---

### Paso 3.3: Agregar métodos al servicio

Ahora creamos métodos para que los componentes puedan acceder a los datos.

**Archivo**: `src/app/services/pelicula.service.ts` (agregar métodos)

```typescript
import { Injectable } from '@angular/core';
import { Pelicula } from '../models/pelicula';

@Injectable({
  providedIn: 'root'
})
export class PeliculaService {
  private peliculas: Pelicula[] = [
    // ... (mismo array de arriba)
  ];

  constructor() { }

  // Método para obtener todas las películas
  getPeliculas(): Pelicula[] {
    return this.peliculas;
  }

  // Método para obtener una película por ID
  getPeliculaById(id: number): Pelicula | undefined {
    return this.peliculas.find(pelicula => pelicula.id === id);
  }

  // Método para cambiar el estado vista/no vista
  toggleVista(id: number): void {
    const pelicula = this.peliculas.find(pelicula => pelicula.id === id);
    if (pelicula) {
      pelicula.vista = !pelicula.vista;
    }
  }
}
```

- Los métodos son públicos para que los componentes puedan llamarlos
- `getPeliculas()` devuelve todas las películas
- `getPeliculaById()` busca una película específica
- `toggleVista()` cambia el estado de una película
- El tipo de retorno (`Pelicula[]`, `Pelicula | undefined`) ayuda a TypeScript a prevenir errores

---

### Paso 3.4: Refactorizar componentes para usar el servicio

Ahora QUITAMOS los datos hardcoded de los componentes y usamos el servicio.

**Archivo**: `src/app/components/pelicula-list/pelicula-list.component.ts` (REFACTORIZADO)

```typescript
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PeliculaService } from '../../services/pelicula.service';
import { Pelicula } from '../../models/pelicula';

@Component({
  selector: 'app-pelicula-list',
  templateUrl: './pelicula-list.component.html',
  styleUrl: './pelicula-list.component.css'
})
export class PeliculaListComponent implements OnInit {
  peliculas: Pelicula[] = [];

  // INYECCIÓN DE DEPENDENCIAS: Angular nos da las instancias
  constructor(
    private peliculaService: PeliculaService,
    private router: Router
  ) { }

  // Este método se ejecuta cuando el componente se inicializa
  ngOnInit(): void {
    // Llamamos al servicio para obtener las películas
    this.peliculas = this.peliculaService.getPeliculas();
  }

  // Método para navegar al detalle
  verDetalle(id: number): void {
    this.router.navigate(['/pelicula', id]);
  }

  // Método para marcar/desmarcar como vista
  toggleVista(id: number): void {
    this.peliculaService.toggleVista(id);
    // Actualizamos la lista
    this.peliculas = this.peliculaService.getPeliculas();
  }
}
```

**Cambios clave**:
- Ya NO tenemos el array de películas en el componente
- INYECTAMOS el servicio en el constructor
- Usamos `this.peliculaService.getPeliculas()` para obtener los datos
- El HTML permanece igual

---

**Refactorizar también el componente de detalle**:

**Archivo**: `src/app/components/pelicula-detail/pelicula-detail.component.ts` (REFACTORIZADO)

```typescript
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PeliculaService } from '../../services/pelicula.service';
import { Pelicula } from '../../models/pelicula';

@Component({
  selector: 'app-pelicula-detail',
  templateUrl: './pelicula-detail.component.html',
  styleUrl: './pelicula-detail.component.css'
})
export class PeliculaDetailComponent implements OnInit {
  pelicula: Pelicula | undefined;

  constructor(
    private route: ActivatedRoute,      // Para leer parámetros
    private router: Router,              // Para navegar
    private peliculaService: PeliculaService     // Para obtener datos
  ) { }

  ngOnInit(): void {
    // Leer el parámetro 'id' de la URL
    const id = Number(this.route.snapshot.paramMap.get('id'));

    // Obtener la película del servicio
    this.pelicula = this.peliculaService.getPeliculaById(id);
  }

  toggleVista(): void {
    if (this.pelicula) {
      this.peliculaService.toggleVista(this.pelicula.id);
      // Actualizar la película
      this.pelicula = this.peliculaService.getPeliculaById(this.pelicula.id);
    }
  }

  volver(): void {
    this.router.navigate(['/peliculas']);
  }
}
```

**Cambios clave**:
- Ya NO tenemos el array duplicado aquí
- Usamos `this.peliculaService.getPeliculaById(id)` para obtener la película
- Centralizamos la lógica en un solo lugar (el servicio)

**Resumen de PARTE 3 - Servicios**:
1. Creamos un servicio con `@Injectable`
2. Movimos los datos y lógica al servicio
3. Inyectamos el servicio en los componentes con Dependency Injection
4. Los componentes ahora son más simples y reutilizan el mismo servicio

---

## 🌐 PARTE 4: INTEGRACIÓN CON API

**Explicar**: Ahora vamos a cambiar el servicio para que consuma datos de una API real.

### Paso 4.1: Preparar la API de Películas

Vamos a usar nuestra propia **API de Películas (TareasAPI)** que ya hemos creado en .NET.

**Endpoints disponibles**:
- `GET /api/peliculas` - Obtener todas las películas
- `GET /api/peliculas/{id}` - Obtener una película por ID
- `POST /api/peliculas` - Crear una nueva película
- `PUT /api/peliculas/{id}` - Actualizar una película existente
- `PATCH /api/peliculas/{id}/cambiar-estado-vista` - Cambiar el estado vista/no vista
- `DELETE /api/peliculas/{id}` - Eliminar una película

**Estructura de datos que devuelve la API** (`PeliculaDto`):
```json
{
  "id": 1,
  "titulo": "El Padrino",
  "director": "Francis Ford Coppola",
  "anio": 1972,
  "genero": "Drama",
  "vista": false,
  "calificacion": 9.2
}
```

**Estructura para crear una película** (`CrearPeliculaDto`):
```json
{
  "titulo": "El Padrino",
  "director": "Francis Ford Coppola",
  "anio": 1972,
  "genero": "Drama",
  "calificacion": 9.2
}
```

---

### Paso 4.2: Levantar la API

Antes de continuar, asegúrate de tener la API corriendo:

```bash
cd TareasApi
dotnet run --project WebApi
```

La API estará disponible en: `http://localhost:5000` o `https://localhost:5001`

**Probar en el navegador**: Abre `https://localhost:5001/api/peliculas` para ver el listado de películas.

---

### Paso 4.3: Actualizar el servicio para usar HttpClient

**Archivo**: `src/app/services/pelicula.service.ts` (versión con API)

```typescript
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Pelicula } from '../models/pelicula';

@Injectable({
  providedIn: 'root'
})
export class PeliculaService {
  // URL base de nuestra API
  private apiUrl = 'http://localhost:5000/api/peliculas';

  // Inyectamos HttpClient
  constructor(private http: HttpClient) { }

  // Obtener todas las películas desde la API
  getPeliculas(): Observable<Pelicula[]> {
    return this.http.get<Pelicula[]>(this.apiUrl);
  }

  // Obtener una película por ID desde la API
  getPeliculaById(id: number): Observable<Pelicula> {
    return this.http.get<Pelicula>(`${this.apiUrl}/${id}`);
  }

  // Crear una nueva película
  crearPelicula(pelicula: Omit<Pelicula, 'id' | 'vista'>): Observable<Pelicula> {
    return this.http.post<Pelicula>(this.apiUrl, pelicula);
  }

  // Actualizar una película existente
  actualizarPelicula(id: number, pelicula: Omit<Pelicula, 'id' | 'vista'>): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, pelicula);
  }

  // Cambiar el estado vista/no vista de una película
  toggleVista(id: number): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${id}/cambiar-estado-vista`, {});
  }

  // Eliminar una película
  eliminarPelicula(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
```

**Paso por paso**:

1. **HttpClient**:
   ```typescript
   constructor(private http: HttpClient) { }
   ```
   - Angular proporciona HttpClient para hacer peticiones HTTP
   - Lo inyectamos como cualquier otro servicio

2. **Observable**:
   ```typescript
   getPeliculas(): Observable<Pelicula[]>
   ```
   - Las peticiones HTTP son asíncronas
   - `Observable` representa un flujo de datos que llegará en el futuro
   - Es como una promesa pero más potente

3. **Métodos HTTP**:
   - `http.get<T>()` - Realizar petición GET (obtener datos)
   - `http.post<T>()` - Realizar petición POST (crear)
   - `http.put<T>()` - Realizar petición PUT (actualizar completo)
   - `http.patch<T>()` - Realizar petición PATCH (actualizar parcial)
   - `http.delete<T>()` - Realizar petición DELETE (eliminar)

4. **Tipado con TypeScript**:
   - `http.get<Pelicula[]>()` le dice a TypeScript qué tipo de datos esperamos
   - Esto nos da autocompletado y detección de errores
   - La API devuelve exactamente el mismo formato que nuestro modelo `Pelicula`

5. **No necesitamos adaptadores**:
   - A diferencia del ejemplo anterior con JSONPlaceholder, nuestra API ya devuelve los datos en el formato correcto
   - Los nombres de las propiedades coinciden exactamente: `titulo`, `director`, `anio`, `genero`, `vista`, `calificacion`
   - Esto hace el código más simple y directo

---

### Paso 4.4: Configurar HttpClient en la app

**Archivo**: `src/app/app.config.ts`

```typescript
import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';  // IMPORTAR
import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient()  // AGREGAR ESTA LÍNEA
  ]
};
```

- `provideHttpClient()` habilita HttpClient en toda la aplicación
- Es necesario para que las peticiones HTTP funcionen

---

### Paso 4.5: Actualizar componentes para usar Observables

**Archivo**: `src/app/components/pelicula-list/pelicula-list.component.ts`

```typescript
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PeliculaService } from '../../services/pelicula.service';
import { Pelicula } from '../../models/pelicula';

@Component({
  selector: 'app-pelicula-list',
  templateUrl: './pelicula-list.component.html',
  styleUrl: './pelicula-list.component.css'
})
export class PeliculaListComponent implements OnInit {
  peliculas: Pelicula[] = [];
  cargando: boolean = false;

  constructor(
    private peliculaService: PeliculaService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.cargarPeliculas();
  }

  cargarPeliculas(): void {
    this.cargando = true;
    this.peliculaService.getPeliculas().subscribe({
      next: (peliculas) => {
        this.peliculas = peliculas;
        this.cargando = false;
      },
      error: (error) => {
        console.error('Error al cargar películas:', error);
        this.cargando = false;
      }
    });
  }

  verDetalle(id: number): void {
    this.router.navigate(['/pelicula', id]);
  }

  toggleVista(id: number): void {
    this.peliculaService.toggleVista(id).subscribe({
      next: () => {
        // Recargar la lista después de cambiar el estado
        this.cargarPeliculas();
      },
      error: (error) => {
        console.error('Error al cambiar estado:', error);
      }
    });
  }

  eliminarPelicula(id: number): void {
    if (confirm('¿Estás seguro de que deseas eliminar esta película?')) {
      this.peliculaService.eliminarPelicula(id).subscribe({
        next: () => {
          // Recargar la lista después de eliminar
          this.cargarPeliculas();
        },
        error: (error) => {
          console.error('Error al eliminar película:', error);
        }
      });
    }
  }
}
```

**Subscribe - Explicación detallada**:

```typescript
this.peliculaService.getPeliculas().subscribe({
  next: (peliculas) => {
    // Este código se ejecuta cuando llegan los datos
    this.peliculas = peliculas;
  },
  error: (error) => {
    // Este código se ejecuta si hay un error
    console.error('Error:', error);
  }
});
```

- `subscribe()` activa la petición HTTP
- Sin `subscribe()`, la petición no se ejecuta (los Observables son "lazy")
- `next`: función que se ejecuta cuando llegan los datos exitosamente
- `error`: función que se ejecuta si algo sale mal (error de red, error 404, etc.)

**Cambios importantes**:
- Agregamos un método `cargarPeliculas()` para reutilizar la lógica
- `toggleVista()` ahora llama a la API y recarga la lista
- `eliminarPelicula()` elimina de la base de datos y recarga la lista
- Agregamos una variable `cargando` para mostrar feedback al usuario

---

**Archivo**: `src/app/components/pelicula-detail/pelicula-detail.component.ts`

```typescript
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PeliculaService } from '../../services/pelicula.service';
import { Pelicula } from '../../models/pelicula';

@Component({
  selector: 'app-pelicula-detail',
  templateUrl: './pelicula-detail.component.html',
  styleUrl: './pelicula-detail.component.css'
})
export class PeliculaDetailComponent implements OnInit {
  pelicula: Pelicula | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private peliculaService: PeliculaService
  ) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    // Suscribirse al Observable
    this.peliculaService.getPeliculaById(id).subscribe({
      next: (pelicula) => {
        this.pelicula = pelicula;
      },
      error: (error) => {
        console.error('Error al cargar película:', error);
      }
    });
  }

  volver(): void {
    this.router.navigate(['/peliculas']);
  }
}
```

---

### Paso 4.6: Configurar CORS en la API .NET

**Problema común**: Al intentar consumir la API desde Angular, puedes encontrar errores de CORS (Cross-Origin Resource Sharing).

**Solución**: Ya debería estar configurado en tu API, pero verifica que el archivo `Program.cs` incluya:

```csharp
// Agregar antes de var app = builder.Build();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Agregar después de var app = builder.Build();
app.UseCors("AllowAngular");
```

**Explicar**:
- CORS es un mecanismo de seguridad del navegador
- Por defecto, las APIs no permiten peticiones desde otros orígenes (dominios)
- `AllowAngular` es el nombre de nuestra política
- `WithOrigins("http://localhost:4200")` permite peticiones solo desde Angular
- `AllowAnyHeader()` y `AllowAnyMethod()` permiten todos los headers y métodos HTTP

---

### Paso 4.7: Ejemplo completo - Crear una nueva película

Ahora vamos a agregar funcionalidad para crear películas desde Angular.

**Paso 1**: Crear un componente para el formulario

```bash
ng generate component components/pelicula-form
```

**Archivo**: `src/app/components/pelicula-form/pelicula-form.component.ts`

```typescript
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PeliculaService } from '../../services/pelicula.service';
import { Pelicula } from '../../models/pelicula';

@Component({
  selector: 'app-pelicula-form',
  imports: [FormsModule],
  templateUrl: './pelicula-form.component.html',
  styleUrl: './pelicula-form.component.css'
})
export class PeliculaFormComponent {
  // Modelo del formulario
  pelicula = {
    titulo: '',
    director: '',
    anio: new Date().getFullYear(),
    genero: '',
    calificacion: 5.0
  };

  constructor(
    private peliculaService: PeliculaService,
    private router: Router
  ) { }

  guardar(): void {
    this.peliculaService.crearPelicula(this.pelicula).subscribe({
      next: (peliculaCreada) => {
        console.log('Película creada:', peliculaCreada);
        // Navegar a la lista de películas
        this.router.navigate(['/peliculas']);
      },
      error: (error) => {
        console.error('Error al crear película:', error);
      }
    });
  }

  cancelar(): void {
    this.router.navigate(['/peliculas']);
  }
}
```

**Archivo**: `src/app/components/pelicula-form/pelicula-form.component.html`

```html
<div>
  <h2>Nueva Película</h2>
  <form (ngSubmit)="guardar()">
    <div>
      <label>Título:</label>
      <input type="text" [(ngModel)]="pelicula.titulo" name="titulo" required>
    </div>

    <div>
      <label>Director:</label>
      <input type="text" [(ngModel)]="pelicula.director" name="director" required>
    </div>

    <div>
      <label>Año:</label>
      <input type="number" [(ngModel)]="pelicula.anio" name="anio" required>
    </div>

    <div>
      <label>Género:</label>
      <input type="text" [(ngModel)]="pelicula.genero" name="genero" required>
    </div>

    <div>
      <label>Calificación (0-10):</label>
      <input type="number" [(ngModel)]="pelicula.calificacion" name="calificacion"
             min="0" max="10" step="0.1" required>
    </div>

    <button type="submit">Guardar</button>
    <button type="button" (click)="cancelar()">Cancelar</button>
  </form>
</div>
```

**Paso 2**: Agregar la ruta

**Archivo**: `src/app/app.routes.ts`

```typescript
import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { PeliculaListComponent } from './components/pelicula-list/pelicula-list.component';
import { PeliculaDetailComponent } from './components/pelicula-detail/pelicula-detail.component';
import { PeliculaFormComponent } from './components/pelicula-form/pelicula-form.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'peliculas', component: PeliculaListComponent },
  { path: 'peliculas/nueva', component: PeliculaFormComponent },  // ← Nueva ruta
  { path: 'pelicula/:id', component: PeliculaDetailComponent },
  { path: '**', redirectTo: '' }
];
```

**IMPORTANTE**: La ruta `peliculas/nueva` debe ir ANTES de `pelicula/:id` para evitar que Angular interprete "nueva" como un ID.

**Paso 3**: Agregar enlace en la lista

**Archivo**: `src/app/components/pelicula-list/pelicula-list.component.html` (agregar al inicio)

```html
<div>
  <h1>Catálogo de Películas</h1>
  <button routerLink="/peliculas/nueva">➕ Agregar Nueva Película</button>

  <!-- ... resto del HTML ... -->
</div>
```

**Explicación del formulario**:

1. **FormsModule**:
   - Necesario para usar `[(ngModel)]` (two-way data binding)
   - Se importa en el componente con standalone imports

2. **ngModel**:
   ```html
   <input [(ngModel)]="pelicula.titulo" name="titulo">
   ```
   - `[(ngModel)]` crea un binding bidireccional
   - Cuando el usuario escribe, `pelicula.titulo` se actualiza automáticamente
   - Requiere el atributo `name`

3. **ngSubmit**:
   ```html
   <form (ngSubmit)="guardar()">
   ```
   - Se ejecuta cuando se envía el formulario
   - Previene el comportamiento por defecto del formulario

4. **POST a la API**:
   ```typescript
   this.peliculaService.crearPelicula(this.pelicula).subscribe(...)
   ```
   - Envía los datos del formulario a la API
   - La API crea la película en la base de datos
   - Al recibir respuesta exitosa, navegamos a la lista

---

## 🔗 Recursos Adicionales

- [Documentación oficial de Angular](https://angular.dev)
- [RxJS para principiantes](https://rxjs.dev/guide/overview)
- [Angular Router Guide](https://angular.dev/guide/routing)
- [Angular Forms Guide](https://angular.dev/guide/forms)
- [ASP.NET Core CORS](https://learn.microsoft.com/en-us/aspnet/core/security/cors)

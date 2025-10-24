import { Injectable } from '@angular/core';
import { Pelicula } from '../models/pelicula';

@Injectable({
  providedIn: 'root'
})
export class PeliculaService {
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

  // Obtener todas las películas
  getPeliculas(): Pelicula[] {
    return this.peliculas;
  }

  // Obtener una película por ID
  getPeliculaById(id: number): Pelicula | undefined {
    return this.peliculas.find(pelicula => pelicula.id === id);
  }

  // Marcar/desmarcar como vista
  toggleVista(id: number): void {
    const pelicula = this.peliculas.find(p => p.id === id);
    if (pelicula) {
      pelicula.vista = !pelicula.vista;
    }
  }
}

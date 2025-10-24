import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Pelicula } from '../../models/pelicula';

@Component({
  selector: 'app-pelicula-list',
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './pelicula-list.component.html',
  styleUrl: './pelicula-list.component.css'
})
export class PeliculaListComponent {
  peliculas: Pelicula[] = [
    {
      id: 1,
      titulo: 'Inception',
      director: 'Christopher Nolan',
      anio: 2010,
      genero: 'Ciencia Ficción',
      vista: true,
      calificacion: 9
    },
    {
      id: 2,
      titulo: 'The Shawshank Redemption',
      director: 'Frank Darabont',
      anio: 1994,
      genero: 'Drama',
      vista: false,
      calificacion: 10
    },
    {
      id: 3,
      titulo: 'The Dark Knight',
      director: 'Christopher Nolan',
      anio: 2008,
      genero: 'Acción',
      vista: true,
      calificacion: 9
    },
    {
      id: 4,
      titulo: 'Pulp Fiction',
      director: 'Quentin Tarantino',
      anio: 1994,
      genero: 'Crimen',
      vista: false,
      calificacion: 8
    },
    {
      id: 5,
      titulo: 'The Matrix',
      director: 'Lana Wachowski',
      anio: 1999,
      genero: 'Ciencia Ficción',
      vista: true,
      calificacion: 9
    }
  ];

  constructor(private router: Router) { }

  verDetalle(id: number): void {
    
  }

  toggleVista(id: number): void {
    const pelicula = this.peliculas.find(p => p.id === id);
    if (pelicula) {
      pelicula.vista = !pelicula.vista;
    }
  }
}


import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { PeliculaService } from '../../services/pelicula.service';
import { Pelicula } from '../../models/pelicula';

@Component({
  selector: 'app-pelicula-list',
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './pelicula-list.component.html',
  styleUrl: './pelicula-list.component.css'
})
export class PeliculaListComponent implements OnInit {
  peliculas: Pelicula[] = [];

  constructor(
    private peliculaService: PeliculaService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.peliculas = this.peliculaService.getPeliculas();
  }

  verDetalle(id: number): void {
    this.router.navigate(['/pelicula', id]);
  }

  toggleVista(id: number): void {
    this.peliculaService.toggleVista(id);
    this.peliculas = this.peliculaService.getPeliculas();
  }
}


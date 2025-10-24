import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { PeliculaService } from '../../services/pelicula.service';
import { Pelicula } from '../../models/pelicula';

@Component({
  selector: 'app-pelicula-detail',
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
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
    this.pelicula = this.peliculaService.getPeliculaById(id);
  }

  toggleVista(): void {
    if (this.pelicula) {
      this.peliculaService.toggleVista(this.pelicula.id);
      this.pelicula = this.peliculaService.getPeliculaById(this.pelicula.id);
    }
  }

  volver(): void {
    this.router.navigate(['/peliculas']);
  }
}

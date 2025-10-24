import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Pelicula } from '../../models/pelicula';

@Component({
  selector: 'app-pelicula-detail',
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './pelicula-detail.component.html',
  styleUrl: './pelicula-detail.component.css'
})
export class PeliculaDetailComponent {
  pelicula: Pelicula | undefined;

  constructor() { }


  toggleVista(): void {
    if (this.pelicula) {
      
    }
  }

  volver(): void {
    
  }
}

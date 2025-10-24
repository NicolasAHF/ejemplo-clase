import { Routes } from '@angular/router';
import { PeliculaListComponent } from './components/pelicula-list/pelicula-list.component';
import { PeliculaDetailComponent } from './components/pelicula-detail/pelicula-detail.component';
import { HomeComponent } from './components/home/home.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'peliculas', component: PeliculaListComponent },
  { path: 'pelicula/:id', component: PeliculaDetailComponent },
  { path: '**', redirectTo: '' }
];

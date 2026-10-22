import { Routes } from '@angular/router';
import { HomePage } from './home/pages/home-page/home-page';

export const routes: Routes = [
  {
    path: 'home',
    pathMatch: 'full',
    component: HomePage
  },
  {
    path: '**',
    redirectTo: 'home'
  }
];

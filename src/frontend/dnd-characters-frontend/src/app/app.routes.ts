import { Routes } from '@angular/router';
import { HomePage } from './home/pages/home-page/home-page';
import { CharactersListPage } from './characters/pages/characters-list-page/characters-list-page';
import { WeaponsListPage } from './weapons/pages/weapons-list-page/weapons-list-page';
import { WeaponDetailPage } from './weapons/pages/weapon-detail-page/weapon-detail-page';

export const routes: Routes = [
  {
    path: 'home',
    pathMatch: 'full',
    component: HomePage
  },
  {
    path: 'characters',
    pathMatch: 'full',
    component: CharactersListPage
  },
  {
    path: 'weapons',
    pathMatch: 'full',
    component: WeaponsListPage
  },
  {
    path: 'weapons/:id',
    pathMatch: 'full',
    component: WeaponDetailPage
  },
  {
    path: '**',
    redirectTo: 'home'
  }
];

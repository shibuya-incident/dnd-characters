import { Routes } from '@angular/router';
import { HomePage } from './home/pages/home-page/home-page';
import { CharactersListPage } from './characters/pages/characters-list-page/characters-list-page';
import { WeaponsListPage } from './weapons/pages/weapons-list-page/weapons-list-page';
import { WeaponDetailPage } from './weapons/pages/weapon-detail-page/weapon-detail-page';
import { ShopDetailPage } from './shops/pages/shop-detail-page/shop-detail-page';
import { ShopsListPage } from './shops/pages/shops-list-page/shops-list-page';
import { CharacterDetailPage } from './characters/pages/character-detail-page/character-detail-page';
import { ItemsListPage } from './items/pages/items-list-page/items-list-page';
import { ItemDetailPage } from './items/pages/item-detail-page/item-detail-page';

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
    path: 'characters/:id',
    pathMatch: 'full',
    component: CharacterDetailPage
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
    path: 'shops',
    pathMatch: 'full',
    component: ShopsListPage
  },
  {
    path: 'shops/:id',
    pathMatch: 'full',
    component: ShopDetailPage
  },
  {
    path: 'items',
    pathMatch: 'full',
    component: ItemsListPage
  },
  {
    path: 'items/:id',
    pathMatch: 'full',
    component: ItemDetailPage
  },
  {
    path: '**',
    redirectTo: 'home'
  }
];

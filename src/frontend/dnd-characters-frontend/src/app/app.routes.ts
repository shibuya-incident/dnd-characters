import { Routes } from '@angular/router';
import { Home } from './home/pages/home/home-page';
import { CharactersListPage } from './characters/pages/characters-list-page/characters-list-page';

export const routes: Routes = [
    {
        path: 'home',
        pathMatch: 'full',
        component: Home
    },
    {
        path: 'characters',
        pathMatch: 'full',
        component: CharactersListPage
    },
    {
        path: '**',
        redirectTo: 'home'
    },
];

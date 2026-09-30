import { Component, signal } from '@angular/core';
import { MainLayout } from './layouts/main-layout/main-layout';

@Component({
  imports: [MainLayout],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App {
  protected readonly title = signal('dnd-characters-frontend');
}

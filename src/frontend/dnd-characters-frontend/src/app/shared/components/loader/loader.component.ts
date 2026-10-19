import { Component, input } from '@angular/core';

@Component({
  imports: [],
  selector: 'app-loader',
  styleUrl: './loader.component.css',
  templateUrl: './loader.component.html',
})
export class LoaderComponent {
   readonly message = input('Loading something lovely…');
}

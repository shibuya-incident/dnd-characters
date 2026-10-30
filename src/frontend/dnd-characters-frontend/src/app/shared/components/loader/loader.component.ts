import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-loader',
    standalone: true,
    imports: [],
    templateUrl: './loader.component.html',
    styleUrl: './loader.component.css'
})
export class Loader {
    @Input() message = 'Preparando la aventura...';
    @Input() fullScreen = false;
}

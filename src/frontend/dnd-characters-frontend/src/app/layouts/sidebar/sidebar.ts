import { Component, Input, Output, EventEmitter } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
    imports: [RouterLink],
    selector: 'app-sidebar',
    styleUrl: './sidebar.css',
    templateUrl: './sidebar.html',
})
export class Sidebar {

    @Input() collapsed = false;

    @Output() toggleSidebar = new EventEmitter<void>();

    onToggleSidebar() {
        this.toggleSidebar.emit();
    }
}
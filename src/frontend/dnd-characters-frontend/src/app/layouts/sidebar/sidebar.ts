import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
    imports: [],
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
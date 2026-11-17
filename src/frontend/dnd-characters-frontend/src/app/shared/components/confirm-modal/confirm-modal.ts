import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  imports: [],
  standalone: true,
  selector: 'app-confirm-modal',
  styleUrl: './confirm-modal.css',
  templateUrl: './confirm-modal.html',
})
export class ConfirmModal {

  @Input() title: string = 'Are you sure?';
  @Input() message: string = 'Do you want to continue?';

  @Input() confirmText: string = 'Confirm';
  @Input() cancelText: string = 'Cancel';

  @Output() confirmed = new EventEmitter<void>();
  @Output() cancelled = new EventEmitter<void>();

}

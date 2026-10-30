import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ItemsService } from '../../items-service';
import { PagedListResponse } from '../../../shared/models/pagination/paged-list-response';
import { GetItemsResponse } from '../../models/get-items/get-items-response.model';
import { Loader } from '../../../shared/components/loader/loader.component';
import { DatePipe } from '@angular/common';

@Component({
  imports: [Loader, RouterLink, DatePipe],
  selector: 'app-items-list-page',
  styleUrl: './items-list-page.css',
  templateUrl: './items-list-page.html',
})
export class ItemsListPage {
  readonly itemsService: ItemsService = inject(ItemsService);

  readonly isLoading = signal<boolean>(false);
  readonly error = signal<string | null>(null);

  readonly items = signal<PagedListResponse<GetItemsResponse> | null>(null);

  ngOnInit(){
    this.loadItems();
  }
  
  loadItems() {
    if (this.isLoading()){
      return;
    }

    this.isLoading.set(true);
    this.itemsService.getItems()
    .subscribe({
      next: (response) => {
        this.items.set(response);
        this.isLoading.set(false);
      },
      error: () => {
        this.error.set('There are no items available... yet')
        this.isLoading.set(false);
      }
    });
  }
}

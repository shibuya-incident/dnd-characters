import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ShopsService } from '../../shops.service';
import { PagedListResponse } from '../../../shared/models/pagination/paged-list-response';
import { GetShopsResponse } from '../../models/get-shops/get-shops-response.model';
import { Loader } from '../../../shared/components/loader/loader.component';
import { DatePipe } from '@angular/common';

@Component({
  imports: [Loader, RouterLink, DatePipe],
  selector: 'app-shops-list-page',
  styleUrl: './shops-list-page.css',
  templateUrl: './shops-list-page.html',
})
export class ShopsListPage {
  readonly shopsService: ShopsService = inject(ShopsService);

  readonly isLoading = signal<boolean>(false);
  readonly error = signal<string | null>(null);

  readonly shops = signal<PagedListResponse<GetShopsResponse> | null>(null);

  ngOnInit(){
    this.loadShops();
  }
  
  loadShops() {
    if (this.isLoading()){
      return;
    }

    this.isLoading.set(true);
    this.shopsService.getShops()
    .subscribe({
      next: (response) => {
        this.shops.set(response);
        this.isLoading.set(false);
      },
      error: () => {
        this.error.set('There are no shops available... yet')
        this.isLoading.set(false);
      }
    });
  }
}

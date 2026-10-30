import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { WeaponsService } from '../../weapons-service'
import { PagedListResponse } from '../../../shared/models/pagination/paged-list-response';
import { GetWeaponsResponse } from '../../models/get-weapons/get-weapons-response.model';
import { Loader } from '../../../shared/components/loader/loader.component';
import { DatePipe } from '@angular/common';

@Component({
  imports: [Loader, RouterLink, DatePipe],
  selector: 'app-weapons-list-page',
  styleUrl: './weapons-list-page.css',
  templateUrl: './weapons-list-page.html',
})
export class WeaponsListPage {
  readonly weaponsService: WeaponsService = inject(WeaponsService);

  readonly isLoading = signal<boolean>(false);
  readonly error = signal<string | null>(null);

  readonly weapons = signal<PagedListResponse<GetWeaponsResponse> | null>(null);

  ngOnInit(){
    this.loadWeapons();
  }
  
  loadWeapons() {
    if (this.isLoading()){
      return;
    }

    this.isLoading.set(true);
    this.weaponsService.getWeapons()
    .subscribe({
      next: (response) => {
        this.weapons.set(response);
        this.isLoading.set(false);
      },
      error: () => {
        this.error.set('There are no weapons available... yet')
        this.isLoading.set(false);
      }
    });
  }
}

import { Component, computed, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { WeaponsService } from '../../weapons-service'
import { PagedListResponse } from '../../../shared/models/pagination/paged-list-response';
import { GetWeaponsResponse } from '../../models/get-weapons/get-weapons-response.model';
import { Loader } from '../../../shared/components/loader/loader.component';
import { DatePipe } from '@angular/common';
import { GetWeaponsSortBy } from '../../models/get-weapons/get-weapons-sort-by';
import { SortDirection } from '../../../shared/models/pagination/sort-direction.enum';
import { GetWeaponsRequest } from '../../models/get-weapons/get-weapons-request.model';

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

  readonly request = signal<GetWeaponsRequest>({
    page: 1,
    pageSize: 10,
    sortBy: GetWeaponsSortBy.CreatedAt,
    sortDirection: SortDirection.Descending,
  });

  readonly totalPages = computed(() => {
    const totalCount = this.weapons()?.totalCount ?? 0;
    return Math.ceil(totalCount / this.request().pageSize);
  });

  ngOnInit() {
    this.loadWeapons();
  }

  loadWeapons() {
    if (this.isLoading()) {
      return;
    }

    this.isLoading.set(true);
    this.weaponsService.getWeapons(this.request())
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
  goToPage(page: number): void {
    if (page < 1 || page === this.request().page) return;

    this.updateRequest({ page });
  }

  private updateRequest(patch: Partial<GetWeaponsRequest>): void {
    if (this.isLoading()) return;

    this.request.update(current => ({
      ...current,
      ...patch,
    }));

    this.loadWeapons();
  }




}

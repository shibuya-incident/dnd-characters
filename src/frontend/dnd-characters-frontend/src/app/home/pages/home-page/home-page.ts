import { Component, inject, signal } from '@angular/core';
import { HomeService } from '../../home.service';
import { GetHomeCountersResponse } from '../../models/get-home-counters-response.model';
import { Loader } from '../../../shared/components/loader/loader.component';
import { DecimalPipe } from '@angular/common';


@Component({
  imports: [Loader, DecimalPipe],
  standalone: true,
  selector: 'app-home-page',
  styleUrl: './home-page.css',
  templateUrl: './home-page.html',
})
export class HomePage {
  readonly homeService: HomeService = inject(HomeService);

  readonly isLoading = signal<boolean>(false);
  readonly error = signal<string | null>(null);

  readonly counters = signal<GetHomeCountersResponse>({
    charactersCount: 0,
    weaponsCount: 0,
    shopsCount: 0,
    itemsCount: 0,
  });

  ngOnInit() {
    this.loadCounters();
  }
  
  loadCounters() {
    if (this.isLoading()) {
      return;
    }

    this.isLoading.set(true);
    this.homeService.getCounters()
    .subscribe({
      next: (response) => {
        this.counters.set(response);
        this.isLoading.set(false);
      },
      error: () => {
        this.error.set('Failed to load counters. Please try again later.');
        this.isLoading.set(false);
      }
    });

  }
}

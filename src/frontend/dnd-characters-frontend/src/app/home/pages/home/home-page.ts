import { Component, inject, signal } from '@angular/core';
import { HomeService } from '../../home.service';
import { LoaderComponent } from '../../../shared/components/loader/loader.component';
import { delay } from 'rxjs';
import { GetHomeCountersResponse } from '../../models/get-home-counters-response.model';

@Component({
  imports: [LoaderComponent],
  selector: 'app-home',
  styleUrl: './home-page.css',
  templateUrl: './home-page.html',
})
export class Home {
  readonly homeService = inject(HomeService);

  readonly isLoading = signal<boolean>(false);
  readonly error = signal<string | null>(null);

  readonly counts = signal<GetHomeCountersResponse>({
    charactersCount: 0,
    weaponsCount: 0,
    itemsCount: 0,
    shopsCount: 0
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
        this.counts.set(response);
         this.isLoading.set(false);
      },
      error: () => {
        this.error.set('Failed to load counters. Please try again later.');
         this.isLoading.set(false);
      }
    });
  }

}



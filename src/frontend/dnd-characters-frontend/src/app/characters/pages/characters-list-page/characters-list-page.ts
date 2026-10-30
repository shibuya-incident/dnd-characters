import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CharactersService } from '../../characters.service';
import { PagedListResponse } from '../../../shared/models/pagination/paged-list-response';
import { GetCharactersResponse } from '../../models/get-characters/get-characters-response.model';
import { Loader } from '../../../shared/components/loader/loader.component';
import { DatePipe } from '@angular/common';

@Component({
  imports: [Loader, RouterLink, DatePipe],
  selector: 'app-characters-list-page',
  styleUrl: './characters-list-page.css',
  templateUrl: './characters-list-page.html',
})
export class CharactersListPage {
  readonly charactersService: CharactersService = inject(CharactersService);

  readonly isLoading = signal<boolean>(false);
  readonly error = signal<string | null>(null);

  readonly characters = signal<PagedListResponse<GetCharactersResponse> | null>(null);

  ngOnInit(){
    this.loadCharacters();
  }
  
  loadCharacters() {
    if (this.isLoading()){
      return;
    }

    this.isLoading.set(true);
    this.charactersService.getCharacters()
    .subscribe({
      next: (response) => {
        this.characters.set(response);
        this.isLoading.set(false);
      },
      error: () => {
        this.error.set('There are no characters available... yet')
        this.isLoading.set(false);
      }
    });
  }
}

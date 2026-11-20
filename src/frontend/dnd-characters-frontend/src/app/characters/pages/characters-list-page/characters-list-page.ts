import { Component, computed, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CharactersService } from '../../characters.service';
import { PagedListResponse } from '../../../shared/models/pagination/paged-list-response';
import { GetCharactersResponse } from '../../models/get-characters/get-characters-response.model';
import { Loader } from '../../../shared/components/loader/loader.component';
import { DatePipe } from '@angular/common';
import { GetCharactersSortBy } from '../../models/get-characters/get-characters-sort-by';
import { SortDirection } from '../../../shared/models/pagination/sort-direction.enum';
import { GetCharactersRequest } from '../../models/get-characters/get-characters-request.model';
import { ConfirmModal } from '../../../shared/components/confirm-modal/confirm-modal';

@Component({
  imports: [Loader, RouterLink, DatePipe, ConfirmModal],
  selector: 'app-characters-list-page',
  styleUrl: './characters-list-page.css',
  templateUrl: './characters-list-page.html',
})
export class CharactersListPage {
  readonly charactersService: CharactersService = inject(CharactersService);

  readonly isLoading = signal<boolean>(false);
  readonly error = signal<string | null>(null);

  readonly characters = signal<PagedListResponse<GetCharactersResponse> | null>(null);

  readonly request = signal<GetCharactersRequest>({
    page: 1,
    pageSize: 5,
    sortBy: GetCharactersSortBy.UpdatedAt,
    sortDirection: SortDirection.Descending
  });

  readonly totalPages = computed(() => {
    const totalCount = this.characters()?.totalCount ?? 0;
    return Math.ceil(totalCount / this.request().pageSize);
  }); 

  readonly SortDirection = SortDirection;

  readonly GetCharactersSortBy = GetCharactersSortBy;

  showDeleteModal = signal<boolean>(false);

  selectedCharacterId = signal<number | null>(null);

  ngOnInit(){
    this.loadCharacters();
  }
  
  loadCharacters() {
    if (this.isLoading()){
      return;
    }

    this.isLoading.set(true);
    this.charactersService.getCharacters(this.request())
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

  goToPage(page: number): void {
      if (page < 1 || page === this.request().page) return;
  
      this.updateRequest({ page });
    }
  
    private updateRequest(patch: Partial<GetCharactersRequest>): void {
      if (this.isLoading()) return;
  
      this.request.update((currentRequest) => ({
        ...currentRequest,
        ...patch
      }));
  
      this.loadCharacters();
    }
  
  changeSort(sortBy: GetCharactersSortBy): void {
    if (this.isLoading()) return;

    this.request.update(current => ({
      ...current,
      sortBy,
      sortDirection:
        current.sortBy === sortBy && current.sortDirection === SortDirection.Ascending 
          ? SortDirection.Descending
          : SortDirection.Ascending,
      page: 1
    }));

    this.loadCharacters();
  }

  deleteCharacter(characterId: number): void {
    if (this.isLoading()) return;
    
    this.isLoading.set(true);
    
    this.charactersService.deleteCharacter({ id: characterId })
      .subscribe({
        next: () => {
          this.isLoading.set(false);
          this.loadCharacters();
        },
        error: () => {
          this.error.set('Failed to delete character.');
          this.isLoading.set(false);
        }
      });
  }

  confirmDelete(characterId: number): void {
    this.selectedCharacterId.set(characterId);
    this.showDeleteModal.set(true);
  }

  cancelDelete(): void {
    this.showDeleteModal.set(false);
    this.selectedCharacterId.set(null);
  }

  executeDelete(): void {
    const characterId = this.selectedCharacterId();

    if (characterId === null) return;

    this.showDeleteModal.set(false);
    this.selectedCharacterId.set(null);

    this.deleteCharacter(characterId);
  }
}

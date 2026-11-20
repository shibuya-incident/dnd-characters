import { Component, computed, inject, signal } from '@angular/core';
import { CharactersService } from '../../characters.service';
import { GetCharacterByIdResponse } from '../../models/get-character-by-id/get-character-by-id-response.model';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Loader } from '../../../shared/components/loader/loader.component';
import { DatePipe } from '@angular/common';
import { WeaponsService } from '../../../weapons/weapons-service';
import { GetWeaponsResponse } from '../../../weapons/models/get-weapons/get-weapons-response.model';
import { PagedListResponse } from '../../../shared/models/pagination/paged-list-response';
import { GetWeaponsRequest } from '../../../weapons/models/get-weapons/get-weapons-request.model';
import { GetWeaponsSortBy } from '../../../weapons/models/get-weapons/get-weapons-sort-by';
import { SortDirection } from '../../../shared/models/pagination/sort-direction.enum';

@Component({
  imports: [Loader, DatePipe, RouterLink],
  selector: 'app-character-detail-page',
  styleUrl: './character-detail-page.css',
  templateUrl: './character-detail-page.html',
})
export class CharacterDetailPage {

  readonly charactersService: CharactersService = inject(CharactersService);

  readonly weaponsService: WeaponsService = inject(WeaponsService);

  readonly isLoadingCharacter = signal<boolean>(false);

  readonly isLoadingWeapons = signal<boolean>(false);

  readonly error = signal<string | null>(null);

  readonly character = signal<GetCharacterByIdResponse | null>(null);

  readonly weapons = signal<PagedListResponse<GetWeaponsResponse> | null>(null);

  private route = inject(ActivatedRoute);

  readonly characterId = Number(this.route.snapshot.paramMap.get('id')) || null;

  readonly weaponCount = computed(() => this.weapons()?.items.length ?? 0);

  readonly request = signal<GetWeaponsRequest>({
      page: 1,
      pageSize: 5,
      sortBy: GetWeaponsSortBy.Name,
      sortDirection: SortDirection.Descending
    });

  ngOnInit() {

    if (this.characterId === null) {
      return;
    }

    this.request.update(request => ({
      ...request,
      characterId : this.characterId ?? undefined
    }));

    this.loadCharacter(this.characterId);
    this.loadWeapons();
  }

  loadCharacter(id: number) {
    if (this.isLoadingCharacter()) {
      return;
    }

    this.isLoadingCharacter.set(true);

    this.charactersService.getCharacterById({ id })
      .subscribe({
        next: (response) => {
          this.character.set(response);
          this.isLoadingCharacter.set(false);
        },
        error: () => {
          this.error.set('Failed to load character details.');
          this.isLoadingCharacter.set(false);
        }
      });
  }

  loadWeapons() {
    if (this.isLoadingWeapons()){
      return;
    }

    this.isLoadingWeapons.set(true);
    this.weaponsService.getWeapons(this.request())
    .subscribe({
      next: (response) => {
        this.weapons.set(response);
        this.isLoadingWeapons.set(false);
      },
      error: () => {
        this.error.set('Something went wrong')
        this.isLoadingWeapons.set(false);
      }
    });
  }
}

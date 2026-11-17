import { Component, inject, signal } from '@angular/core';
import { Loader } from '../../../shared/components/loader/loader.component';
import { WeaponsService } from '../../weapons-service';
import { ActivatedRoute } from '@angular/router';
import { CreateWeaponRequest } from '../../models/create-weapon/create-weapon-request.model';
import { UpdateWeaponRequest } from '../../models/update-weapon/update-weapon-request.model';
import { CreateWeaponResponse } from '../../models/create-weapon/create-weapon-response.model';
import { UpdateWeaponResponse } from '../../models/update-weapon/update-weapon-response.model';

@Component({
  imports: [Loader],
  selector: 'app-weapon-form-page',
  styleUrl: './weapon-form-page.css',
  templateUrl: './weapon-form-page.html',
})
export class WeaponFormPage {

  readonly weaponsService: WeaponsService = inject(WeaponsService);

  readonly isLoading = signal<boolean>(false);

  readonly error = signal<string | null>(null);

  readonly weapon = signal<CreateWeaponResponse | UpdateWeaponResponse | null>(null);

  createRequest = signal<CreateWeaponRequest>({
    name: '',
    description: '',
    weight: 0,
    properties: '',
    type: null as any,
    displayImageUrl: ''
  });

  updateRequest = signal<UpdateWeaponRequest>({
    name: '',
    description: '',
    weight: 0,
    properties: '',
    type: null as any,
    displayImageUrl: '',
    characterId: null
  });

  private readonly route = inject(ActivatedRoute);

  readonly weaponId: number | null = this.route.snapshot.paramMap.get('id') as number | null;

  ngOnInit() {
    if (this.weaponId !== null) {
      this.loadWeapon(this.weaponId);
    }
  }

  loadWeapon(id: number) {
    if (this.isLoading()) {
      return;
    }

    this.isLoading.set(true);

    this.weaponsService.getWeaponById({ id })
      .subscribe({
        next: (response) => {
          this.weapon.set(response);
          this.isLoading.set(false);
        },
        error: () => {
          this.error.set('Failed to load weapon details.');
          this.isLoading.set(false);
        }
      });
  }

  createWeapon() {
    if (this.isLoading()) {
      return;
    }

    this.isLoading.set(true);

    this.weaponsService.createWeapon(this.createRequest())
      .subscribe({
        next: (response) => {
          this.weapon.set(response);
          this.isLoading.set(false);
        },
        error: () => {
          this.error.set('Failed to create weapon.');
          this.isLoading.set(false);
        }
      });
  }

  updateWeapon() {
    if (this.isLoading() || this.weaponId === null) {
      return;
    }

    this.isLoading.set(true);

    this.weaponsService.updateWeapon(this.weaponId, this.updateRequest())
      .subscribe({
        next: (response) => {
          this.weapon.set(response);

          this.updateRequest.set({
            name: response.name,
            description: response.description,
            weight: response.weight,
            properties: response.properties,
            type: response.type,
            displayImageUrl: response.displayImageUrl,
            characterId: response.characterId
          });

          this.isLoading.set(false);
        },
        error: () => {
          this.error.set('Failed to update weapon.');
          this.isLoading.set(false);
        }
      });
  }
}

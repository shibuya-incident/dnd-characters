import { Component, inject, signal } from '@angular/core';
import { Loader } from '../../../shared/components/loader/loader.component';
import { WeaponsService } from '../../weapons-service';
import { GetWeaponByIdResponse } from '../../models/get-weapon-by-id/get-weapon-by-id-response.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  imports: [Loader],
  selector: 'app-weapon-detail-page',
  styleUrl: './weapon-detail-page.css',
  templateUrl: './weapon-detail-page.html',
})
export class WeaponDetailPage {

  readonly weaponsService: WeaponsService = inject(WeaponsService);

  readonly isLoading = signal<boolean>(false);

  readonly error = signal<string | null>(null);

  readonly weapon = signal<GetWeaponByIdResponse | null>(null);

  private route = inject(ActivatedRoute);

  weaponId: number | null = this.route.snapshot.paramMap.get('id') as number | null;

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
}

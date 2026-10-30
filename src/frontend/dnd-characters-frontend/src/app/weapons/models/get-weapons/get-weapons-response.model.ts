import { WeaponType } from "../weapon-type.enum";

export interface GetWeaponsResponse {
  id: number;
  name: string;
  weaponType: WeaponType;
  obtainedAt: Date;
  createdAt: Date;
  displayImageUrl: string;
  characterId: number;
}



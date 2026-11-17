import { WeaponType } from "../weapon-type.enum";

export interface CreateWeaponResponse {
  id: number;
  name: string;
  description: string;
  weight: number;
  properties: string;
  weaponType: WeaponType;
  displayImageUrl?: string;
  characterId?: number;
}
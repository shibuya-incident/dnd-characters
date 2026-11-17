import { WeaponType } from "../weapon-type.enum";

export interface UpdateWeaponResponse {
  id: number;
  name: string;
  description: string;
  weight: number;
  properties: string;
  type: WeaponType;
  obtainedAt?: Date;
  displayImageUrl?: string;
  characterId?: number | null;
}
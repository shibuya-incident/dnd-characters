import { WeaponType } from "../weapon-type.enum";

export interface GetWeaponByIdResponse {
  id: number;
  name: string;
  description: string;
  weight: number;
  properties: string;
  weaponType: WeaponType;
  obtainedAt?: Date;
  createdAt: Date;
  displayImageUrl: string;
  characterId?: number;
}
import { WeaponType } from "../weapon-type.enum";

export interface CreateWeaponRequest {
  name: string;
  description: string;
  weight: number;
  properties: string;
  type: WeaponType;
  displayImageUrl?: string;
}
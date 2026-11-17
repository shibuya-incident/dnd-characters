import { WeaponType } from "../weapon-type.enum";

export interface UpdateWeaponRequest {
    name: string;
    description: string;
    weight: number;
    properties: string;
    type: WeaponType;
    displayImageUrl?: string;
    characterId?: number | null;
}
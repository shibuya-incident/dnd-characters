import { PageListRequest } from "../../../shared/models/pagination/page-list-request";
import { WeaponType } from "../weapon-type.enum";
import { GetWeaponsSortBy } from "./get-weapons-sort-by";

export interface GetWeaponsRequest extends PageListRequest<GetWeaponsSortBy> {
    name?: string;
    type?: WeaponType;
    characterId?: number;
}
import { PageListRequest } from "../../../shared/models/pagination/page-list-request";
import { GetWeaponsSortBy } from "./get-weapons-sort-by";

export interface GetWeaponsRequest extends PageListRequest<GetWeaponsSortBy> {
    name?: string;
    type?: string;
    characterId?: number;
}


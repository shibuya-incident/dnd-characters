import { PageListRequest } from "../../../shared/models/pagination/page-list-request";
import { CharacterClass } from "../character-class.enum";
import { CharacterRace } from "../character-race.enum";
import { GetCharactersSortBy } from "./get-characters-sort-by";

export interface GetCharactersRequest extends PageListRequest<GetCharactersSortBy> {
    name?: string;
    race?: CharacterRace;
    class?: CharacterClass;
}
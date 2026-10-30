import { CharacterClass } from "../character-class.enum";
import { CharacterRace } from "../character-race.enum";

export interface GetCharactersResponse {
    id: number;
    name: string;
    race: CharacterRace;
    class: CharacterClass;
    displayImageUrl: string;
    updatedAt: Date;
}
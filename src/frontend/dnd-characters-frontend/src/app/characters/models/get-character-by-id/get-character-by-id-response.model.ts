import { CharacterClass } from "../character-class.enum";
import { CharacterRace } from "../character-race.enum";

export interface GetCharacterByIdResponse {
  id: number;
  name: string;
  surname?: string;
  gender: string;
  height: number;
  age: number;
  birthday: Date;
  lore: string;
  displayImageUrl: string;
  race: CharacterRace;
  class: CharacterClass;
  pet: Pet;
}

export interface Pet {
    name: string;
    species: string;
    lore: string;
    displayImageUrl: string;
}
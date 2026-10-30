import { ItemType } from "../item-type.enum";

export interface GetItemsResponse {
    id: number;
    name: string;
    itemType: ItemType;
    displayImageUrl: string;
    createdAt: Date;
}
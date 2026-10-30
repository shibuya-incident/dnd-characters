import { ShopType } from "../shop-type.enum";

export interface GetShopsResponse {
    id: number;
    name: string;
    shopType: ShopType;
    displayImageUrl: string;
    itemsCount: number;
    updatedAt: Date;
}
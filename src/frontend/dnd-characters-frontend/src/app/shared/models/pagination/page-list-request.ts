import { SortDirection } from "./sort-direction.enum";

export interface PageListRequest<TEnum> {
    page: number; 
    pageSize: number;
    sortBy: TEnum;
    sortDirection: SortDirection;
}



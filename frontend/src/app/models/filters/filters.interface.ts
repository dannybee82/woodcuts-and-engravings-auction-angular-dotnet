import { ItemType } from "../enmus/item-type.enum";

export interface FiltersInterface {
    isPriceAscending: boolean,
    isPriceDescending: boolean,
    isAmountOfBidsAscending: boolean,
    isAmountOfBidsDescending: boolean,
    filterType: ItemType,
    closingSoon: boolean,
    textSearch: string
}
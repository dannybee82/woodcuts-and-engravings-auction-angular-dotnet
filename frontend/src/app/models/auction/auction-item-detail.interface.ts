import { ItemType } from "../enmus/item-type.enum";

export interface AuctionItemDetail {
	id?: number,
	title: string,
	description: string,
	itemType: ItemType
}

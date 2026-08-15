import { SafeUrl } from "@angular/platform-browser";
import { AuctionItemDetail } from "./auction-item-detail.interface";
import { AuctionExpiration } from "./auction-expiration.interface";

export interface AuctionItem {
	id: number,
	isOpen: boolean,
	details: AuctionItemDetail,
	imageId: number,
	currentPrice: number,
	bidCount: number,
	endsAt: Date,
	auctionLength: number,
	itemImage?: SafeUrl | null,
	expiration: AuctionExpiration
}

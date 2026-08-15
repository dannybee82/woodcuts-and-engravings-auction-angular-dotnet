import { AuctionItemDetail } from "./auction-item-detail.interface";

export interface CreateOrUpdateAuctionInterface {
    id?: number,
    isOpen: boolean,
    details: AuctionItemDetail,
    currentPrice: number,
    auctionLength: number
}
import { Component, computed, effect, inject, input, InputSignal, model, ModelSignal, OnDestroy, OnInit, Signal, signal, WritableSignal } from '@angular/core';
import { CurrencyPipe, DatePipe, I18nPluralPipe } from '@angular/common';
import { AuctionItem } from '../../models/auction/auction-item.interface';
import { BidPriceInterface } from '../../models/bids/bid-price.interface';
import { Toast } from '../../services/toast/toast';
import { Toastr } from '../../components/toast/toastr';
import { AuctionRealTime } from '../../services/auction/auction-real-time';
import { AuctionBidDetail } from '../../models/auction/auction-bid-detail.interface';
import { ShowItemType } from '../../components/show-item-type/show-item-type';
import { BidEndsInterface } from '../../models/bids/bid-ends.interface';
import { Countdown } from '../../components/countdown/countdown';
import { environment } from '../../../environments/environment';
import { RESOURCE_CONFIG } from '../../services/generics/tokens/resource.config';
import { GenericsByIdService } from '../../services/generics/by_id/generics-by-id';
import { GenericsAllByIdService } from '../../services/generics/all_by_id/generics-all-by-id';
import { PlaceBidForm } from './place-bid-form/place-bid-form';
import { LatestBids } from './latest-bids/latest-bids';
import { LoadingSpinner } from '../../components/loading-spinner/loading-spinner';

@Component({
  selector: 'app-auction-details-page',
  imports: [CurrencyPipe, DatePipe, I18nPluralPipe, Toastr, ShowItemType, Countdown, PlaceBidForm, LatestBids, LoadingSpinner],
  templateUrl: './auction-details-page.html',
  styleUrl: './auction-details-page.scss',
  providers: [  
    GenericsByIdService<AuctionItem>, GenericsAllByIdService<AuctionBidDetail[]>,
    {  
      provide: RESOURCE_CONFIG,  
      useValue: {  
        controller: 'auction-items',  
        methodGetAllById: 'GetLatestBids',
        methodById: 'GetById'
      }  
    },
  ]
})
export class AuctionDetailsPage implements OnInit, OnDestroy {

  readonly id: InputSignal<number> = input.required();
  private _apiurl: string = environment.endpoint;    

  private _auction: WritableSignal<AuctionItem | null> = signal<AuctionItem | null>(null); 
  protected readonly auction: Signal<AuctionItem | null> = computed<AuctionItem | null>(() => this._auction());
  private _currentPrice: WritableSignal<number> = signal<number>(0);
  protected readonly currentPrice: Signal<number> = computed<number>(() => {
    if(this._currentPrice()) {
      return this._currentPrice();
    }

    return 0;
  });  
  private _currentBids: WritableSignal<number> = signal<number>(0);
  protected readonly currentBids: Signal<number> = computed<number>(() => {
    if(this._currentBids() > 0) {
      return this._currentBids();
    }

    return 0;
  });
  protected minimumBid: WritableSignal<number> = signal<number>(0);
  protected updateBids: ModelSignal<boolean> = model<boolean>(false);

  protected disablePlacingBid: WritableSignal<boolean> = signal<boolean>(false);  
  protected auctionIsClosing: WritableSignal<boolean> = signal<boolean>(false);

  protected pluralMapping = {
    '=0': 'No bids placed',
    '=1': '1 bid placed',
    'other': '# bids placed'
  };
  
  protected readonly auctionItemsService = inject(GenericsByIdService<AuctionItem>);  
  private auctionRealTimeService = inject(AuctionRealTime);    
  private toastService = inject(Toast);
  private _auctionPriceListener: Signal<BidPriceInterface | null> = this.auctionRealTimeService.price;
  private _auctionEndListener: Signal<BidEndsInterface | null> = this.auctionRealTimeService.auctionEnd;

  constructor() {
    effect(() => {
      if(this.auctionItemsService.data()) {
        this._auction.set(this.auctionItemsService.data());

        if(this._auction()) {
          this._currentPrice.set(this.auction()!.currentPrice);
          this._currentBids.set(this.auction()!.bidCount); 

          if(this.auction()!.isOpen) {
            this.minimumBid.set(this.getFloat(this.currentPrice(), 0.01));            
          }

          if(this.auction()!.isOpen && !this.auctionRealTimeService.isConnectionOpen()) {
            this.auctionRealTimeService.start(parseInt(this.id().toString()));
          }
        }

        this.auctionItemsService.params.set(undefined);
      }

      if(this.auctionItemsService.error()) {
        this.toastService.error('Can\'t fetch auction by id.');
      }
    });

    effect(() => {
      const auctionPriceChanges: BidPriceInterface | null = this._auctionPriceListener();

      if(auctionPriceChanges) {
        if(this._currentPrice() !== auctionPriceChanges.currentPrice) {
          this._currentPrice.set(auctionPriceChanges.currentPrice);
          this._currentBids.set(auctionPriceChanges.bidCount);
          this.minimumBid.set(this.getFloat(this._currentPrice(), 0.01));
          this.updateBids.set(true);
        }        
      }
    });

    effect(() => {
      const auctionEnd: BidEndsInterface | null = this._auctionEndListener();

      if(auctionEnd && this._auction() && (this._auction()?.isOpen ?? false)) {
        if(parseInt(this.id().toString()) === parseInt(auctionEnd.itemId)) {          
          this.getAuctionById();
        }
      }
    });
  }

  ngOnInit(): void {
    this.getAuctionById();
  }

  ngOnDestroy(): void {
    this.auctionRealTimeService.disconnect();
    this.auctionItemsService.destroy();    
    this.toastService.reset();
  }

  getImageUrl(id: number): string {
    return `${this._apiurl}auction-items/${id}`
  }

  refreshBids($event: boolean): void {
    if($event) {
      this.updateBids.set(true);
    }
  }

  stopAuction($event: boolean): void {
    if($event) {      
        this.auctionIsClosing.set(true);
        this.disablePlacingBid.set(true)
    }
  }

  private getFloat(f1: number, f2: number): number {
    const float01: number = parseFloat(f1.toFixed(2));
    const float02: number = parseFloat(f2.toFixed(2));
    const total: number = float01 + float02;
    return parseFloat(total.toFixed(2));
  }

  private getAuctionById(): void {    
    const params: Record<string, any> = {
      "id": this.id()
    };
    this.auctionItemsService.params.set(params);
  }

}
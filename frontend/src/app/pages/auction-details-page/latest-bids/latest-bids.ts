import { Component, computed, effect, inject, input, InputSignal, model, ModelSignal, OnDestroy, OnInit, signal, Signal, WritableSignal } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import { AuctionBidDetail } from '../../../models/auction/auction-bid-detail.interface';
import { GenericsAllByIdService } from '../../../services/generics/all_by_id/generics-all-by-id';
import { RESOURCE_CONFIG } from '../../../services/generics/tokens/resource.config';
import { Toast } from '../../../services/toast/toast';

@Component({
  selector: 'app-latest-bids',
  imports: [CurrencyPipe],
  templateUrl: './latest-bids.html',
  styleUrl: './latest-bids.scss',
    providers: [  
    GenericsAllByIdService<AuctionBidDetail[]>,
    {  
      provide: RESOURCE_CONFIG,  
      useValue: {  
        controller: 'bids',  
        methodGetAllById: 'GetLatestBids'
      }  
    },
  ]
})
export class LatestBids implements OnInit, OnDestroy {

  readonly id: InputSignal<number> = input.required<number>();
  readonly updateBids: ModelSignal<boolean> = model.required<boolean>();
  
  private _latestBids: WritableSignal<AuctionBidDetail[]> = signal<AuctionBidDetail[]>([]);
  protected readonly latestBids: Signal<AuctionBidDetail[]> = computed<AuctionBidDetail[]>(() => {
    if(this._latestBids().length > 0) {
      return this._latestBids();
    }

    return [];
  });

  private auctionLatestBidsService = inject(GenericsAllByIdService<AuctionBidDetail[]>);
  private toastService = inject(Toast);

  constructor() {    
    effect(() => {
      if(this.auctionLatestBidsService.data()) {
        this._latestBids.set(this.auctionLatestBidsService.data()!);
      }  

      if(this.auctionLatestBidsService.error()) {
        this.toastService.error('Can\'t update Latest Bids.');
      }   

      if(this.updateBids()) {
        this.auctionLatestBidsService.reload();
        this.updateBids.set(false);
      }
    });    
  }

  ngOnInit(): void {
    this.getLatestBids();
  }

  ngOnDestroy(): void {
    this.auctionLatestBidsService.destroy();
  }
  
  private getLatestBids(): void {
    const params: Record<string, any> = {
      "id": this.id()
    };
    this.auctionLatestBidsService.params.set(params);
    if(this.auctionLatestBidsService.data()) {
      this.auctionLatestBidsService.reload();
    }
  }

}

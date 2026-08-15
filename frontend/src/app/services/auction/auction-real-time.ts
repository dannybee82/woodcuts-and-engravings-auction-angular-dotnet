import { Service, signal, WritableSignal } from '@angular/core';
import { HubConnectionBuilder } from '@microsoft/signalr/dist/esm/HubConnectionBuilder';
import { environment } from '../../../environments/environment';
import { BidPriceInterface } from '../../models/bids/bid-price.interface';
import { BidEndsInterface } from '../../models/bids/bid-ends.interface';

@Service()
export class AuctionRealTime {
  private readonly api: string = environment.endpoint;
  private connection = new HubConnectionBuilder()  
    .withUrl(`${this.api}hubs/auction`)  
    .withAutomaticReconnect() // survives brief network drops  
    .build();  
    
  private _price: WritableSignal<BidPriceInterface | null> = signal<BidPriceInterface | null>(null);  
  readonly price = this._price.asReadonly();
  private _auctionEnd: WritableSignal<BidEndsInterface | null> = signal<BidEndsInterface | null>(null);
  readonly auctionEnd = this._auctionEnd.asReadonly();
  private _connectionOpen: WritableSignal<boolean> = signal<boolean>(false);
  
  async start(itemId: number) {  
    this._connectionOpen.set(true);
    this.connection.on('BidPlaced', (d: BidPriceInterface) => this._price.set(d));    
    this.connection.on('AuctionEnded', (d: BidEndsInterface) => this._auctionEnd.set(d));
    await this.connection.start();  
    await this.connection.invoke('JoinItem', itemId);  
    this._connectionOpen();    
  }

  isConnectionOpen(): boolean {    
    return this._connectionOpen();
  }

  disconnect() {
    this._price.set(null);
    this._auctionEnd.set(null);
    if(this.isConnectionOpen()) {
      this.connection.stop();
    }
    this._connectionOpen.set(false);
  }
}
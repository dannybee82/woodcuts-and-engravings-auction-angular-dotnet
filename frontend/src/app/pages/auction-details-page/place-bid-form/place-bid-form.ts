import { Component, effect, inject, input, InputSignal, model, ModelSignal, OnDestroy, output, OutputEmitterRef, ResourceStatus, signal, WritableSignal } from '@angular/core';
import { disabled, form, FormField, min, required } from '@angular/forms/signals';
import { ConfirmModal } from '../../../components/confirm-modal/confirm-modal';
import { CurrencyPipe } from '@angular/common';
import { PlaceBidInterface } from '../../../models/bids/place-bid.interface';
import { Toast } from '../../../services/toast/toast';
import { ConfirmModalInterface } from '../../../models/confirm-modal/confirm-modal.interface';
import { DummyUser } from '../../../services/dummy-user/dummy-user';
import { RESOURCE_CONFIG } from '../../../services/generics/tokens/resource.config';
import { GenericsCreateService } from '../../../services/generics/create/generics-create';

@Component({
  selector: 'app-place-bid-form',
  imports: [FormField, ConfirmModal, CurrencyPipe],
  templateUrl: './place-bid-form.html',
  styleUrl: './place-bid-form.scss',
  providers: [
    GenericsCreateService<PlaceBidInterface>,
    {  
      provide: RESOURCE_CONFIG,  
      useValue: {  
         controller: 'bids',  
          methodCreate: 'PlaceBid'
        }  
    }
  ]
})
export class PlaceBidForm implements OnDestroy {

  id: InputSignal<number> = input.required<number>();
  currentPrice: ModelSignal<number> = model<number>(0);
  minimumBid: ModelSignal<number> = model<number>(0);
  auctionIsClosing: ModelSignal<boolean> = model<boolean>(false);
  disablePlacingBid: ModelSignal<boolean> = model<boolean>(false);

  refresh: OutputEmitterRef<boolean> = output<boolean>();

  protected confirmBid: WritableSignal<ConfirmModalInterface | undefined> = signal<ConfirmModalInterface | undefined>(undefined);

  private _defaultBid: Omit<PlaceBidInterface, 'auctionItemId' | 'userId'> = {
    amount: 0
  };
  private _formModel: WritableSignal<Omit<PlaceBidInterface, 'auctionItemId' | 'userId'>> = signal<Omit<PlaceBidInterface, 'auctionItemId' | 'userId'>>(structuredClone(this._defaultBid));
  protected bidForm = form(this._formModel, (form) => {
    required(form.amount, { message: 'Bid amount is required' });
    min(form.amount, 0.01, { message: 'Bid amount must be greater than 0.01' });
    disabled(form, {
      when: () => this.auctionIsClosing()
    })
  });

  private toastService = inject(Toast);
  private dummyUserService = inject(DummyUser);
  private placeBidService = inject(GenericsCreateService<PlaceBidInterface>);

  constructor() {
    effect(() => {
      if(!this.placeBidService.data() && !this.placeBidService.error() && this.placeBidService.entity()) {
        this.toastService.success('Bid has been placed successfully');
        this.placeBidService.entity.set(undefined);          
        this.disablePlacingBid.set(false);
        this.refresh.emit(true);
      }

      if(this.placeBidService.error() && this.placeBidService.entity()) {                
        this.toastService.error('Can\'t place Bid'); 
        this.disablePlacingBid.set(false);
      }
    });
  }

  ngOnDestroy(): void {
    this.toastService.reset();
  }

  onPlaceBid($event: SubmitEvent): void {
    $event.preventDefault();
    if (!this.bidForm().valid()) {
      this.bidForm().markAsTouched();
      this.toastService.error('The bid is invalid');
      return;
    }

    const amount: number = this.bidForm.amount().value();

    if(amount <= this.currentPrice()) {
      // Handle the case where the bid amount is not higher than the current price
      this.toastService.error('The amount is lower than the current price');
      return;
    }

    // Call the service to place a bid
    this.disablePlacingBid.set(true);

    const confirmDialog: ConfirmModalInterface = {
      title: 'Confirm Bid',
      message: '',
      messageHtml: `Do you want to place a bid for <strong>€ ${amount}</strong>?`,
      additionalText: '',
      cancelText: 'Cancel',
      confirmText: 'Place Bid',
    };
    this.confirmBid.set(confirmDialog);
  }

  execConfirmBid($event: boolean): void {
    this.confirmBid.set(undefined);

    if($event) {      
      const amount: number = this.bidForm.amount().value();
      const data: PlaceBidInterface = {
        auctionItemId: this.id(),
        userId: this.dummyUserService.user().userId,
        amount: amount
      };

      this.placeBidService.entity.set(data);
    } else {
      this.disablePlacingBid.set(false);
    }
  }

}

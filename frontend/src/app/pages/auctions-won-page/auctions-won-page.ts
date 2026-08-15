import { Component, OnInit, signal, computed, inject, OnDestroy, WritableSignal, Signal, effect } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuctionItem } from '../../models/auction/auction-item.interface';
import { Toastr } from '../../components/toast/toastr';
import { Toast } from '../../services/toast/toast';
import { ShowItemType } from '../../components/show-item-type/show-item-type';
import { environment } from '../../../environments/environment';
import { RESOURCE_CONFIG } from '../../services/generics/tokens/resource.config';
import { GenericsAllByIdService } from '../../services/generics/all_by_id/generics-all-by-id';
import { DummyUser } from '../../services/dummy-user/dummy-user';
import { LoadingSpinner } from '../../components/loading-spinner/loading-spinner';

@Component({
  selector: 'app-auctions-won-page',
  imports: [CommonModule, Toastr, ShowItemType, LoadingSpinner],
  templateUrl: './auctions-won-page.html',
  styleUrl: './auctions-won-page.scss',
  providers: [
    GenericsAllByIdService<AuctionItem[]>, 
    {
      provide: RESOURCE_CONFIG,
      useValue: {
        controller: 'auction-won',
        methodGetAllById: 'GetAllWonItems'
      }
    }
  ]
})
export class AuctionsWonPage implements OnInit, OnDestroy {
  protected readonly wonAuctions: WritableSignal<AuctionItem[]> = signal<AuctionItem[]>([]);
  private _apiurl: string = environment.endpoint;    

  readonly pageSizeOptions: number[] = [5, 10, 25, 50, 100];
  pageSize: WritableSignal<number> = signal<number>(5);
  currentPage: WritableSignal<number> = signal<number>(1);

  readonly totalItems: Signal<number> = computed(() => this.wonAuctions().length);

  readonly totalPages: Signal<number> = computed(() =>
    Math.max(1, Math.ceil(this.totalItems() / this.pageSize()))
  );

  readonly pagedItems: Signal<AuctionItem[]> = computed<AuctionItem[]>(() => {
    const start = (this.currentPage() - 1) * this.pageSize();
    return this.wonAuctions().slice(start, start + this.pageSize());
  });

  readonly pageNumbers: Signal<number[]> = computed<number[]>(() =>
    Array.from({ length: this.totalPages() }, (_, i) => i + 1)
  ); 

  // Range string shown in the pager footer, e.g. "1 – 10 of 42".
  readonly rangeLabel: Signal<string> = computed<string>(() => {
    const total = this.totalItems();
    if (total === 0) return '0 items';
    const start = (this.currentPage() - 1) * this.pageSize() + 1;
    const end   = Math.min(this.currentPage() * this.pageSize(), total);
    return `${start} – ${end} of ${total}`;
  });

  protected readonly auctionWonService = inject(GenericsAllByIdService<AuctionItem[]>);
  private toastr = inject(Toast);
  private dummyUserservice = inject(DummyUser);

  constructor() {
    effect(() => {
      if(this.auctionWonService.data()) {
        this.wonAuctions.set(this.auctionWonService.data()!);
      }

      if(this.auctionWonService.error()) {
        this.toastr.error('Can\'t fetch won auctions.');
      }
    });
  }

  ngOnInit(): void {
    this.getAuctionsWon();
  }

  ngOnDestroy(): void {
    this.toastr.reset();
  }

  getImageUrl(id: number): string {
    return `${this._apiurl}auction-items/${id}`
  }

  onPageSizeChange($event: Event): void {
    const size: number = parseInt(($event.target as HTMLSelectElement).value);

    this.pageSize.set(size);
    this.currentPage.set(1);
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages()) {
      this.currentPage.set(page);
    }
  }

  prevPage(): void { 
    this.goToPage(this.currentPage() - 1); 
  }

  nextPage(): void { 
    this.goToPage(this.currentPage() + 1); 
  }

  private getAuctionsWon(): void {
    const params: Record<string, any> = {
      "id": this.dummyUserservice.user().userId
    }
    this.auctionWonService.params.set(params);
  }

}
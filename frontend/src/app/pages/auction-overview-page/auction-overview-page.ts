import { Component, computed, effect, inject, OnInit, ResourceStatus, Signal, signal, WritableSignal } from '@angular/core';
import { AuctionItem } from '../../models/auction/auction-item.interface';
import { ItemType } from '../../models/enmus/item-type.enum';
import { MinPipe } from '../../pipes/min-pipe';
import { Toast } from '../../services/toast/toast';
import { Toastr } from '../../components/toast/toastr';
import { CurrencyPipe } from '@angular/common';
import { ShowItemType } from '../../components/show-item-type/show-item-type';
import { Router } from '@angular/router';
import { DeleteModalInterface } from '../../models/delete-modal/delete-modal.interface';
import { DeleteModal } from '../../components/delete-modal/delete-modal';
import { RESOURCE_CONFIG } from '../../services/generics/tokens/resource.config';
import { GenericsAllService } from '../../services/generics/all/generics-all';
import { GenericsDeleteService } from '../../services/generics/delete/generics-delete';
import { LoadingSpinner } from '../../components/loading-spinner/loading-spinner';

@Component({
  selector: 'app-auction-overview-page',
  imports: [MinPipe, Toastr, CurrencyPipe, ShowItemType, DeleteModal, LoadingSpinner],
  templateUrl: './auction-overview-page.html',
  styleUrl: './auction-overview-page.scss',
  providers: [
    GenericsAllService<AuctionItem[]>, GenericsDeleteService,
    {
      provide: RESOURCE_CONFIG,
      useValue: {
        controller: 'auction-items',
        methodGetAll: 'GetAll',
        methodDelete: 'Delete'
      }
    }
  ]
})
export class AuctionOverviewPage implements OnInit {

  protected allItems: WritableSignal<AuctionItem[]> = signal<AuctionItem[]>([]);
  protected deleteModal: WritableSignal<DeleteModalInterface | undefined> = signal(undefined);
  private _deleteId: WritableSignal<number> = signal<number>(0);

  readonly pageSize: WritableSignal<number> = signal<number>(5);
  readonly currentPage: WritableSignal<number> = signal<number>(1);

  readonly totalItems: Signal<number> = computed(() => this.allItems().length);
  readonly totalPages: Signal<number> = computed(() =>
    Math.max(1, Math.ceil(this.totalItems() / this.pageSize()))
  );

  readonly pagedItems: Signal<AuctionItem[]> = computed<AuctionItem[]>(() => {
    const start = (this.currentPage() - 1) * this.pageSize();
    return this.allItems().slice(start, start + this.pageSize());
  });

  readonly visiblePages: Signal<number[]> = computed<number[]>(() => {
    const total   = this.totalPages();
    const current = this.currentPage();
    const delta   = 2;
    const range: number[] = [];

    const from = Math.max(1, current - delta);
    const to   = Math.min(total, current + delta);

    for (let i = from; i <= to; i++) {
      range.push(i);
    }
    return range;
  });

  readonly ItemType = ItemType;

  protected readonly auctionItemsService = inject(GenericsAllService<AuctionItem[]>);
  private auctionDeleteService = inject(GenericsDeleteService);
  private toastrService = inject(Toast);
  private router = inject(Router);

  constructor() {
    effect(() => {
      if(this.auctionItemsService.data()) {
        this.allItems.set(this.auctionItemsService.data()!);
      }

      if(this.auctionItemsService.error()) {
        this.toastrService.error('Can\'t fetch all Auctions.');
      }
    });

    effect(() => {
      const resolved: ResourceStatus = 'resolved';

      if(this.auctionDeleteService.status() === resolved && this.auctionDeleteService.statusCode() === 200) {
        this.toastrService.success('The Auction has been deleted successfully');
        this._deleteId.set(0);
        this.deleteModal.set(undefined);
        this.getAll();
        this.auctionDeleteService.id.set(undefined);
      }

      if(this.auctionDeleteService.error()) {
        this.toastrService.error('Can\'t delete the Auction');
        this._deleteId.set(0);
        this.deleteModal.set(undefined);
        this.auctionDeleteService.id.set(undefined);
      }
    });
  }

  ngOnInit(): void {
    this.getAll();
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

  changePageSize(event: Event): void {
    const size = Number((event.target as HTMLSelectElement).value);
    this.pageSize.set(size);
    this.currentPage.set(1);
  }

  onUpdate(item: AuctionItem): void {
    this.router.navigate(['create-or-update-auction', item.id]); 
  }

  onDelete(item: AuctionItem): void {
    this._deleteId.set(item.id);
    const data: DeleteModalInterface = {
      title: 'Delete Auction',
      message: 'Do you really want to delete the item here below?',
      additionalText: `${item.details.title} - ${item.details.itemType.toString()}`,
      cancelText: 'Cancel',
      deleteText: 'Delete',
    };
    this.deleteModal.set(data);
  }

  execDelete($event: boolean): void {
    if($event && this._deleteId() > 0) {
      this.auctionDeleteService.id.set(this._deleteId());      
    } else {
      this._deleteId.set(0);
      this.deleteModal.set(undefined);
    }
  }

  private getAll(): void {
    this.resetPagination();
    this.allItems.set([]);

    if(this.auctionItemsService.data()) {
      this.auctionItemsService.reload();
    }
  }

  private resetPagination(): void {
    this.pageSize.set(5);
    this.currentPage.set(1);
  }

}

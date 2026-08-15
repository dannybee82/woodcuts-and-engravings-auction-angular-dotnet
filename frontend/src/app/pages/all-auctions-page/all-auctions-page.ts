import { Component, effect, inject, OnInit, signal, WritableSignal } from '@angular/core';
import { AuctionItem } from '../../models/auction/auction-item.interface';
import { DecimalPipe } from '@angular/common';
import { PaginatedList } from '../../models/paginated-list/paginated-list.interface';
import { RouterModule } from '@angular/router';
import { Filters } from '../../components/filters/filters';
import { ActiveFilters } from '../../services/filters/active-filters';
import { FiltersInterface } from '../../models/filters/filters.interface';
import { PaginationAndPager } from '../../services/pagination-and-pager/pagination-and-pager';
import { PaginationAndPagerInterface } from '../../models/pagination-and-pager/pagination-and-pager.interface';
import { Pager } from '../../components/pager/pager';
import { Pagination } from '../../components/pagination/pagination';
import { ScrollToTop } from '../../components/scroll-to-top/scroll-to-top';
import { ShowItemType } from '../../components/show-item-type/show-item-type';
import { LoadingSpinner } from '../../components/loading-spinner/loading-spinner';
import { RESOURCE_CONFIG } from '../../services/generics/tokens/resource.config';
import { GenericsByParamsService } from '../../services/generics/by_params/generics-by-params';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-all-auctions-page',
  imports: [DecimalPipe, RouterModule, Filters, Pager, Pagination, ScrollToTop, ShowItemType, LoadingSpinner],
  templateUrl: './all-auctions-page.html',
  styleUrl: './all-auctions-page.scss',
  providers: [
    GenericsByParamsService<PaginatedList<AuctionItem>>, {
      provide: RESOURCE_CONFIG,
      useValue: {
        controller: 'auction-items',
        methodByParams: 'GetPagedList'
      }
    }
  ]
})
export class AllAuctionsPage implements OnInit {

  private _currentPageSize: WritableSignal<number> = signal<number>(10);
  private _currentPageNumber: WritableSignal<number> = signal<number>(1);
  private _currentFilters: WritableSignal<FiltersInterface | undefined> = signal<FiltersInterface | undefined>(undefined);
  private _apiurl: string = environment.endpoint;

  protected readonly auctionItemsService = inject(GenericsByParamsService<PaginatedList<AuctionItem>>);
  private activeFiltersService = inject(ActiveFilters);
  private paginationAndPagerService = inject(PaginationAndPager);
  
  constructor() {
    effect(() => {
      const paginationAndPager: PaginationAndPagerInterface = this.paginationAndPagerService.paginationAndPager();

      if(paginationAndPager) {
        let changesMade: boolean = false;

        if(this._currentPageSize() !== paginationAndPager.pageSize) {
          this._currentPageSize.set(paginationAndPager.pageSize);
          changesMade = true;
        }

        if(this._currentPageNumber() !== paginationAndPager.pageNumber) {
          this._currentPageNumber.set(paginationAndPager.pageNumber);
          changesMade = true;
        }

        if(changesMade) {
          this.getAll();
        }
      }
    });

    effect(() => {
      const filters: FiltersInterface | undefined = this.activeFiltersService.activeFilters();

      if(!this.checkEqualityObject(this._currentFilters(), filters)) {
        this._currentFilters.set(filters);
        this.getAll();
      } 
    });

    effect(() => {
      if(this.auctionItemsService.data()) {
        this.paginationAndPagerService.updateTotalPages(this.auctionItemsService.data().totalPages);
      }
    });
  }

  ngOnInit(): void {
    this.getAll(); 
  }

  getImageUrl(id: number): string {
    return `${this._apiurl}auction-items/${id}`
  }

  private getAll(): void {
    const additionalParams: Record<string, any> = {
      "pageNumber": this._currentPageNumber(),
      "pageSize": this._currentPageSize()
    };
    this.auctionItemsService.additionalParams.set(additionalParams);
    this.auctionItemsService.params.set(this._currentFilters());
  }

  private checkEqualityObject(obj1: FiltersInterface | undefined, obj2: FiltersInterface | undefined): boolean {
    if(!obj1 && !obj2) {
      return true;
    }

    if(obj1 && obj2) {
      return Object.entries(obj1).sort().toString() === Object.entries(obj2).sort().toString();
    }

    return false;
  }

}
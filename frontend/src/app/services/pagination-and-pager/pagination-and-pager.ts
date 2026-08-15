import { computed, Service, Signal, signal, WritableSignal } from '@angular/core';
import { PaginationAndPagerInterface } from '../../models/pagination-and-pager/pagination-and-pager.interface';

@Service()
export class PaginationAndPager {
  
  private _pageNumber: WritableSignal<number> = signal<number>(1);
  private _pageSize: WritableSignal<number> = signal<number>(10);
  private _totalPages: WritableSignal<number> = signal<number>(0);

  readonly paginationAndPager: Signal<PaginationAndPagerInterface> = computed<PaginationAndPagerInterface>(() => {
    const data: PaginationAndPagerInterface = {
      pageNumber: this._pageNumber(),
      pageSize: this._pageSize(),
      totalPages: this._totalPages()
    };
    return data;
  });

  updatePagination(pageNumber: number): void {
    this._pageNumber.set(pageNumber);
  }

  updatePager(pageSize: number): void {
    this._pageSize.set(pageSize);
  }

  updateTotalPages(totalPages: number): void {
    this._totalPages.set(totalPages);
  }

  reset(): void {
    this._pageNumber.set(1);
    this._pageSize.set(10);
    this._totalPages.set(0);
  }

}
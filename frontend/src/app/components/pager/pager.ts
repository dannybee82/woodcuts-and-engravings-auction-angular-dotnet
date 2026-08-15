import { AfterViewInit, Component, ElementRef, OnInit, Signal, inject, viewChild, WritableSignal, signal, OutputEmitterRef } from '@angular/core';
import { PaginationAndPager } from '../../services/pagination-and-pager/pagination-and-pager';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.html',
  styleUrls: ['./pager.scss']
})

export class Pager implements OnInit, AfterViewInit {

  public itemsPerPage?: number;
  public selectElement: Signal<ElementRef | undefined> = viewChild('selectEl');

  protected options: WritableSignal<number[]> = signal([5, 10, 25, 50, 100]);

	private paginationAndPagerService = inject(PaginationAndPager);

  ngOnInit() : void {}

  ngAfterViewInit(): void {
    if(this.selectElement()) {
      let selectEl: HTMLSelectElement | undefined = this.selectElement()?.nativeElement;

      if(selectEl) {
        const defaultPageSize: number = this.paginationAndPagerService.paginationAndPager().pageSize;
        selectEl.value = this.itemsPerPage != undefined ? this.itemsPerPage.toString() !== '' ? this.itemsPerPage?.toString()  : defaultPageSize.toString() : defaultPageSize.toString();
      }
    }
  }

  changePageSize($event: Event) {
    if($event.target instanceof HTMLSelectElement) {
      const el: HTMLSelectElement = $event.target as HTMLSelectElement;

      let parsed = parseInt(el.value);

      if(this.itemsPerPage != parsed) {
        this.itemsPerPage = parsed;
        this.paginationAndPagerService.updatePager(this.itemsPerPage);
      }  
    }  
  }

}
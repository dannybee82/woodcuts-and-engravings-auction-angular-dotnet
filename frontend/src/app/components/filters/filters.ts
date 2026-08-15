import { Component, computed, inject, OnInit, Signal, signal, WritableSignal } from '@angular/core';
import { ItemType, itemTypeArr } from '../../models/enmus/item-type.enum';
import { FiltersInterface } from '../../models/filters/filters.interface';
import { ActiveFilters } from '../../services/filters/active-filters';
import { SortOption, SortOptionConfig } from '../../models/filters/sort-options-config.interface';
import { ActiveFilter } from '../../models/filters/active-filter.interface';

@Component({
  selector: 'app-filters',
  imports: [],
  templateUrl: './filters.html',
  styleUrl: './filters.scss',
})
export class Filters implements OnInit {
  readonly ItemType: typeof ItemType = ItemType;
  readonly itemTypeArr: ItemType[] = itemTypeArr;

  readonly sortOptions: SortOptionConfig[] = [
    { value: 'PRICE_ASC',  label: 'Price Ascending'  },
    { value: 'PRICE_DESC', label: 'Price Descending' },
    { value: 'BIDS_ASC',  label: 'Bids Ascending'   },
    { value: 'BIDS_DESC', label: 'Bids Descending'  },
    { value: 'CLOSING_SOON', label: 'Closing Soon' }
  ];

  readonly sortOption: WritableSignal<SortOption> = signal<SortOption>(null);
  readonly filterType: WritableSignal<ItemType> = signal<ItemType>(ItemType.ALL);
  readonly textSearch: WritableSignal<string>  = signal<string>('');
  protected searchForText: WritableSignal<boolean> = signal<boolean>(false);

  readonly filters: Signal<FiltersInterface | undefined> = computed<FiltersInterface | undefined>(() => {
    const filters: FiltersInterface = {
      isPriceAscending:        this.sortOption() === 'PRICE_ASC',
      isPriceDescending:       this.sortOption() === 'PRICE_DESC',
      isAmountOfBidsAscending: this.sortOption() === 'BIDS_ASC',
      isAmountOfBidsDescending:this.sortOption() === 'BIDS_DESC',
      closingSoon:             this.sortOption() === 'CLOSING_SOON',
      filterType:              this.filterType(),      
      textSearch:              this.textSearch()
    };

    if(!filters.isPriceAscending && 
      !filters.isPriceDescending && 
      !filters.isAmountOfBidsAscending && 
      !filters.isAmountOfBidsDescending &&
      !filters.closingSoon &&
      filters.filterType === ItemType.ALL &&
      filters.textSearch.trim() === '') {
        return undefined;
    }
    
    return filters;  
  });

  readonly activeFilters: Signal<ActiveFilter[]> = computed<ActiveFilter[]>(() => {
    const chips: ActiveFilter[] = [];

    const sort = this.sortOption();
    if (sort) {
      const label = this.sortOptions.find(o => o.value === sort)?.label ?? sort;
      chips.push({ 
        label: `Sort: ${label}`, 
        remove: () => {
          this.sortOption.set(null);
          this.activeFiltersService.activeFilters.set(this.filters());
        } 
      });
    }

    if (this.filterType() !== ItemType.ALL) {
      chips.push({
        label: `Type: ${this.itemTypeLabel(this.filterType())}`,
        remove: () => {
          this.filterType.set(ItemType.ALL);
          this.activeFiltersService.activeFilters.set(this.filters());
        }
      });
    }

    const search = this.textSearch().trim();
    if (search && this.searchForText()) {
      chips.push({
        label: `"${search}"`, 
        remove: () => {
          this.textSearch.set('')
          this.searchForText.set(false);
          this.activeFiltersService.activeFilters.set(this.filters());
        } 
      });
    }

    return chips;
  });

  readonly hasActiveFilters: Signal<boolean> = computed(() => this.activeFilters().length > 0);

  private activeFiltersService = inject(ActiveFilters);

  ngOnInit(): void {
    const filters: FiltersInterface | undefined = this.activeFiltersService.activeFilters();

    if(filters) {
      if(filters.isPriceAscending) {
        this.sortOption.set('PRICE_ASC');
      }

      if(filters.isPriceDescending) {
        this.sortOption.set('PRICE_DESC');
      }

      if(filters.isAmountOfBidsAscending) {
        this.sortOption.set('BIDS_ASC');
      }

      if(filters.isAmountOfBidsDescending) {
        this.sortOption.set('BIDS_DESC');
      }

      if(filters.closingSoon) {
        this.sortOption.set('CLOSING_SOON');
      }

      if(filters.filterType) {
        this.filterType.set(filters.filterType);
      }

      if(filters.textSearch) {
        this.textSearch.set(filters.textSearch);
        this.searchForText.set(true);
      }
    }
  }

  sortLabel(): string {
    const sort = this.sortOption();
    return sort
      ? (this.sortOptions.find(o => o.value === sort)?.label ?? 'Sort by')
      : 'Sort by';
  }

  typeLabel(): string {
    return this.itemTypeLabel(this.filterType());
  }

  itemTypeLabel(type: ItemType): string {
    const map: Record<ItemType, string> = {
      [ItemType.ALL]:       'All Types',
      [ItemType.WOODCUT]:   'Woodcuts',
      [ItemType.ENGRAVING]: 'Engravings',
      [ItemType.UNKNOWN]:   'Unknown',
    };
    return map[type] ?? type;
  }

  selectSort(value: SortOption, detailsEl: HTMLDetailsElement): void {
    this.sortOption.set(value);
    detailsEl.removeAttribute('open');
    this.activeFiltersService.activeFilters.set(this.filters());
  }

  selectType(type: ItemType, detailsEl: HTMLDetailsElement): void {
    this.filterType.set(type);
    detailsEl.removeAttribute('open');
    this.activeFiltersService.activeFilters.set(this.filters());
  }

  onSearchInput(event: Event): void {
    this.textSearch.set((event.target as HTMLInputElement).value);
  }

  searchText(): void {
    if(this.textSearch().trim() !== '') {
      this.searchForText.set(true);
      this.activeFiltersService.activeFilters.set(this.filters());
    }
  }

  resetFilters(): void {
    this.sortOption.set(null);
    this.filterType.set(ItemType.ALL);
    this.textSearch.set('');
    this.searchForText.set(false);
    this.activeFiltersService.activeFilters.set(undefined);
  }
  
}
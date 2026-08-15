import { Service, signal, WritableSignal } from '@angular/core';
import { FiltersInterface } from '../../models/filters/filters.interface';

@Service()
export class ActiveFilters {

    readonly activeFilters: WritableSignal<FiltersInterface | undefined> = signal<FiltersInterface | undefined>(undefined);
    
}

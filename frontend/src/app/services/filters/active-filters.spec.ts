import { TestBed } from '@angular/core/testing';

import { ActiveFilters } from './active-filters';

describe('ActiveFilters', () => {
  let service: ActiveFilters;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ActiveFilters);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

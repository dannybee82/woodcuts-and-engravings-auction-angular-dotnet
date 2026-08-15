import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LatestBids } from './latest-bids';

describe('LatestBids', () => {
  let component: LatestBids;
  let fixture: ComponentFixture<LatestBids>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LatestBids],
    }).compileComponents();

    fixture = TestBed.createComponent(LatestBids);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

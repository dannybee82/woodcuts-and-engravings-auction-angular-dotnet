import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuctionOverviewPage } from './auction-overview-page';

describe('AuctionOverviewPage', () => {
  let component: AuctionOverviewPage;
  let fixture: ComponentFixture<AuctionOverviewPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuctionOverviewPage],
    }).compileComponents();

    fixture = TestBed.createComponent(AuctionOverviewPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

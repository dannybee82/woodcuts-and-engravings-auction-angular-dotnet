import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllAuctionsPage } from './all-auctions-page';

describe('AllAuctionsPage', () => {
  let component: AllAuctionsPage;
  let fixture: ComponentFixture<AllAuctionsPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AllAuctionsPage],
    }).compileComponents();

    fixture = TestBed.createComponent(AllAuctionsPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

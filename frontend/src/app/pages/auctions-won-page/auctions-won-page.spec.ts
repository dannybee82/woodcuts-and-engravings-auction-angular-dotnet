import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuctionsWonPage } from './auctions-won-page';

describe('AuctionsWonPage', () => {
  let component: AuctionsWonPage;
  let fixture: ComponentFixture<AuctionsWonPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuctionsWonPage],
    }).compileComponents();

    fixture = TestBed.createComponent(AuctionsWonPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

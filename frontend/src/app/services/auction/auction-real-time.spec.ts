import { TestBed } from '@angular/core/testing';
import { AuctionRealTime } from '../../services/auction/auction-real-time';

describe('AuctionRealTime', () => {
  let service: AuctionRealTime;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuctionRealTime);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

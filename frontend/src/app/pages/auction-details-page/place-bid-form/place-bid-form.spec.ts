import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaceBidForm } from './place-bid-form';

describe('PlaceBidForm', () => {
  let component: PlaceBidForm;
  let fixture: ComponentFixture<PlaceBidForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PlaceBidForm],
    }).compileComponents();

    fixture = TestBed.createComponent(PlaceBidForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

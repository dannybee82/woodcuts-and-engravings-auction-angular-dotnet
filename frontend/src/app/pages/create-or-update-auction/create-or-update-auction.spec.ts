import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrUpdateAuction } from './create-or-update-auction';

describe('CreateOrUpdateAuction', () => {
  let component: CreateOrUpdateAuction;
  let fixture: ComponentFixture<CreateOrUpdateAuction>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateOrUpdateAuction],
    }).compileComponents();

    fixture = TestBed.createComponent(CreateOrUpdateAuction);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

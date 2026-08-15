import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowItemType } from './show-item-type';

describe('ItemType', () => {
  let component: ShowItemType;
  let fixture: ComponentFixture<ShowItemType>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShowItemType],
    }).compileComponents();

    fixture = TestBed.createComponent(ShowItemType);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

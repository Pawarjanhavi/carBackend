import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PriceRangeSearchComponent } from './price-range-search.component';

describe('PriceRangeSearchComponent', () => {
  let component: PriceRangeSearchComponent;
  let fixture: ComponentFixture<PriceRangeSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PriceRangeSearchComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PriceRangeSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

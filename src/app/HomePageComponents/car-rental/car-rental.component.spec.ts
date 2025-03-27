import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarRentalComponent } from './car-rental.component';

describe('CarRentalComponent', () => {
  let component: CarRentalComponent;
  let fixture: ComponentFixture<CarRentalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CarRentalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CarRentalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

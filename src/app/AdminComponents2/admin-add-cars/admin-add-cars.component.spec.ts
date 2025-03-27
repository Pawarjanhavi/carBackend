import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminAddCarsComponent } from './admin-add-cars.component';

describe('AdminAddCarsComponent', () => {
  let component: AdminAddCarsComponent;
  let fixture: ComponentFixture<AdminAddCarsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminAddCarsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminAddCarsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

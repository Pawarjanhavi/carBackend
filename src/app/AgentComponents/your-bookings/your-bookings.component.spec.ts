import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YourBookingsComponent } from './your-bookings.component';

describe('YourBookingsComponent', () => {
  let component: YourBookingsComponent;
  let fixture: ComponentFixture<YourBookingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [YourBookingsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YourBookingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

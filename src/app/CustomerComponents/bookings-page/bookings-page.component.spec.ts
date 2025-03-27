import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingsPageComponent } from './bookings-page.component';

describe('BookingsPageComponent', () => {
  let component: BookingsPageComponent;
  let fixture: ComponentFixture<BookingsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookingsPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookingsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YourCarsComponent } from './your-cars.component';

describe('YourCarsComponent', () => {
  let component: YourCarsComponent;
  let fixture: ComponentFixture<YourCarsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [YourCarsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YourCarsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

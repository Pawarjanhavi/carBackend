import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TearmsandConditionsComponent } from './terms-and-conditions.component';

describe('TearmsandConditionsComponent', () => {
  let component: TearmsandConditionsComponent;
  let fixture: ComponentFixture<TearmsandConditionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TearmsandConditionsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TearmsandConditionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

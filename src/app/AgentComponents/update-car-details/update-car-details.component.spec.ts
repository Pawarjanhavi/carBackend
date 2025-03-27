import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateCarDetailsComponent } from './update-car-details.component';

describe('UpdateCarDetailsComponent', () => {
  let component: UpdateCarDetailsComponent;
  let fixture: ComponentFixture<UpdateCarDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateCarDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateCarDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

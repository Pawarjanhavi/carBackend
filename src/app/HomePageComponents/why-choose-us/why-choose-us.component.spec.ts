import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WhyChooseUSComponent } from './why-choose-us.component';

describe('WhyChooseUSComponent', () => {
  let component: WhyChooseUSComponent;
  let fixture: ComponentFixture<WhyChooseUSComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WhyChooseUSComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WhyChooseUSComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

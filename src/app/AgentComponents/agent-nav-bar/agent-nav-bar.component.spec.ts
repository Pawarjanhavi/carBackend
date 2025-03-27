import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentNavBarComponent } from './agent-nav-bar.component';

describe('AgentNavBarComponent', () => {
  let component: AgentNavBarComponent;
  let fixture: ComponentFixture<AgentNavBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AgentNavBarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AgentNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

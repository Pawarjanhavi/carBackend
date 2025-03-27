import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExploreBrandsComponent } from './explore-brands.component';

describe('ExploreBrandsComponent', () => {
  let component: ExploreBrandsComponent;
  let fixture: ComponentFixture<ExploreBrandsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExploreBrandsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExploreBrandsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarouselMaterialComponent } from './carousel-material.component';

describe('CarouselMaterialComponent', () => {
  let component: CarouselMaterialComponent;
  let fixture: ComponentFixture<CarouselMaterialComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CarouselMaterialComponent]
    });
    fixture = TestBed.createComponent(CarouselMaterialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

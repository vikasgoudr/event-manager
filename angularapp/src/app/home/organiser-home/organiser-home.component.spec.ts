import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganiserHomeComponent } from './organiser-home.component';

describe('OrganiserHomeComponent', () => {
  let component: OrganiserHomeComponent;
  let fixture: ComponentFixture<OrganiserHomeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OrganiserHomeComponent]
    });
    fixture = TestBed.createComponent(OrganiserHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

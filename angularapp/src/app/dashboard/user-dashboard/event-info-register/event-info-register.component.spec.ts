import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventInfoRegisterComponent } from './event-info-register.component';

describe('EventInfoRegisterComponent', () => {
  let component: EventInfoRegisterComponent;
  let fixture: ComponentFixture<EventInfoRegisterComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EventInfoRegisterComponent]
    });
    fixture = TestBed.createComponent(EventInfoRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InitialRegisterComponent } from './initial-register.component';

describe('InitialRegisterComponent', () => {
  let component: InitialRegisterComponent;
  let fixture: ComponentFixture<InitialRegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InitialRegisterComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InitialRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

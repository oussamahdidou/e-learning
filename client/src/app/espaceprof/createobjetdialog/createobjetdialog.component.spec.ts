import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateobjetdialogComponent } from './createobjetdialog.component';

describe('CreateobjetdialogComponent', () => {
  let component: CreateobjetdialogComponent;
  let fixture: ComponentFixture<CreateobjetdialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateobjetdialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateobjetdialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

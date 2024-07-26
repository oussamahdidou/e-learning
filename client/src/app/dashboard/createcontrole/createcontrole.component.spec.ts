import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatecontroleComponent } from './createcontrole.component';

describe('CreatecontroleComponent', () => {
  let component: CreatecontroleComponent;
  let fixture: ComponentFixture<CreatecontroleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreatecontroleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatecontroleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

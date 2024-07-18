import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatechapitreComponent } from './createchapitre.component';

describe('CreatechapitreComponent', () => {
  let component: CreatechapitreComponent;
  let fixture: ComponentFixture<CreatechapitreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreatechapitreComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatechapitreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ObjetsPedagogiquesComponent } from './objets-pedagogiques.component';

describe('ObjetsPedagogiquesComponent', () => {
  let component: ObjetsPedagogiquesComponent;
  let fixture: ComponentFixture<ObjetsPedagogiquesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ObjetsPedagogiquesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ObjetsPedagogiquesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

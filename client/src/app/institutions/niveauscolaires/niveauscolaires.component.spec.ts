import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NiveauScolairesComponent } from './niveauscolaires.component';

describe('NiveauscolairesComponent', () => {
  let component: NiveauScolairesComponent;
  let fixture: ComponentFixture<NiveauScolairesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NiveauScolairesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NiveauScolairesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

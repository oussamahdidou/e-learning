import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateNiveauScolaireModuleDialogComponent } from './create-niveau-scolaire-module-dialog.component';

describe('CreateNiveauScolaireModuleDialogComponent', () => {
  let component: CreateNiveauScolaireModuleDialogComponent;
  let fixture: ComponentFixture<CreateNiveauScolaireModuleDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateNiveauScolaireModuleDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateNiveauScolaireModuleDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

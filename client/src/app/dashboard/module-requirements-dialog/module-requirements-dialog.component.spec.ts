import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModuleRequirementsDialogComponent } from './module-requirements-dialog.component';

describe('ModuleRequirementsDialogComponent', () => {
  let component: ModuleRequirementsDialogComponent;
  let fixture: ComponentFixture<ModuleRequirementsDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ModuleRequirementsDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModuleRequirementsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

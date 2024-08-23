import { Component, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DashboardService } from '../../services/dashboard.service';
import { ModuleRequirementsDialogComponent } from '../module-requirements-dialog/module-requirements-dialog.component';

@Component({
  selector: 'app-create-niveau-scolaire-module-dialog',
  templateUrl: './create-niveau-scolaire-module-dialog.component.html',
  styleUrl: './create-niveau-scolaire-module-dialog.component.css',
})
export class CreateNiveauScolaireModuleDialogComponent {
  form: FormGroup;
  institutions: any[] = [];
  niveauscolaires: any[] = [];

  constructor(
    public dialogRef: MatDialogRef<CreateNiveauScolaireModuleDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private dashboardservice: DashboardService
  ) {
    this.form = this.fb.group({
      institution: ['', Validators.required],
      niveauscolaire: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.dashboardservice.getinstitutions().subscribe((data) => {
      this.institutions = data;
    });

    this.form.get('institution')?.valueChanges.subscribe((institutionId) => {
      if (institutionId) {
        this.dashboardservice
          .getniveauscolaires(institutionId)
          .subscribe((data) => {
            this.niveauscolaires = data.niveauScolaires;
            this.form.get('niveauscolaire')?.setValue(''); // Reset niveauscolaire
          });
      } else {
        this.niveauscolaires = [];
      }
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const result = {
        institutionId: this.form.value.institution,
        niveauscolaireId: this.form.value.niveauscolaire,
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}

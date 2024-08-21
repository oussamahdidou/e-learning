import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSelectChange } from '@angular/material/select';
import { DashboardService } from '../../services/dashboard.service';

@Component({
  selector: 'app-module-requirements-dialog',
  templateUrl: './module-requirements-dialog.component.html',
  styleUrl: './module-requirements-dialog.component.css',
})
export class ModuleRequirementsDialogComponent implements OnInit {
  form: FormGroup;
  institutions: any[] = [];
  niveauscolaires: any[] = [];
  modules: any[] = [];

  constructor(
    public dialogRef: MatDialogRef<ModuleRequirementsDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private dashboardservice: DashboardService
  ) {
    this.form = this.fb.group({
      institution: ['', Validators.required],
      niveauscolaire: ['', Validators.required],
      module: ['', Validators.required],
      seuil: [0, [Validators.required, Validators.min(0), Validators.max(20)]],
    });
  }

  ngOnInit(): void {
    this.dashboardservice.getinstitutions().subscribe((data) => {
      this.institutions = data;
      console.log('Institutions:', this.institutions);
    });

    this.form.get('institution')?.valueChanges.subscribe((institutionId) => {
      console.log('Selected institution:', institutionId);
      if (institutionId) {
        this.dashboardservice
          .getniveauscolaires(institutionId)
          .subscribe((data) => {
            console.log('Niveauscolaires fetched:', data);
            this.niveauscolaires = data.niveauScolaires;
            this.form.get('niveauscolaire')?.setValue(''); // Reset niveauscolaire
            this.modules = []; // Clear modules
            this.form.get('module')?.setValue(''); // Reset module
          });
      } else {
        this.niveauscolaires = [];
        this.modules = [];
      }
    });

    this.form
      .get('niveauscolaire')
      ?.valueChanges.subscribe((niveauscolaireId) => {
        console.log('Selected niveauscolaire:', niveauscolaireId);
        if (niveauscolaireId) {
          this.dashboardservice
            .getModules(niveauscolaireId)
            .subscribe((data) => {
              console.log('Modules fetched:', data);
              this.modules = data.niveauScolaireModules.map(
                (item: any) => item.module
              );
              this.form.get('module')?.setValue(''); // Reset module
            });
        } else {
          this.modules = [];
        }
      });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const result = {
        moduleId: this.form.value.module,
        seuil: this.form.value.seuil,
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}

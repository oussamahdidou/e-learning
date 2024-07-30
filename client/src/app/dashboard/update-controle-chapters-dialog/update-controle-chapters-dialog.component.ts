import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DashboardService } from '../../services/dashboard.service';

@Component({
  selector: 'app-update-controle-chapters-dialog',
  templateUrl: './update-controle-chapters-dialog.component.html',
  styleUrl: './update-controle-chapters-dialog.component.css',
})
export class UpdateControleChaptersDialogComponent implements OnInit {
  form: FormGroup;
  items: any[] = [];
  errorMessage: string | null = null;
  constructor(
    public dialogRef: MatDialogRef<UpdateControleChaptersDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private dashboardservice: DashboardService
  ) {
    this.form = this.fb.group({});
  }
  ngOnInit(): void {
    this.dashboardservice
      .getchapitrestoupdatecontroles(this.data.id)
      .subscribe((response) => {
        this.items = response;
        this.items.forEach((item) => {
          this.form.addControl(
            item.id.toString(),
            new FormControl(item.checked)
          );
        });
      });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    if (this.isAtLeastOneChecked()) {
      const updatedItems = this.items.map((item) => ({
        ...item,
        checked: this.form.get(item.id.toString())?.value ?? item.checked,
      }));
      this.dialogRef.close(updatedItems);
    } else {
      this.errorMessage = 'You must select at least one chapter.';
    }
  }
  private isAtLeastOneChecked(): boolean {
    return this.items.some((item) => this.form.get(item.id.toString())?.value);
  }
}

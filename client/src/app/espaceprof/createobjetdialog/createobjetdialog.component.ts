import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-createobjetdialog',
  templateUrl: './createobjetdialog.component.html',
  styleUrl: './createobjetdialog.component.css',
})
export class CreateobjetdialogComponent {
  textInput: string = '';
  selectedFile: File | null = null;

  constructor(
    public dialogRef: MatDialogRef<CreateobjetdialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { id: number }
  ) {}

  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    const result = {
      id: this.data.id,
      textInput: this.textInput,
      file: this.selectedFile,
    };
    this.dialogRef.close(result);
  }
}

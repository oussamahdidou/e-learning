import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-createchapitre',
  templateUrl: './createchapitre.component.html',
  styleUrl: './createchapitre.component.css',
})
export class CreatechapitreComponent {
  chapterForm: FormGroup;
  selectedFiles: { [key: string]: File } = {};

  constructor(private fb: FormBuilder) {
    this.chapterForm = this.fb.group({
      chapterName: ['', Validators.required],
      pdfCourse: ['', Validators.required],
      videoFile: ['', Validators.required],
      pdfSynthese: ['', Validators.required],
      schema: ['', Validators.required],
    });
  }

  onFileSelect(event: any, fieldName: string): void {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.selectedFiles[fieldName] = file;
    }
  }

  onSubmit(): void {
    if (this.chapterForm.valid) {
      const formData = new FormData();
      formData.append(
        'chapterName',
        this.chapterForm.get('chapterName')?.value
      );
      formData.append('pdfCourse', this.selectedFiles['pdfCourse']);
      formData.append('videoFile', this.selectedFiles['videoFile']);
      formData.append('pdfSynthese', this.selectedFiles['pdfSynthese']);
      formData.append('schema', this.selectedFiles['schema']);

      // Log the form data to the console
      console.log('Form Data:');
      console.log('Chapter Name:', this.chapterForm.get('chapterName')?.value);
      console.log('PDF Course:', this.selectedFiles['pdfCourse']);
      console.log('Video File:', this.selectedFiles['videoFile']);
      console.log('PDF Synthese:', this.selectedFiles['pdfSynthese']);
      console.log('Schema:', this.selectedFiles['schema']);

      // Submit the form data to your backend API
      // Example: this.http.post('/api/chapters', formData).subscribe(...)
    }
  }
}

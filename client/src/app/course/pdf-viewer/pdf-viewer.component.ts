import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseService } from '../../services/course.service';

@Component({
  selector: 'app-pdf-viewer',
  templateUrl: './pdf-viewer.component.html',
  styleUrls: ['./pdf-viewer.component.css'],
})
export class PdfViewerComponent {
  pdfUrl: string | undefined;
  isExam: boolean = false;
  exam : any;
  isFirstCour: number = 1;
  isLastControle: number = 1;
  selectedFile: File | null = null;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private courseService: CourseService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('pdfid');
      if (idParam) {
        const id = +idParam;
        const routePath = this.route.snapshot.routeConfig?.path;

        if (routePath?.includes('cour')) {
          this.courseService.getCourPdfUrlById(id).subscribe((url) => {
            this.pdfUrl = url;
          });
        } else if (routePath?.includes('synthese')) {
          this.courseService.getSyntheseById(id).subscribe((url) => {
            this.pdfUrl = url;
          });
        } else if (routePath?.includes('exam')) {
          this.courseService.getControleById(id).subscribe((url) => {
            this.pdfUrl = url?.ennonce;
            this.exam = url
          });
          this.isExam = true;

        } else if (routePath?.includes('schema')) {
          this.courseService.getSchemaById(id).subscribe((url) => {
            this.pdfUrl = url;
          });
        } else {
          console.error('Unknown route type');
        }
      } else {
        console.error('ID parameter is missing');
      }
    });
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    }
  }

  uploadDevoir(): void {
    if (!this.selectedFile || this.exam.id === null) {
      console.error('No file selected or no controle ID available');
      return;
    }

    const formData = new FormData();
    formData.append('file', this.selectedFile);
    formData.append('controleId', this.exam.id.toString());

    this.courseService.uploadSolution(formData).subscribe(
      (response) => {
        console.log('File uploaded successfully:', response);
      },
      (error) => {
        console.error('Error uploading file:', error);
      }
    );
  }
}

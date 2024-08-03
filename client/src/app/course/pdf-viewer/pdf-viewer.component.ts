import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseService } from '../../services/course.service';
import { environment } from '../../../environments/environment';
import Swal from 'sweetalert2';
import { ErrorHandlingService } from '../../services/error-handling.service';

@Component({
  selector: 'app-pdf-viewer',
  templateUrl: './pdf-viewer.component.html',
  styleUrls: ['./pdf-viewer.component.css'],
})
export class PdfViewerComponent {
  pdfUrl: string | undefined;
  isExam: boolean = false;
  exam: any;
  isFirstCour: number = 1;
  isLastControle: number = 1;
  selectedFile: File | null = null;
  host = environment.apiUrl;
  devoirePdfUrl: string = this.host;
  devoirExists: boolean = false;
  id: number = 0;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private courseService: CourseService,
    private errorHandlingService: ErrorHandlingService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('pdfid');
      if (idParam) {
        this.id = +idParam;
        const routePath = this.route.snapshot.routeConfig?.path;

        if (routePath?.includes('cour')) {
          this.courseService.getCourPdfUrlById(this.id).subscribe((url) => {
            this.pdfUrl = url;
          }
          , (error) => {
            this.errorHandlingService.handleError(error,'Failed to load Cour. Please try again later. ')
          }
        );
        } else if (routePath?.includes('synthese')) {
          this.courseService.getSyntheseById(this.id).subscribe((url) => {
            if (url) {
              this.pdfUrl = url;
            }
            }
            , (error) => {
              this.errorHandlingService.handleError(error,'Failed to load Synthese. Please try again later. ')
          });
        } else if (routePath?.includes('exam')) {
          this.courseService.getControleById(this.id).subscribe((url) => {
            this.pdfUrl = url?.ennonce;
            this.exam = url;
            this.isDevoirExists(this.id);}
            , (error) => {
              console.error("error fetching exam", error);
              this.errorHandlingService.handleError(error,'Failed to load Exam. Please try again later. ')
            });
          this.isExam = true;
        } else if (routePath?.includes('schema')) {
          this.courseService.getSchemaById(this.id).subscribe((url) => {
            this.pdfUrl = url;}
            , (error) => {
              this.errorHandlingService.handleError(error,'Failed to load Schema. Please try again later. ')
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
      this.errorHandlingService.handleError(null,'No file selected or no controle ID available')
      return;
    }

    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.courseService.uploadSolution(formData, this.exam.id).subscribe(
      (response) => {
        this.devoirExists = true;
      },
      (error) => {
        this.errorHandlingService.handleError(error,'Error uploading file')
      }
    );
  }
  isDevoirExists(id: number) {
    this.courseService.isDevoirUploaded(id).subscribe((res) => {
      this.devoirePdfUrl = this.devoirePdfUrl + res.reponse;
      this.devoirExists = true;
      console.log(this.devoirePdfUrl);
    });
  }
  deleteDevoir() {
    this.courseService.deleteDevoir(this.id).subscribe((res) => {
      this.devoirExists = false;
    });
  }
}

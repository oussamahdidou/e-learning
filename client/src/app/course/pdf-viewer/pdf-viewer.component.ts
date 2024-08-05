import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseService } from '../../services/course.service';
import { environment } from '../../../environments/environment';
import { ErrorHandlingService } from '../../services/error-handling.service';

@Component({
  selector: 'app-pdf-viewer',
  templateUrl: './pdf-viewer.component.html',
  styleUrls: ['./pdf-viewer.component.css'],
})
export class PdfViewerComponent {
  pdfUrl: string | undefined;
  isExam: boolean = false;
  isControleFinal: boolean = false;
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
          this.loadCourPdf();
        } else if (routePath?.includes('synthese')) {
          this.loadSynthesePdf();
        } else if (routePath?.includes('exam')) {
          this.loadExamPdf();
          this.isExam = true;
        } else if (routePath?.includes('schema')) {
          this.loadSchemaPdf();
        } else if (routePath?.includes('ControleFinal')) {
          this.loadControleFinalPdf();
          this.isControleFinal = true;
        } else {
          console.error('Unknown route type');
        }
      } else {
        console.error('ID parameter is missing');
      }
    });
  }

  // Load Methods
  private loadCourPdf() {
    this.courseService.getCourPdfUrlById(this.id).subscribe(
      (url) => {
        this.pdfUrl = url;
      },
      (error) => {
        this.errorHandlingService.handleError(error, 'Failed to load Cour. Please try again later.');
      }
    );
  }

  private loadSynthesePdf() {
    this.courseService.getSyntheseById(this.id).subscribe(
      (url) => {
        if (url) {
          this.pdfUrl = url;
        }
      },
      (error) => {
        this.errorHandlingService.handleError(error, 'Failed to load Synthese. Please try again later.');
      }
    );
  }
  private loadSchemaPdf() {
    this.courseService.getSchemaById(this.id).subscribe(
      (url) => {
        if (url) {
          this.pdfUrl = url;
        }
      },
      (error) => {
        this.errorHandlingService.handleError(error, 'Failed to load Schema. Please try again later.');
      }
    );
  }

  private loadExamPdf() {
    this.courseService.getControleById(this.id).subscribe(
      (url) => {
        this.pdfUrl = url?.ennonce;
        this.exam = url;
        this.isDevoirExists(this.id);
      },
      (error) => {
        this.errorHandlingService.handleError(error, 'Failed to load Exam. Please try again later.');
      }
    );
  }

  private loadControleFinalPdf() {
    this.courseService.getExamFinalById(this.id).subscribe(
      (url) => {
        this.pdfUrl = url.ennonce;
        this.exam = url;
        this.idControleFinalExists(this.id);
      },
      (error) => {
        this.errorHandlingService.handleError(error, 'Failed to load ControleFinal. Please try again later.');
      }
    );
  }

  // Methods for Exam
  private isDevoirExists(id: number) {
    this.courseService.isDevoirUploaded(id).subscribe((res) => {
      this.devoirePdfUrl = this.devoirePdfUrl + res.reponse;
      this.devoirExists = true;
      console.log(this.devoirePdfUrl);
    });
  }

  private uploadExamDevoir(formData: FormData) {
    this.courseService.uploadSolution(formData, this.exam.id).subscribe(
      (res) => {
        this.devoirExists = true;
      },
      (error) => {
        this.errorHandlingService.handleError(error, 'Error uploading file');
      }
    );
  }

  // Methods for ControleFinal
  private idControleFinalExists(id: number) {
    this.courseService.idControlFinalExists(id).subscribe((res) => {
      this.devoirePdfUrl = this.devoirePdfUrl + res.reponse;
      this.devoirExists = true;
      console.log(this.devoirePdfUrl);
    });
  }

  private uploadControleFinalDevoir(formData: FormData) {
    this.courseService.uploadFinalSolution(formData, this.exam.id).subscribe(
      (response) => {
        this.devoirExists = true;
      },
      (error) => {
        this.errorHandlingService.handleError(error, 'Error uploading ControleFinal file');
      }
    );
  }

  // Common Methods for File Selection and Deletion
  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    }
  }

  uploadDevoir(): void {
    if (!this.selectedFile || this.exam.id === null) {
      this.errorHandlingService.handleError(null, 'No file selected or no controle ID available');
      return;
    }

    const formData = new FormData();
    formData.append('file', this.selectedFile);

    if (this.isExam) {
      this.uploadExamDevoir(formData);
    } else if (this.isControleFinal) {
      this.uploadControleFinalDevoir(formData);
    }
  }

  deleteDevoir() {
    if (this.isExam) {
      this.courseService.deleteDevoir(this.id).subscribe((res) => {
        this.devoirExists = false;
      });
    } else if (this.isControleFinal) {
      this.courseService.deleteFinalDevoir(this.id).subscribe((res) => {
        this.devoirExists = false;
      });
    }
  }
}

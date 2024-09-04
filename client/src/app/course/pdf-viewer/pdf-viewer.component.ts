import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseService } from '../../services/course.service';
import { environment } from '../../../environments/environment';
import { ErrorHandlingService } from '../../services/error-handling.service';
import { SharedDataService } from '../../services/shared-data.service';
import Swal from 'sweetalert2';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-pdf-viewer',
  templateUrl: './pdf-viewer.component.html',
  styleUrls: ['./pdf-viewer.component.css'],
})
export class PdfViewerComponent {
  pdfUrl: string = '';
  isExam: boolean = false;
  isControleFinal: boolean = false;
  exam: any;
  isFirstCour: number = 1;
  isLastControle: number = 1;
  selectedFile: File | null = null;
  devoirePdfUrl: string = '';
  devoirExists: boolean = false;
  id: number = 0;
  res: any;
  urls: any;
  extension: string = '';
  sanitizedDocUrl?: SafeResourceUrl;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private courseService: CourseService,
    private errorHandlingService: ErrorHandlingService,
    private shared: SharedDataService,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('pdfid');
      if (idParam) {
        this.id = +idParam;
        const routePath = this.route.snapshot.routeConfig?.path;

        if (routePath?.includes('cour')) {
          this.loadParagrapheUrl();
          this.shared.setActiveDiv(`cour/${this.id}`);
        } else if (routePath?.includes('synthese')) {
          this.loadSynthesePdf();
          this.shared.setActiveDiv(`synthese/${this.id}`);
        } else if (routePath?.includes('exam')) {
          this.loadExamPdf();
          this.isExam = true;
          this.shared.setActiveDiv(`exam/${this.id}`);
        } else if (routePath?.includes('schema')) {
          this.loadSchemaPdf();
          this.shared.setActiveDiv(`schema/${this.id}`);
        } else if (routePath?.includes('ControleFinal')) {
          this.loadControleFinalPdf();
          this.isControleFinal = true;
          this.shared.setActiveDiv(`ControleFinal/${this.id}`);
        } else {
          console.error('Unknown route type');
        }
      } else {
        console.error('ID parameter is missing');
      }
    });
  }

  public loadDoc(path: string) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(
      'https://view.officeapps.live.com/op/embed.aspx?src=' + path
    );
  }

  // Load Methods
  private loadParagrapheUrl() {
    this.courseService.getParagrapheUrlById(this.id).subscribe(
      (url) => {
        this.pdfUrl = url;
        this.urls = url.split('.').length;
        this.extension = url.split('.')[this.urls - 1];
        console.log(this.extension);
      },
      (error) => {
        this.errorHandlingService.handleError(
          error,
          'Failed to load Cour. Please try again later.'
        );
      }
    );
  }

  private loadSynthesePdf() {
    this.courseService.getSyntheseById(this.id).subscribe(
      (url) => {
        if (url) {
          console.log(url);
          this.pdfUrl = url;
          this.urls = url.split('.').length;
          this.extension = url.split('.')[this.urls - 1];
          console.log(this.extension);
        }
      },
      (error) => {
        this.errorHandlingService.handleError(
          error,
          'Failed to load Synthese. Please try again later.'
        );
      }
    );
  }
  private loadSchemaPdf() {
    this.courseService.getSchemaById(this.id).subscribe(
      (url) => {
        if (url) {
          this.pdfUrl = url;
          this.urls = url.split('.').length;
          this.extension = url.split('.')[this.urls - 1];
          console.log(this.extension);
        }
      },
      (error) => {
        this.errorHandlingService.handleError(
          error,
          'Failed to load Schema. Please try again later.'
        );
      }
    );
  }

  private loadExamPdf() {
    this.courseService.getControleById(this.id).subscribe(
      (url) => {
        this.pdfUrl = url.ennonce;
        this.exam = url;
        this.urls = url.ennonce.split('.').length;
        this.extension = url.ennonce.split('.')[this.urls - 1];
        this.isDevoirExists(this.id);
      },
      (error) => {
        this.errorHandlingService.handleError(
          error,
          'Failed to load Exam. Please try again later.'
        );
      }
    );
  }

  private loadControleFinalPdf() {
    this.courseService.getExamFinalById(this.id).subscribe(
      (url) => {
        this.pdfUrl = url.ennonce;
        this.exam = url;
        this.urls = url.ennonce.split('.').length;
        this.extension = url.ennonce.split('.')[this.urls - 1];
        this.idControleFinalExists(this.id);
      },
      (error) => {
        this.errorHandlingService.handleError(
          error,
          'Failed to load ControleFinal. Please try again later.'
        );
      }
    );
  }

  // Methods for Exam
  private isDevoirExists(id: number) {
    this.courseService.isDevoirUploaded(id).subscribe((res) => {
      if (res.reponse != '') {
        this.devoirePdfUrl = res.reponse;
        this.devoirExists = true;
      }
      console.log(this.devoirePdfUrl);
    });
  }

  private uploadExamDevoir(formData: FormData) {
    Swal.fire({
      title: 'Uploading...',
      text: 'Please wait while the file is being uploaded.',
      allowOutsideClick: false,
      didOpen: () => {
        Swal.showLoading();
      },
    });
    this.courseService.uploadSolution(formData, this.exam.id).subscribe(
      (res) => {
        this.devoirExists = true;
        this.devoirePdfUrl = res;
        Swal.fire('Success', 'File uploaded successfully', 'success');
      },
      (error) => {
        this.errorHandlingService.handleError(error, 'Error uploading file');
      }
    );
    this.devoirePdfUrl = this.res.reponse;
  }

  // Methods for ControleFinal
  private idControleFinalExists(id: number) {
    this.courseService.idControlFinalExists(id).subscribe((res) => {
      this.devoirePdfUrl = res.reponse;
      this.devoirExists = true;
      console.log(this.devoirePdfUrl);
    });
  }

  private uploadControleFinalDevoir(formData: FormData) {
    Swal.fire({
      title: 'Uploading...',
      text: 'Please wait while the file is being uploaded.',
      allowOutsideClick: false,
      didOpen: () => {
        Swal.showLoading();
      },
    });
    this.courseService.uploadFinalSolution(formData, this.exam.id).subscribe(
      (response) => {
        this.devoirExists = true;
        console.log(response.reponse);
        Swal.fire('Success', 'File uploaded successfully', 'success');
        // this.devoirePdfUrl = response.reponse;
      },
      (error) => {
        this.errorHandlingService.handleError(
          error,
          'Error uploading ControleFinal file'
        );
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
      this.errorHandlingService.handleError(
        null,
        'No file selected or no controle ID available'
      );
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
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.isConfirmed) {
        // Show loading modal
        Swal.fire({
          title: 'Deleting...',
          text: 'Please wait while we process your request.',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });
        if (this.isExam) {
          this.courseService.deleteDevoir(this.id).subscribe((res) => {
            this.devoirExists = false;
            this.devoirePdfUrl = '';
            Swal.fire({
              title: 'Deleted!',
              text: 'Your file has been deleted.',
              icon: 'success',
            });
          });
        } else if (this.isControleFinal) {
          this.courseService.deleteFinalDevoir(this.id).subscribe((res) => {
            this.devoirExists = false;
            this.devoirePdfUrl = '';
            Swal.fire({
              title: 'Deleted!',
              text: 'Your file has been deleted.',
              icon: 'success',
            });
          });
        }
      }
    });
  }
}

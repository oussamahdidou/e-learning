import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CourseService } from '../../services/course.service';

@Component({
  selector: 'app-pdf-viewer',
  templateUrl: './pdf-viewer.component.html',
  styleUrl: './pdf-viewer.component.css',
})
export class PdfViewerComponent {
  @Input() pdfUrl: string | undefined;

  constructor(
    private route: ActivatedRoute,
    private courseService: CourseService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('id');
      if (idParam) {
        const id = +idParam;
        this.courseService
          .getCourPdfUrlById(id)
          .subscribe((url: string | undefined) => {
            this.pdfUrl = url;
          });
      } else {
        console.error('ID parameter is missing');
      }
    });
  }
}

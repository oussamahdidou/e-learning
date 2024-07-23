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
  isFirstCour: number = 1;
  isLastControle: number = 1;
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
          this.IsfirstItem();
        } else if (routePath?.includes('synthese')) {
          this.courseService.getSyntheseById(id).subscribe((url) => {
            this.pdfUrl = url;
          });
        } else if (routePath?.includes('exam')) {
          this.courseService.getChapterNumber(id).subscribe((chapterNumber) => {
            console.log(chapterNumber);
            if (chapterNumber !== null) {
              // this.courseService
              //   .getControleById(chapterNumber)
              //   .subscribe((pdfUrl) => {
              //     this.pdfUrl = pdfUrl;
              //   });
              // this.IsLastItem();
            } else {
              console.log('Chapter not found');
            }
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
  nextItem(): void {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('id');
      if (idParam) {
        const id = +idParam;
        const routePath = this.route.snapshot.routeConfig?.path;
        if (routePath?.includes('cour')) {
          this.router.navigate(['/course/lecture/', id]);
        }
        if (routePath?.includes('schema')) {
          this.router.navigate(['/course/synthese/', id]);
        }
        if (routePath?.includes('synthese')) {
          this.router.navigate(['/course/quiz/', id]);
        }
        if (routePath?.includes('exam')) {
          this.router.navigate(['/course/cour/', id + 1]);
        }
      }
    });
  }

  previousItem(): void {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('id');
      if (idParam) {
        const id = +idParam;
        const routePath = this.route.snapshot.routeConfig?.path;
        if (routePath?.includes('cour')) {
          this.router.navigate(['/course/quiz/', id - 1]);
        }
        if (routePath?.includes('schema')) {
          this.router.navigate(['/course/lecture/', id]);
        }
        if (routePath?.includes('synthese')) {
          this.router.navigate(['/course/schema/', id]);
        }
        if (routePath?.includes('exam')) {
          this.router.navigate(['/course/quiz/', id]);
        }
      }
    });
  }
  IsfirstItem() {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('id');
      if (idParam) {
        const id = +idParam;
        const routePath = this.route.snapshot.routeConfig?.path;

        if (routePath?.includes('cour')) {
          this.courseService.getFirstChapterId(id).subscribe((state) => {
            if (state) {
              this.isFirstCour = 0;
            } else {
              this.isFirstCour = 1;
            }
          });
        }
      }
    });
  }
  IsLastItem() {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('id');
      if (idParam) {
        const id = +idParam;
        const routePath = this.route.snapshot.routeConfig?.path;

        if (routePath?.includes('exam')) {
          this.courseService.isLastChapter(id).subscribe((state) => {
            if (state) {
              this.isLastControle = 0;
            } else {
              this.isLastControle = 1;
            }
          });
        }
      }
    });
  }
}

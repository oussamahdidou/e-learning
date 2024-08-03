import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseService } from '../../services/course.service';
import { environment } from '../../../environments/environment';
import Swal from 'sweetalert2';
import { ErrorHandlingService } from '../../services/error-handling.service';

@Component({
  selector: 'app-lecture',
  templateUrl: './lecture.component.html',
  styleUrl: './lecture.component.css',
})
export class LectureComponent {
  @Input() vdUrl: string | undefined;
  host = environment.apiUrl;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private courseService: CourseService,
    private errorHandlingService: ErrorHandlingService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('lectureid');
      if (idParam) {
        const id = +idParam;
        this.courseService
          .getVdUrlById(id)
          .subscribe((url: string | undefined) => {
            this.vdUrl = url;
          },
          (error) =>{
            this.errorHandlingService.handleError(error,'Failed to load video. Please try again later.')
          }
        );
      } else {
        this.errorHandlingService.handleError(null,'ID parameter is missing')
      }
    });
  }
}

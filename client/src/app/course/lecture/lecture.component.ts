import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseService } from '../../services/course.service';
import { environment } from '../../../environments/environment';
import { MatSnackBar } from '@angular/material/snack-bar';

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
    private snackBar: MatSnackBar
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
          });
          console.error('videourl', this.vdUrl);
      } else {
        console.error('ID parameter is missing');
      }
    });
  }
}

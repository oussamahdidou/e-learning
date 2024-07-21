import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CourseService } from '../../services/course.service';

@Component({
  selector: 'app-lecture',
  templateUrl: './lecture.component.html',
  styleUrl: './lecture.component.css',
})
export class LectureComponent {
  @Input() vdUrl: string | undefined;

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
          .getVdUrlById(id)
          .subscribe((url: string | undefined) => {
            this.vdUrl = url;
          });
      } else {
        console.error('ID parameter is missing');
      }
    });
  }
}

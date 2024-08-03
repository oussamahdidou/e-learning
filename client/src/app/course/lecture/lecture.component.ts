import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseService } from '../../services/course.service';
import { environment } from '../../../environments/environment';
import Swal from 'sweetalert2';

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
            console.error('Error fetching video URL:', error);
            Swal.fire({
              title: 'Error!',
              text: 'Failed to load video. Please try again later.',
              icon: 'error',
              confirmButtonText: 'OK'
            });
          }
        );
      } else {
        console.error('ID parameter is missing');
        Swal.fire({
          title: 'Error!',
          text:'Lecture ID is missing from the URL.',
          icon: 'error',
          confirmButtonText: 'OK'
        })
      }
    });
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseService } from '../services/course.service';
import { Module } from '../interfaces/dashboard';
import { ErrorHandlingService } from '../services/error-handling.service';
import { SharedDataService } from '../services/shared-data.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css'],
})
export class CourseComponent {
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private courseService: CourseService,
    private errorHandlingService: ErrorHandlingService,
    private sharedDataService: SharedDataService
  ) {}
  module: Module | undefined;

  ngOnInit() {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.courseService.isEligible(id).subscribe({
      next: (data) => {
        if (data.isEligible) {
          console.log('dkhel');
          this.courseService.getCourseById(id).subscribe(
            (module) => {
              console.log(module);
              this.module = module;
            },
            (error) => {
              this.errorHandlingService.handleError(
                error,
                'Error Fetching Module'
              );
              this.router.navigate(['/']);
            }
          );
        } else {
          this.router.navigate(['/institutions']);
          this.showWarningModal(data.modules);
        }
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
  showWarningModal(modules: any[]): void {
    const moduleLinks = modules
      .map(
        (module) =>
          `<a href="/course/${module.id}/testniveau/${module.id}" style="color: blue; text-decoration: none;">
        ${module.name} (seuill: ${module.seuill})
      </a>`
      )
      .join('<br>');

    Swal.fire({
      icon: 'warning',
      title: 'Access refusee',
      html: `tu peux pas acceder a se module il faut que tu passe un test de niveau dans les modules suivants: <br> ${moduleLinks}`,
      confirmButtonText: 'OK',
    });
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseService } from '../services/course.service';
import { Module } from '../interfaces/dashboard';
import { ErrorHandlingService } from '../services/error-handling.service';

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
    private errorHandlingService: ErrorHandlingService
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
          this.errorHandlingService.handleError(
            'your not elligible',
            `you must have `
          );
          this.router.navigate(['/']);
        }
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}

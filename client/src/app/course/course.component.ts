import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseService } from '../services/course.service';
import { Module } from '../interfaces/dashboard';
import { ErrorHandlingService } from '../services/error-handling.service';
import { SharedDataService } from '../services/shared-data.service';

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
          this.sharedDataService.setData(data);
          this.router.navigate(['/institutions']);
        }
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}

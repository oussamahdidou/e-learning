import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DashboardService } from '../../services/dashboard.service';

@Component({
  selector: 'app-paragraphe',
  templateUrl: './paragraphe.component.html',
  styleUrl: './paragraphe.component.css',
})
export class ParagrapheComponent {
  paragrapheid!: number;
  paragraphe: any;
  constructor(
    private readonly route: ActivatedRoute,
    private readonly dashboardservice: DashboardService
  ) {
    this.route.params.subscribe((params) => {
      this.paragrapheid = params['id'];
      dashboardservice.getparagraphebyid(this.paragrapheid).subscribe(
        (response) => {
          this.paragraphe = response;
          console.log(this.paragraphe);
        },
        (error) => {}
      );
    });
  }
}

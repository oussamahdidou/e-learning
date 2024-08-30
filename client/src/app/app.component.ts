import { Component, OnInit } from '@angular/core';
import { environment } from '../environments/environment';
import {
  ChildrenOutletContexts,
  RouterLink,
  RouterOutlet,
} from '@angular/router';
import { slideInAnimation } from './animation';
import { TeacherProgressServiceService } from './services/teacher-progress-service.service';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  animations: [slideInAnimation],
})
export class AppComponent implements OnInit {
  title = 'client';

  constructor(
    private contexts: ChildrenOutletContexts,
    private readonly teacherprogress: TeacherProgressServiceService,
    private readonly authservice: AuthService
  ) {
    console.log(environment.apiUrl);
  }
  ngOnInit(): void {
    this.authservice.$IsTeacher.subscribe((isteacher) => {
      this.teacherprogress.showFeedbackModal(
        this.authservice.token.unique_name
      );
    });
  }
  getRouteAnimationData() {
    return this.contexts.getContext('primary')?.route?.snapshot?.data?.[
      'animation'
    ];
  }
}

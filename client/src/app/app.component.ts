import { Component, OnInit } from '@angular/core';
import { environment } from '../environments/environment';
import { TeacherProgressServiceService } from './services/teacher-progress-service.service';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'client';
  constructor(
    private readonly teacherprogress: TeacherProgressServiceService,
    private readonly authservice: AuthService
  ) {
    authservice.$IsTeacher.subscribe((isteacher) => {
      teacherprogress.showFeedbackModal(authservice.token.unique_name);
    });
  }
}

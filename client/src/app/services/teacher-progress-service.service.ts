import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import Swal from 'sweetalert2';
import { environment } from '../../environments/environment';
import { DashboardService } from './dashboard.service';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class TeacherProgressServiceService {
  constructor(
    private http: HttpClient,
    private readonly dashbaordservice: DashboardService
  ) {}

  submitFeedback(userId: string, comment: number): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Dashboard/TeacherProgress/${userId}`,
      {
        newProgress: comment,
      }
    );
  }

  showFeedbackModal(userId: string): void {
    this.dashbaordservice.getTeacherbyid(userId).subscribe((response) => {
      const lastFeedbackDate = new Date(response.lastChapterProgressUpdate);
      const currentDate = new Date();

      const oneMonth = 1000 * 60 * 60 * 24 * 30; // Approx one month in milliseconds

      if (currentDate.getTime() - lastFeedbackDate.getTime() > oneMonth) {
        Swal.fire({
          title: 'Avancement dans le cours',
          text: ' Quel est le dernier chapitre que vous avez traité cette semaine ?',
          icon: 'question',
          input: 'number',

          showCancelButton: true,
          confirmButtonText: 'Submit',
          cancelButtonText: 'Later',
        }).then((result) => {
          if (result.isConfirmed) {
            this.submitFeedback(userId, result.value).subscribe(() => {
              console.log('Feedback submitted successfully');
            });
          }
        });
      }
    });
  }
}

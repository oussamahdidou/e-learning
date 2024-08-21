import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DashboardService } from '../../services/dashboard.service';
import { AuthService } from '../../services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-paragraphe',
  templateUrl: './paragraphe.component.html',
  styleUrl: './paragraphe.component.css',
})
export class ParagrapheComponent {
  SelectParagraphe(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('File', file);
      formData.append('Id', this.paragrapheid.toString());

      // Show loading modal
      Swal.fire({
        title: 'Uploading...',
        text: 'Please wait while the file is being uploaded.',
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });

      this.dashboardservice.updateparagraphe(formData).subscribe(
        (response) => {
          // Close the loading modal and show success message
          Swal.fire('Success', 'File uploaded successfully', 'success');
          this.paragraphe.contenu = response.contenu;
        },
        (error) => {
          // Close the loading modal and show error message
          Swal.fire('Error', `${error.error}`, 'error');
        }
      );
    }
  }
  paragrapheid!: number;
  paragraphe: any;
  constructor(
    private readonly route: ActivatedRoute,
    private readonly dashboardservice: DashboardService,
    public readonly authservice: AuthService
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

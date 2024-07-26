import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotPasswordComponent {
  
  constructor(private authService: AuthService) {}

  email: string = '';

  forgotPassword() {
    this.authService.forgotPassword( this.email,).subscribe(
      (response) => {
        Swal.fire({
          title: 'Success',
          text: response.token,
          icon: 'success',
        });
      },
      (error) => {
        Swal.fire({
          title: 'Error',
          text: `${error.error}`,
          icon: 'error',
        });
      }
    );
  }

}

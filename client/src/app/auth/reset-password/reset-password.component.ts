import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent {
  
  constructor(private authService: AuthService) {}
  token = new URLSearchParams(window.location.search).get('token');
  email = new URLSearchParams(window.location.search).get('email');
  password: string = '';
  confirmpassword: string= '';

  resetPassword() {
    console.log('token : ',this.token,'                  email : ',this.email!, this.password);
    this.authService.resetPassword( this.password, this.confirmpassword, this.email!, this.token!).subscribe(
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

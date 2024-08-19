import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-verify-email',
  templateUrl: './verify-email.component.html',
  styleUrl: './verify-email.component.css'
})
export class VerifyEmailComponent {
  constructor(private authService: AuthService,private router: Router) {}
  token = new URLSearchParams(window.location.search).get('token');
  email = new URLSearchParams(window.location.search).get('email');
  ngOnInit() {
    this.email = new URLSearchParams(window.location.search).get('email');
    this.token = new URLSearchParams(window.location.search).get('token');
    this.verifyEmail();
}
verifyEmail(){
  this.authService.verifyEmail( this.email!, this.token!).subscribe(
    (response) => {
      Swal.fire({
        title: 'Success',
        text: response.token,
        icon: 'success',
      }).then(
        () => {
          this.router.navigate(['/auth/login']);
        }
      )

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

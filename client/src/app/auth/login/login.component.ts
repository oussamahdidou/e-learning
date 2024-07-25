import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  constructor(private authService: AuthService) {}
  
  username: string = '';
  password: string = '';

  login() {
    this.authService.login(this.username, this.password).subscribe(
      (response) => {
        Swal.fire({
          title: 'Welcome!',
          text: `${response.username}`,
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

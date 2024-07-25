import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  
  constructor(private authService: AuthService) {}
  
  username: string = '';
  email: string= '';
  password: string = '';
  confirmpassword: string= '';

  register() {
    this.authService.registeruser(this.username, this.email, this.password, this.confirmpassword).subscribe(
      (response) => {
        Swal.fire({
          title: 'Confirm email',
          text: `A confirmation link sent to your email adress email : ${response.token}`,
          icon: 'success',
        });
      },
      (error) => {
        console.log(error);
        console.log("1111111111111111111111111111111111111111111111111111111111");
        Swal.fire({
          title: 'Error',
          text: `${error.error}`,
          icon: 'error',
        });
      }
    );
  }

}

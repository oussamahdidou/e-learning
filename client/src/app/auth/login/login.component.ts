import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import Swal from 'sweetalert2';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginForm: FormGroup;

  constructor(private authService: AuthService) {
    this.loginForm= new FormGroup({
      userNameV: new FormControl('',[Validators.required]),
      PasswordV: new FormControl('',[Validators.required]),
    })
  }
  
  username: string = '';
  password: string = '';

  login() {
    console.log(this.loginForm.controls);
    console.log('Form Value:', this.loginForm.value);
    if (this.loginForm.valid) {
      const { userNameV, PasswordV } = this.loginForm.value;
      this.authService.login(userNameV, PasswordV).subscribe(
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
    }else{
      const { userNamev, Passwordv } = this.loginForm.value;
      console.log("1111111111111111111111111111111111111111111111111111111111111"+userNamev.value);
    }
  }
}

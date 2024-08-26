import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import Swal from 'sweetalert2';
import { Time } from '@angular/common';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(private authService: AuthService) {
    this.registerForm= new FormGroup({
      username: new FormControl('',[Validators.required]),
      email: new FormControl('',[Validators.required,Validators.email]),
      password: new FormControl('',[Validators.required,passwordValidator()]),
      confirmpassword: new FormControl('',[Validators.required]),
      nom: new FormControl('',[Validators.required]),
      prenom: new FormControl('',[Validators.required]),
      datenaissaince: new FormControl('',[Validators.required]),
      etablissement: new FormControl('',[Validators.required]),
      branche: new FormControl('',[Validators.required]),
      niveau: new FormControl('',[Validators.required]),
    })
  }


  // username: string = '';
  // email: string = '';
  // password: string = '';
  // confirmpassword: string = '';
  // nom: string = '';
  // prenom: string= '';
  // datenaissaince: Date= new Date();
  // etablissement: string= '';
  // branche: string= '';
  // niveau: string= '';


  register() {
    if (this.registerForm.valid) {
      const { nom, prenom,datenaissaince,etablissement,branche,niveau,email,username,password,confirmpassword, } = this.registerForm.value;
      this.authService
        .registeruser(
          nom,
          prenom,
          datenaissaince,
          etablissement,
          branche,
          niveau,
          username,
          email,
          password,
          confirmpassword
        )
        .subscribe(
          (response) => {
            Swal.fire({
              title: 'Confirm email',
              text: `A confirmation link sent to your email adress email : ${response.token}`,
              icon: 'success',
            });
          },
          (error) => {
            
            console.log(error);
            console.log(
              '1111111111111111111111111111111111111111111111111111111111'
            );
            Swal.fire({
              title: 'Error',
              text: `${error.message}`,
              icon: 'error',
            });
          }
        );
    }else{
      Swal.fire({
        title: 'Error',
        text: `One or all Field(s) doesn't respect fields format or are empty .`,
        icon: 'error',
      });
    }
  }
}

export function passwordValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (!control.value) {
      return null; // No error if the control is empty
    }

    const value = control.value;

    const errors: ValidationErrors = {};

    // Check for minimum length
    if (value.length < 8) {
      errors['minlength'] = 'Password must be at least 8 characters long.';
    }

    // Check for at least one digit
    if (!/\d/.test(value)) {
      errors['digit'] = 'Password must include at least one digit.';
    }

    // Check for at least one lowercase letter
    if (!/[a-z]/.test(value)) {
      errors['lowercase'] = 'Password must include at least one lowercase letter.';
    }

    // Check for at least one uppercase letter
    if (!/[A-Z]/.test(value)) {
      errors['uppercase'] = 'Password must include at least one uppercase letter.';
    }

    // Check for at least one special character
    if (!/\W/.test(value)) {
      errors['special'] = 'Password must include at least one special character.';
    }

    return Object.keys(errors).length ? errors : null;
  };
}

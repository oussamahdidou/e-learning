import { Component } from '@angular/core';
import Swal from 'sweetalert2';
import { AuthService } from '../../services/auth.service';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';

@Component({
  selector: 'app-teacher-register',
  templateUrl: './teacher-register.component.html',
  styleUrl: './teacher-register.component.css',
  
})
export class TeacherRegisterComponent {
  isLoading = false;
  currentStep = 1;
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
      justification: new FormControl('',[Validators.required]),
      status: new FormControl('',[Validators.required]),
      specialite: new FormControl('',[Validators.required]),
      phonenumber: new FormControl('',[Validators.pattern(/^(?:\+2127\d{8}|\+2126\d{8}|07\d{8}|06\d{8})$/)]),
    })
  }
  fileJustification: File| null = null;
  // username: string = '';
  // email: string = '';
  // password: string = '';
  // confirmpassword: string = '';
  // nom: string = '';
  // prenom: string= '';
  // datenaissaince: Date= new Date();
  // justification: File| null = null;
  // etablissement: string= '';
  SelectPdf(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      this.fileJustification=file;
      // const formData = new FormData();
      // formData.append('File', file);
      // formData.append('Id', this.chapterid.toString());

      // Show loading modal
      Swal.fire({
        title: 'Uploading...',
        text: 'Please wait while the Justification is being uploaded.',
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });

     
    }
  }

  onFileChange(event: any) {
    const file = event.target.files[0];
    this.registerForm.patchValue({ justification: file });
    this.registerForm.get('justification')?.updateValueAndValidity();
  }

  isFirstStepValid() {
    return this.registerForm.get('nom')?.valid &&
           this.registerForm.get('prenom')?.valid &&
           this.registerForm.get('datenaissaince')?.valid &&
           this.registerForm.get('etablissement')?.valid &&
           this.registerForm.get('justification')?.valid &&
           this.registerForm.get('status')?.valid &&
           this.registerForm.get('specialite')?.valid;
  }

  goToNextStep() {
    if (this.isFirstStepValid()) {
      this.currentStep = 2;
    }
  }

  register() {
    this.isLoading = true; 
    if (this.registerForm.valid) {
      const { nom, prenom, datenaissaince, etablissement,phonenumber ,email,status, specialite,username, password, confirmpassword } = this.registerForm.value;
      const justification = this.registerForm.get('justification')?.value;
      const formattedDate = new Date(datenaissaince).toISOString(); 

      if (!justification) {
        this.isLoading = false; 
        Swal.fire({
          title: 'Error',
          text: 'No File Selected',
          icon: 'error',
        });
        return;
      }

      const formData = new FormData();
      formData.append('Teacher_Nom', nom);
      formData.append('Teacher_Prenom', prenom);
      formData.append('Teacher_DateDeNaissance', formattedDate);
      formData.append('Teacher_Etablissement', etablissement);

      if (justification instanceof File && this.isValidFileType(justification)) {
        formData.append('JustificatifDeLaProfession', justification, justification.name);
      } else {
        this.isLoading = false; 
        Swal.fire({
          title: 'Error',
          text: 'Invalid File Type. Please select an image (JPEG, PNG, etc.)',
          icon: 'error',
        });
        return;
      }

      formData.append('UserName', username);
      formData.append('Email', email);
      formData.append('Status', status);
      formData.append('Specialite', specialite);
      formData.append('PhoneNumber', phonenumber);
      formData.append('Password', password);
      formData.append('ConfirmPassword', confirmpassword);
      formData.append('PhoneNumber ',phonenumber)

      this.authService.teacherregisteruser(formData)
        .subscribe(
          (response) => {
            this.isLoading = false; 
            console.log('Registration successful:', response);
            Swal.fire({
              title: 'Confirm email',
              text: `A confirmation link sent to your email adress email : ${response.token}`,
              icon: 'success',
            });
          },
          (error) => {
            this.isLoading = false; 
            console.error('Registration failed:', error.message);
            Swal.fire({
              title: 'Error',
              text:  `Registration failed. ${error.error}`,
              icon: 'error',
            });
          }
        );
    }
  }

  isValidFileType(file: File): boolean {
    const allowedMimeTypes = ['image/jpeg', 'image/png', 'image/gif'];
    return allowedMimeTypes.includes(file.type);
  }

}


export function passwordValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (!control.value) {
      return null; // No error if the control is empty
    }

    const value = control.value;

    const errors: ValidationErrors = {};

    if (value.length < 8) {
      errors['minlength'] = 'Password must be at least 8 characters long.';
    }

    if (!/\d/.test(value)) {
      errors['digit'] = 'Password must include at least one digit.';
    }

    if (!/[a-z]/.test(value)) {
      errors['lowercase'] = 'Password must include at least one lowercase letter.';
    }

    if (!/[A-Z]/.test(value)) {
      errors['uppercase'] = 'Password must include at least one uppercase letter.';
    }
    if (!/\W/.test(value)) {
      errors['special'] = 'Password must include at least one special character.';
    }

    return Object.keys(errors).length ? errors : null;
  };
}
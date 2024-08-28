import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { AppRoutingModule } from '../app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { VerifyEmailComponent } from './verify-email/verify-email.component';
import { RouterModule, Routes } from '@angular/router';
import { TeacherRegisterComponent } from './teacher-register/teacher-register.component';
import { InitialRegisterComponent } from './initial-register/initial-register.component';

const routes: Routes = [
  {
    path: '',
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'student-registeration', component: RegisterComponent, data: { animation: 'student-register' } },
      { path: 'forgot-password', component: ForgotPasswordComponent },
      { path: 'reset-password', component: ResetPasswordComponent },
      { path: 'verify-email', component: VerifyEmailComponent },
      { path:'register', component: InitialRegisterComponent , data: { animation: 'register' }},
      { path:'teacher-registration', component:TeacherRegisterComponent, data: { animation: 'teacher-register' }},
    ],
  },
];

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    VerifyEmailComponent,
    TeacherRegisterComponent,
    InitialRegisterComponent,
  ],
  imports: [CommonModule, FormsModule, ReactiveFormsModule,CommonModule, RouterModule.forChild(routes)],
})
export class AuthModule {}

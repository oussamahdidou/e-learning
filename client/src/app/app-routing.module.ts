import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NiveauScolairesComponent } from './institutions/niveauscolaires/niveauscolaires.component';
import { ModulesComponent } from './institutions/modules/modules.component';
import { InstitutionsComponent } from './institutions/institutions/institutions.component';

 
  
 
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { ForgotPasswordComponent } from './auth/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './auth/reset-password/reset-password.component';
import { VerifyEmailComponent } from './auth/verify-email/verify-email.component';

const routes: Routes = [
  {path:'login',component:LoginComponent},
  {path:'register',component:RegisterComponent},
  {path:'forgot-password',component:ForgotPasswordComponent},
  {path:'reset-password',component:ResetPasswordComponent},
  {path:'verify-email',component:VerifyEmailComponent},
 
  {
      path:"institutions",
      loadChildren:()=>import("./institutions/institutions.module").then((m)=>m.InstitutionsModule)
  
  
    }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }



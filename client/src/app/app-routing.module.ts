import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NiveauScolairesComponent } from './institutions/niveauscolaires/niveauscolaires.component';
import { ModulesComponent } from './institutions/modules/modules.component';
import { InstitutionsComponent } from './institutions/institutions/institutions.component';

 const routes: Routes = [
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



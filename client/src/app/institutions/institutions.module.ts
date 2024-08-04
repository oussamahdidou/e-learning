import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Route, RouterModule, Routes } from '@angular/router';
import { InstitutionsComponent } from './institutions/institutions.component';
import { NiveauScolairesComponent } from './niveauscolaires/niveauscolaires.component';
import { ModulesComponent } from './modules/modules.component';
import { MatButtonModule } from '@angular/material/button';
import { ProfileModule } from '../profile/profile.module';
import { InstitutionService } from '../services/institution.service';
const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        component: InstitutionsComponent,
        title: 'institutions',
      },
      {
        path: 'niveau-scolaire/:id',
        component: NiveauScolairesComponent,
        title: 'Niveau Scolaire',
      },
      {
        path: 'modules/:id',
        component: ModulesComponent,
        title: 'module',
      },
    ],
  },
];

@NgModule({
  declarations: [
    InstitutionsComponent,
    NiveauScolairesComponent,
    ModulesComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatButtonModule,
    ProfileModule,
  ],
  providers: [InstitutionService],
  exports: [InstitutionsComponent, NiveauScolairesComponent, ModulesComponent],
})
export class InstitutionsModule {}

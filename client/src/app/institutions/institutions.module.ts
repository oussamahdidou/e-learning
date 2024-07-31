import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Route, RouterModule, Routes } from '@angular/router';
import { InstitutionsComponent } from './institutions/institutions.component';
import { NiveauScolairesComponent } from './niveauscolaires/niveauscolaires.component';
import { ModulesComponent } from './modules/modules.component';
import { MatButtonModule } from '@angular/material/button';
const routes: Routes = [
  {
    path: '',
    children: [
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
      {
        path: '',
        component: InstitutionsComponent,
        title: 'institutions',
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
  imports: [CommonModule, RouterModule.forChild(routes), MatButtonModule],
  exports: [InstitutionsComponent, NiveauScolairesComponent, ModulesComponent],
})
export class InstitutionsModule {}

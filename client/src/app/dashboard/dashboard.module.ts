import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeachersComponent } from './teachers/teachers.component';
import { InstitutionsTableComponent } from './institutions-table/institutions-table.component';
import { NiveauScolairesTableComponent } from './niveau-scolaires-table/niveau-scolaires-table.component';
import { ModulesTableComponent } from './modules-table/modules-table.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { ApprobationTableComponent } from './approbation-table/approbation-table.component';
import { CreatecontroleComponent } from './createcontrole/createcontrole.component';
import { CreatechapitreComponent } from './createchapitre/createchapitre.component';
import { CreatequizComponent } from './createquiz/createquiz.component';
import { MatCardModule } from '@angular/material/card';
import { UpdatequizComponent } from './updatequiz/updatequiz.component';
const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'institutionstable',
        component: InstitutionsTableComponent,
      },
      {
        path: 'niveautable/:id',
        component: NiveauScolairesTableComponent,
      },
      {
        path: 'moduletable/:id',
        component: ModulesTableComponent,
      },
      {
        path: 'teacherstable',
        component: TeachersComponent,
      },
      {
        path: 'approbtionstable',
        component: ApprobationTableComponent,
      },
      {
        path: 'createcontrole/:id',
        component: CreatecontroleComponent,
      },
      {
        path: 'createchapitre/:id',
        component: CreatechapitreComponent,
      },
      {
        path: 'createquiz/:id',
        component: CreatequizComponent,
      },
      {
        path: 'updatequiz/:id',
        component: UpdatequizComponent,
      },
    ],
  },
];

@NgModule({
  declarations: [
    TeachersComponent,
    InstitutionsTableComponent,
    NiveauScolairesTableComponent,
    ModulesTableComponent,
    SidenavComponent,
    ApprobationTableComponent,
    CreatecontroleComponent,
    CreatechapitreComponent,
    CreatequizComponent,
    UpdatequizComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatTableModule,
    MatSortModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatIconModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
  ],
})
export class DashboardModule {}

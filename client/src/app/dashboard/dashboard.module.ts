import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { VgCoreModule } from '@videogular/ngx-videogular/core';
import { VgControlsModule } from '@videogular/ngx-videogular/controls';
import { VgOverlayPlayModule } from '@videogular/ngx-videogular/overlay-play';
import { VgBufferingModule } from '@videogular/ngx-videogular/buffering';
import { MatStepperModule } from '@angular/material/stepper';
import { TeachersComponent } from './teachers/teachers.component';
import { InstitutionsTableComponent } from './institutions-table/institutions-table.component';
import { NiveauScolairesTableComponent } from './niveau-scolaires-table/niveau-scolaires-table.component';
import { ModulesTableComponent } from './modules-table/modules-table.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { ApprobationTableComponent } from './approbation-table/approbation-table.component';
import { CreatecontroleComponent } from './createcontrole/createcontrole.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DashboardService } from '../services/dashboard.service';
import { CreateChapterQuizComponent } from './create-chapter-quiz/create-chapter-quiz.component';
import { MatRadioModule } from '@angular/material/radio';
import { ChapterComponent } from './chapter/chapter.component';
import { ModuleComponent } from './module/module.component';
import { ModuleRequirementsDialogComponent } from './module-requirements-dialog/module-requirements-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { ControleComponent } from './controle/controle.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { UpdateControleChaptersDialogComponent } from './update-controle-chapters-dialog/update-controle-chapters-dialog.component';
import { DashboardtableComponent } from './dashboardtable/dashboardtable.component';
import {
  BaseChartDirective,
  provideCharts,
  withDefaultRegisterables,
} from 'ng2-charts';

import { AdminGuardService } from '../services/admin-guard.service';
import { DashboardGuardService } from '../dashboard-guard.service';
import { NavbarComponent } from './navbar/navbar.component';
import { SafeUrlPipe } from '../pipes/safe-url.pipe';
import { ParagrapheComponent } from './paragraphe/paragraphe.component';
const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'institutionstable',
        canActivate: [DashboardGuardService],
        component: InstitutionsTableComponent,
      },
      {
        path: 'niveautable/:id',
        canActivate: [DashboardGuardService],
        component: NiveauScolairesTableComponent,
      },
      {
        path: 'moduletable/:id',
        canActivate: [DashboardGuardService],
        component: ModulesTableComponent,
      },
      {
        path: 'teacherstable',
        canActivate: [AdminGuardService],
        component: TeachersComponent,
      },
      {
        path: 'approbtionstable',
        canActivate: [AdminGuardService],
        component: ApprobationTableComponent,
      },
      {
        path: 'createcontrole/:id',
        canActivate: [DashboardGuardService],
        component: CreatecontroleComponent,
      },
      {
        path: 'chapter/:id',
        canActivate: [DashboardGuardService],
        component: ChapterComponent,
      },
      {
        path: 'createchapter/:id',
        canActivate: [DashboardGuardService],
        component: CreateChapterQuizComponent,
      },
      {
        path: 'module/:id',
        canActivate: [DashboardGuardService],
        component: ModuleComponent,
      },
      {
        path: 'controle/:id',
        canActivate: [DashboardGuardService],
        component: ControleComponent,
      },
      {
        path: 'paragraphe/:id',
        canActivate: [DashboardGuardService],
        component: ParagrapheComponent,
      },
      {
        path: '',
        canActivate: [DashboardGuardService],
        component: DashboardtableComponent,
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
    SafeUrlPipe,
    CreateChapterQuizComponent,
    ChapterComponent,
    ModuleComponent,
    ModuleRequirementsDialogComponent,
    ControleComponent,
    UpdateControleChaptersDialogComponent,
    DashboardtableComponent,
    NavbarComponent,
    ParagrapheComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatTableModule,
    MatSortModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    BaseChartDirective,
    MatStepperModule,
    MatIconModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatRadioModule,
    MatCheckboxModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    PdfViewerModule,
    VgCoreModule,
    VgControlsModule,
    VgOverlayPlayModule,
    VgBufferingModule,
    MatDialogModule,
    MatSelectModule,
  ],
  providers: [DashboardService],
})
export class DashboardModule {}

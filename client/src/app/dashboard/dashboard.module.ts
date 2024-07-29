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
const routes: Routes = [
  {
    path: '',
    children: [
      { path: 'institutionstable', component: InstitutionsTableComponent },
      { path: 'niveautable/:id', component: NiveauScolairesTableComponent },
      { path: 'moduletable/:id', component: ModulesTableComponent },
      { path: 'teacherstable', component: TeachersComponent },
      { path: 'approbtionstable', component: ApprobationTableComponent },
      { path: 'createcontrole/:id', component: CreatecontroleComponent },
      { path: 'chapter/:id', component: ChapterComponent },
      { path: 'createchapter/:id', component: CreateChapterQuizComponent },
      { path: 'module/:id', component: ModuleComponent },
      { path: 'controle/:id', component: ControleComponent },
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

    CreateChapterQuizComponent,
    ChapterComponent,
    ModuleComponent,
    ModuleRequirementsDialogComponent,
    ControleComponent,
    UpdateControleChaptersDialogComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatTableModule,
    MatSortModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
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

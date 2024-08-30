import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
//import { InstitutionmComponent } from './institutionm/institutionm.component';

import { RouterModule, Routes } from '@angular/router';
import { EspaceprofService } from '../services/espaceprof.service';
import { NiveauscolairemComponent } from './niveauscolaire/niveauscolairem.component';
import { InstutitionmmComponent } from './instutitionmm/instutitionmm.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { ObjetsPedagogiquesComponent } from './objets-pedagogiques/objets-pedagogiques.component';
import { MatButtonModule } from '@angular/material/button';
import { CreateobjetdialogComponent } from './createobjetdialog/createobjetdialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSortModule } from '@angular/material/sort';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTableModule } from '@angular/material/table';
import { VgBufferingModule } from '@videogular/ngx-videogular/buffering';
import { VgControlsModule } from '@videogular/ngx-videogular/controls';
import { VgCoreModule } from '@videogular/ngx-videogular/core';
import { VgOverlayPlayModule } from '@videogular/ngx-videogular/overlay-play';
import { BaseChartDirective } from 'ng2-charts';
import { PdfViewerModule } from 'ng2-pdf-viewer';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        component: InstutitionmmComponent,
        title: 'institutions',
      },
      {
        path: 'niveauscolaire/:id',
        component: NiveauscolairemComponent,
        title: 'Niveau Scolaire',
      },

      {
        path: 'ObjetPedagogique/:id',
        component: ObjetsPedagogiquesComponent,
        title: 'ObjetPedagogique',
      },
    ],
  },
];

@NgModule({
  declarations: [
    InstutitionmmComponent,
    NiveauscolairemComponent,
    NiveauscolairemComponent,
    InstutitionmmComponent,
    NavbarComponent,
    ObjetsPedagogiquesComponent,
    CreateobjetdialogComponent,
  ],
  imports: [
    MatButtonModule,
    CommonModule,
    ReactiveFormsModule,
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
  providers: [EspaceprofService],
  exports: [
    InstutitionmmComponent,
    NiveauscolairemComponent,
    ObjetsPedagogiquesComponent,
  ],
})
export class EspaceProfModule {}

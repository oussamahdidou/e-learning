import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModuleInfoComponent } from './module-info.component';
import { HeaderComponent } from './header/header.component';
import { RouterModule, Routes } from '@angular/router';
import { InfoCardComponent } from './info-card/info-card.component';
import { ProgramComponent } from './program/program.component';
import { ProgramPdfComponent } from './program-pdf/program-pdf.component';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { TruncatePipe } from '../pipes/truncate.pipe';

const routes: Routes = [
  {
    path: ':id',
    component: ModuleInfoComponent,
  },
];

@NgModule({
  declarations: [
    ModuleInfoComponent,
    HeaderComponent,
    InfoCardComponent,
    ProgramComponent,
    ProgramPdfComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    PdfViewerModule,
    TruncatePipe,
  ],
})
export class ModuleInfoModule {}

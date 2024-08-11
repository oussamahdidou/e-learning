import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModuleInfoComponent } from './module-info.component';
import { HeaderComponent } from './header/header.component';
import { RouterModule, Routes } from '@angular/router';
import { InfoCardComponent } from './info-card/info-card.component';
import { ProgramComponent } from './program/program.component';

const routes: Routes = [
  {
    path: ':id',
    component: ModuleInfoComponent,
  },
];

@NgModule({
  declarations: [ModuleInfoComponent, HeaderComponent, InfoCardComponent, ProgramComponent],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class ModuleInfoModule {}

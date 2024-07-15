import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InstitutionsComponent } from './institutions/institutions.component';
import { NiveauscolairesComponent } from './niveauscolaires/niveauscolaires.component';
import { ModulesComponent } from './modules/modules.component';

@NgModule({
  declarations: [InstitutionsComponent, NiveauscolairesComponent, ModulesComponent],
  imports: [CommonModule],
})
export class InstitutionsModule {}

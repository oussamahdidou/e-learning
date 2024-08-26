
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
//import { InstitutionmComponent } from './institutionm/institutionm.component';

import { RouterModule, Routes } from '@angular/router';
import { EspaceprofService } from '../services/espaceprof.service';
import { NiveauscolairemComponent } from './niveauscolaire/niveauscolairem.component';
import { InstutitionmmComponent } from './instutitionmm/instutitionmm.component';
import { NavbarComponent } from './navbar/navbar.component'; 
import { ElementPedagogiqueComponent } from './element-pedagogique/element-pedagogique.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { ObjetsPedagogiquesComponent } from './objets-pedagogiques/objets-pedagogiques.component';

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
        path: 'ElementPedagogique/:id',
        component: ElementPedagogiqueComponent,
        title: 'ElementPedagogique',
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
    ElementPedagogiqueComponent,
    NiveauscolairemComponent,
    InstutitionmmComponent,
    NavbarComponent,
    ObjetsPedagogiquesComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    MatIconModule,
  ],
  providers: [EspaceprofService],
  exports: [InstutitionmmComponent, NiveauscolairemComponent,ElementPedagogiqueComponent,ObjetsPedagogiquesComponent],

})
export class EspaceProfModule{}

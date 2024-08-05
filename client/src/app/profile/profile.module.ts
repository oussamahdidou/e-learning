import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LearningComponent } from './learning/learning.component';
import { TruncatePipe } from '../pipes/truncate.pipe';
import { ProfileComponent } from './profile.component';
import { NavbarComponent } from './navbar/navbar.component';
import { MatIcon } from '@angular/material/icon';
import { PersonelinfoComponent } from './personelinfo/personelinfo.component';

const routes: Routes = [
  {
    path: '',
    component: ProfileComponent,
    children: [
      {
        path: 'mylearning',
        component: LearningComponent,
      },
      {
        path: 'info',
        component: PersonelinfoComponent,
      },
    ],
  },
];
@NgModule({
  declarations: [
    LearningComponent,
    ProfileComponent,
    NavbarComponent,
    PersonelinfoComponent,
  ],
  imports: [CommonModule, RouterModule.forChild(routes), TruncatePipe, MatIcon],
  exports: [NavbarComponent],
})
export class ProfileModule {}

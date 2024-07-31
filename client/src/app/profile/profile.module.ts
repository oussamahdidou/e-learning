import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LearningComponent } from './learning/learning.component';
import { TruncatePipe } from '../pipes/truncate.pipe';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'mylearning',
        component: LearningComponent,
      },
    ],
  },
];
@NgModule({
  declarations: [LearningComponent],
  imports: [CommonModule, RouterModule.forChild(routes), TruncatePipe],
})
export class ProfileModule {}

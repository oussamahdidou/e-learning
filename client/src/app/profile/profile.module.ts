import {  NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LearningComponent } from './learning/learning.component';


const routes : Routes = [
  {
    path: '',
    children: [
      {
        path: 'mylearning',
        component: LearningComponent,
      }
    ]
  }
]
@NgModule({
  declarations: [
    LearningComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class ProfileModule { }

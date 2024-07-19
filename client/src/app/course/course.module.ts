import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { QuizComponent } from './quiz/quiz.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'quiz',
        component: QuizComponent,
      },
    ],
  },
];

@NgModule({
  declarations: [QuizComponent],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class CourseModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { RouterModule, Routes } from '@angular/router';
import { QuizComponent } from './quiz/quiz.component';
import { LectureComponent } from './lecture/lecture.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { PdfViewerComponent } from './pdf-viewer/pdf-viewer.component';
import { MatIconModule } from '@angular/material/icon';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { CourseComponent } from './course.component';

const routes: Routes = [
  {
    path: '',
    component: CourseComponent,
    children: [
      {
        path:'module/:id',
        component: SidebarComponent
      },
      {
        path: 'lecture/:id',
        component: LectureComponent,
      },
      {
        path: 'quiz/:id',
        component: QuizComponent,
      },
      {
        path: 'cour/:id',
        component: PdfViewerComponent,
      },
      {
        path: 'synthese/:id',
        component: PdfViewerComponent,
      },
      {
        path: 'exam/:id',
        component: PdfViewerComponent,
      },
      {
        path: 'schema/:id',
        component: PdfViewerComponent,
      }
    ],
  },
];

@NgModule({
  declarations: [
    NavbarComponent,
    QuizComponent,
    LectureComponent,
    SidebarComponent,
    PdfViewerComponent,
    CourseComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatIconModule,
    PdfViewerModule,
  ],
})
export class CourseModule {}

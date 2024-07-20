import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of, retry, throwError } from 'rxjs';
import { environment } from '../../environments/environment';

interface Option {
  id: number;
  nom: string;
  truth: string;
}

interface Question {
  id: number;
  nom: string;
  options: Option[];
}

interface Quiz {
  id: number;
  questions: Question[];
}

interface Chapitre {
  id: number;
  ChapitreNum: number;
  nom: string;
  Statue: string;
  CoursPdfPath: string;
  VideoPath: string;
  Synthese: string;
  Schema: string;
  Premium: boolean;
  quizId: number;
  quiz: Quiz;
}

interface Controle {
  id: number;
  nom: string;
  ennonce: string;
  solution: string;
  ChapitreNum: number[];
}

interface Module {
  id: number;
  nom: string;
  chapitres: Chapitre[];
  controles?: Controle[];
}

@Injectable({
  providedIn: 'root',
})
export class CourseService {
  module: Module = {
    id: 1,
    nom: 'Module 1',
    chapitres: [
      {
        id: 1,
        ChapitreNum: 1,
        nom: 'Chapitre 1',
        Statue: 'checked',
        CoursPdfPath: '/path/to/pdf',
        VideoPath: '/path/to/video',
        Synthese: 'Summary of Chapitre 1',
        Schema: 'Schema1',
        Premium: true,
        quizId: 1,
        quiz: {
          id: 1,
          questions: [
            {
              id: 1,
              nom: 'Question 1',
              options: [
                { id: 1, nom: 'Option 1', truth: 'false' },
                { id: 2, nom: 'Option 2', truth: 'true' },
              ],
            },
          ],
        },
      },
      {
        id: 2,
        ChapitreNum: 2,
        nom: 'Chapitre 2',
        Statue: 'unchecked',
        CoursPdfPath: '/path/to/pdf',
        VideoPath: '/path/to/video',
        Synthese: 'Summary of Chapitre 2',
        Schema: 'Schema2',
        Premium: false,
        quizId: 2,
        quiz: {
          id: 2,
          questions: [
            {
              id: 2,
              nom: 'Question 2',
              options: [
                { id: 3, nom: 'Option 3', truth: 'true' },
                { id: 4, nom: 'Option 4', truth: 'false' },
              ],
            },
          ],
        },
      },
    ],
    controles: [
      {
        id: 1,
        nom: 'Controle 1',
        ennonce: 'Enonce for Controle 1',
        solution: 'Solution for Controle 1',
        ChapitreNum: [1, 2],
      },
    ],
  };
  constructor(private http: HttpClient) {}

  getCourse(): Observable<Module> {
    // return this.http
    //   .get<Module>(`${environment.apiUrl}/Account/Login`)
    //   .pipe(retry(1), catchError(this.handleError));
    return of(this.module);
  }
  // private handleError(error: HttpErrorResponse) {
  //   if (error.status === 0) {
  //     console.error('An error occurred:', error.error);
  //   } else {
  //     console.error(
  //       `Backend returned code ${error.status}, body was: `,
  //       error.error
  //     );
  //   }
  //   return throwError(
  //     () => new Error('Something bad happened; please try again later.')
  //   );
  // }
  getQuizByID(id: number): Observable<Quiz | undefined> {
    const chapter = this.module.chapitres.find(
      (chapitre) => chapitre.quiz.id === id
    );

    const quiz = chapter ? chapter.quiz : undefined;
    return of(quiz);
  }
}

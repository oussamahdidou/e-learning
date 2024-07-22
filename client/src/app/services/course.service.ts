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
  nom: string;
  questions: Question[];
}

interface Chapitre {
  id: number;
  ChapitreNum: number;
  nom: string;
  Statue: string;
  CoursPdfPath: string | null;
  VideoPath: string | null;
  Synthese: string | null;
  Schema: string | null;
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
  controles: Controle[];
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
        id: 99,
        ChapitreNum: 1,
        nom: 'Chapitre 1',
        Statue: 'checked',
        CoursPdfPath: '/cour/XMLChp1.pdf',
        VideoPath: null,
        Synthese: '/cour/XMLChp1.pdf',
        Schema: '/cour/XMLChp1.pdf',
        Premium: true,
        quizId: 1,
        quiz: {
          id: 1,
          nom: 'quiz 1',
          questions: [
            {
              id: 1,
              nom: 'Question 1',
              options: [
                { id: 1, nom: 'Option 1', truth: 'false' },
                { id: 2, nom: 'Option 2', truth: 'true' },
              ],
            },
            {
              id: 2,
              nom: 'Question 2',
              options: [
                { id: 1, nom: 'Option 1', truth: 'false' },
                { id: 2, nom: 'Option 2', truth: 'true' },
              ],
            },
          ],
        },
      },
      {
        id: 100,
        ChapitreNum: 2,
        nom: 'Chapitre 2',
        Statue: 'unchecked',
        CoursPdfPath: '/cour/cours3.pdf',
        VideoPath: '/cour/20210807_223157.mp4',
        Synthese: '/cour/cours3.pdf',
        Schema: '/cour/cours3.pdf',
        Premium: false,
        quizId: 2,
        quiz: {
          id: 2,
          nom: 'quiz 2',
          questions: [
            {
              id: 2,
              nom: 'Question 1',
              options: [
                { id: 3, nom: 'Option kkfhgkdahsfk', truth: 'true' },
                { id: 4, nom: 'Option fjakhfkahkf', truth: 'false' },
              ],
            },
            {
              id: 55,
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
        nom: 'Controle 2',
        ennonce: '/cour/XMLChp1.pdf',
        solution: 'Solution for Controle 1',
        ChapitreNum: [1, 2],
      },
      {
        id: 2,
        nom: 'Controle 1',
        ennonce: '/cour/XMLChp1.pdf',
        solution: 'Solution for Controle 1',
        ChapitreNum: [1],
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
      (chapitre) => chapitre.id === id
    );

    const quiz = chapter ? chapter.quiz : undefined;
    return of(quiz);
  }

  getVdUrlById(id: number): Observable<string | undefined> {
    const chapter = this.module.chapitres.find(
      (chapitre) => chapitre.id === id
    );

    const vdUrl = chapter ? chapter.VideoPath : undefined;
    return of(vdUrl as string | undefined);
  }

  getCourPdfUrlById(id: number): Observable<string | undefined> {
    const chapter = this.module.chapitres.find(
      (chapitre) => chapitre.id === id
    );

    const pdfUrl = chapter ? chapter.CoursPdfPath : undefined;
    return of(pdfUrl as string | undefined);
  }

  getSyntheseById(id: number): Observable<string | undefined>{
    const chapter = this.module.chapitres.find(
      (chapitre) => chapitre.id === id
    );
    const syntheseUrl = chapter ? chapter.Synthese : undefined;
    return of(syntheseUrl as string | undefined);
  }

  getControleById(id: number): Observable<string | undefined>{
    const controle = this.module.controles.find(
      (controle) => controle.id === id
    );
    const controleUrl = controle ? controle.ennonce : undefined;
    return of(controleUrl as string | undefined);
  }
  getSchemaById(id: number): Observable<string | undefined>{
    const chapter = this.module.chapitres.find(
      (chapitre) => chapitre.id === id
    );
    const schemaeUrl = chapter ? chapter.Schema : undefined;
    return of(schemaeUrl as string | undefined);
  }
}

import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of, retry, tap, throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';

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
  chapitreNum: number;
  nom: string;
  statue: boolean;
  coursPdfPath: string | null;
  videoPath: string | null;
  synthese: string | null;
  schema: string | null;
  premium: boolean;
  quizId: number;
  quiz: Quiz;
}

interface Controle {
  id: number;
  nom: string;
  ennonce: string;
  solution: string;
  chapitreNum: number[];
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
  private module: Module = {
    id: 1,
    nom: 'Module 1',
    chapitres: [
      {
        id: 99,
        chapitreNum: 1,
        nom: 'Chapitre 1',
        statue: true,
        coursPdfPath: '/cour/XMLChp1.pdf',
        videoPath: '/cour/20210807_223157.mp4',
        synthese: '/cour/XMLChp1.pdf',
        schema: '/cour/XMLChp1.pdf',
        premium: true,
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
        chapitreNum: 2,
        nom: 'Chapitre 2',
        statue: false,
        coursPdfPath: '/cour/cours3.pdf',
        videoPath: '/cour/20210807_223157.mp4',
        synthese: '/cour/cours3.pdf',
        schema: '/cour/cours3.pdf',
        premium: false,
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
        chapitreNum: [1, 2],
      },
      {
        id: 2,
        nom: 'Controle 1',
        ennonce: '/cour/XMLChp1.pdf',
        solution: 'Solution for Controle 1',
        chapitreNum: [1],
      },
    ],
  };

  private currentChapterIndex: number = 0;
  private currentItemIndex: number = 0;
  private itemsOrder: (keyof Chapitre)[] = [
    'coursPdfPath',
    'videoPath',
    'schema',
    'synthese',
    'quiz',
  ];

  constructor(private http: HttpClient, private router: Router) {}

  getCourseById(id: number): Observable<Module | undefined> {
    console.log('Current Module:', this.module);
    console.log('Requested ID:', id);
    console.log('Module ID:', this.module?.id);
    console.log('ID Match:', this.module?.id === id);
    const token = localStorage.getItem('token');

    // if (this.module?.id === id) {
    //   return of(this.module);
    // }
    return this.http
      .get<Module>(`${environment.apiUrl}/api/module/${id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .pipe(
        map((data) => {
          this.module = data;
          return data;
        }),
        catchError(this.handleError)
      );
  }
  private handleError(error: HttpErrorResponse) {
    console.error('An error occurred:', error);
    return throwError('Something went wrong; please try again later.');
  }

  checkChapter(id: number): Observable<boolean> {
    return this.http
      .get<any>(`${environment.apiUrl}/api/checkChapter/${id}`)
      .pipe(
        map((data) => {
          this.module = data;
          return data;
        }),
        catchError(this.handleError)
      );
  }

  getQuizByID(id: number): Observable<Quiz | undefined> {
    const quiz = this.module.chapitres
      .map((chapitre) => chapitre.quiz)
      .find((quiz) => quiz.id === id);

    return of(quiz);
  }

  getVdUrlById(id: number): Observable<string | undefined> {
    const chapter = this.module.chapitres.find(
      (chapitre) => chapitre.id === id
    );

    const vdUrl = chapter ? chapter.videoPath : undefined;
    return of(vdUrl as string | undefined);
  }

  getCourPdfUrlById(id: number): Observable<string | undefined> {
    const chapter = this.module.chapitres.find(
      (chapitre) => chapitre.id === id
    );

    const pdfUrl = chapter ? chapter.coursPdfPath : undefined;
    return of(pdfUrl as string | undefined);
  }

  getSyntheseById(id: number): Observable<string | undefined> {
    const chapter = this.module.chapitres.find(
      (chapitre) => chapitre.id === id
    );
    const syntheseUrl = chapter ? chapter.synthese : undefined;
    return of(syntheseUrl as string | undefined);
  }

  getControleById(id: number): Observable<Controle | undefined> {
    const controle = this.module.controles.find(
      (controle) => controle.id === id
    );
    if (controle == null) return of(undefined);
    return of(controle);
  }

  getSchemaById(id: number): Observable<string | undefined> {
    const chapter = this.module.chapitres.find(
      (chapitre) => chapitre.id === id
    );
    const schemaUrl = chapter ? chapter.schema : undefined;
    return of(schemaUrl as string | undefined);
  }

  isVdUrlAvailable(id: number): Observable<boolean> {
    const chapter = this.module.chapitres.find((ch) => ch.id === id);
    const vdUrlExists = chapter ? !!chapter.videoPath : false;
    return of(vdUrlExists);
  }
  getChapterNumber(id: number): Observable<number | null> {
    const chapter = this.module.chapitres.find((ch) => ch.id === id);
    const chapterNum = chapter ? chapter.chapitreNum : null;
    return of(chapterNum);
  }
  getControle(id: number): Observable<boolean> {
    const controle = this.module.controles.find(
      (controle) => Math.max(...controle.chapitreNum) === id
    );
    if (controle) {
      return of(true);
    }
    return of(false);
  }
  getFirstChapterId(id: number): Observable<boolean> {
    const chapterId = this.module.chapitres[0].id;
    if (chapterId === id) return of(true);
    return of(false);
  }

  isLastChapter(id: number): Observable<boolean> {
    const maxchapitreNum = Math.max(
      ...this.module.chapitres.map((chapitre) => chapitre.chapitreNum)
    );
    const chapter = this.module.chapitres.find((chapter) => chapter.id === id);
    if (chapter?.chapitreNum === maxchapitreNum) return of(true);
    return of(false);
  }

  createQuizResult(quizId: number, note: number): Observable<any> {
    const result = { quizId, note };
    console.log('Object being sent to backend:', result);

    return this.http
      .post<any>(`${environment.apiUrl}/api/QuizResult/Create`, result)
      .pipe(
        tap((response) => {
          console.log('Response from backend:', response);
        }),
        catchError(this.handleError)
      );
  }
  uploadSolution(formData: FormData, id: number): Observable<any> {
    const token = localStorage.getItem('token');
    return this.http
      .post(`${environment.apiUrl}/api/ResultControle/${id}`, formData, {
        responseType: 'text',
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .pipe(
        tap((response) => {
          console.log('Response from backend:', response);
        }),
        catchError(this.handleError)
      );
  }
  isDevoirUploaded(id: number): Observable<any> {
    const token = localStorage.getItem('token');
    return this.http
      .get(`${environment.apiUrl}/api/ResultControle/${id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .pipe(
        tap((response) => {
          return response;
        }),
        catchError(this.handleError)
      );
  }
  deleteDevoir(id: number): Observable<any> {
    const token = localStorage.getItem('token');
    return this.http
      .delete(`${environment.apiUrl}/api/ResultControle/${id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .pipe(
        tap((response) => {
          return true;
        }),
        catchError(this.handleError)
      );
  }
}

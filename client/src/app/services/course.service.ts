import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of, retry, throwError } from 'rxjs';
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
  private module: Module = {
    id: 1,
    nom: 'Module 1',
    chapitres: [
      {
        id: 99,
        ChapitreNum: 1,
        nom: 'Chapitre 1',
        Statue: 'checked',
        CoursPdfPath: '/cour/XMLChp1.pdf',
        VideoPath: '/cour/20210807_223157.mp4',
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

  private currentChapterIndex: number = 0;
  private currentItemIndex: number = 0;
  private itemsOrder: (keyof Chapitre)[] = [
    'CoursPdfPath',
    'VideoPath',
    'Schema',
    'Synthese',
    'quiz',
  ];

  constructor(private http: HttpClient, private router: Router) {}

  getCourseById(id: number): Observable<Module | undefined> {
    console.log('Current Module:', this.module);
    console.log('Requested ID:', id);
    console.log('Module ID:', this.module?.id);
    console.log('ID Match:', this.module?.id === id);

    if (this.module?.id === id) {
      return of(this.module);
    }

    return of(undefined);
    // return this.http.get<Module>(`${environment.apiUrl}/modules/${id}`).pipe(
    //   map((data) => {
    //     this.module = data;
    //     return data;
    //   }),
    //   catchError(this.handleError)
    // );
  }
  private handleError(error: HttpErrorResponse) {
    console.error('An error occurred:', error);
    return throwError('Something went wrong; please try again later.');
  }
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

  getSyntheseById(id: number): Observable<string | undefined> {
    const chapter = this.module.chapitres.find(
      (chapitre) => chapitre.id === id
    );
    const syntheseUrl = chapter ? chapter.Synthese : undefined;
    return of(syntheseUrl as string | undefined);
  }

  getControleById(id: number): Observable<string | undefined> {
    const controle = this.module.controles.find(
      (controle) => Math.max(...controle.ChapitreNum) === id
    );
    const controleUrl = controle?.ennonce;
    return of(controleUrl);
  }

  getSchemaById(id: number): Observable<string | undefined> {
    const chapter = this.module.chapitres.find(
      (chapitre) => chapitre.id === id
    );
    const schemaUrl = chapter ? chapter.Schema : undefined;
    return of(schemaUrl as string | undefined);
  }

  isVdUrlAvailable(id: number): Observable<boolean> {
    const chapter = this.module.chapitres.find((ch) => ch.id === id);
    const vdUrlExists = chapter ? !!chapter.VideoPath : false;
    return of(vdUrlExists);
  }
  getChapterNumber(id: number): Observable<number | null> {
    const chapter = this.module.chapitres.find((ch) => ch.id === id);
    const chapterNum = chapter ? chapter.ChapitreNum : null;
    return of(chapterNum);
  }
  getControle(id: number): Observable<boolean> {
    const controle = this.module.controles.find(
      (controle) => Math.max(...controle.ChapitreNum) === id
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
    const maxChapitreNum = Math.max(
      ...this.module.chapitres.map((chapitre) => chapitre.ChapitreNum)
    );
    const chapter = this.module.chapitres.find((chapter) => chapter.id === id);
    if (chapter?.ChapitreNum === maxChapitreNum) return of(true);
    return of(false);
  }
}

export interface Option {
  id: number;
  nom: string;
  truth: boolean;
}

export interface Question {
  id: number;
  nom: string;
  options: Option[];
}

export interface Quiz {
  id: number;
  nom: string;
  questions: Question[];
}

export interface Chapitre {
  id: number;
  chapitreNum: number;
  nom: string;
  statue: boolean;
  coursPdfPath: string | null;
  studentCoursParagraphes: any[] | null;
  videoPath: string | null;
  videos: any | null;
  synthese: string | null;
  syntheses: any | null;
  schema: string | null;
  schemas: any | null;
  premium: boolean;
  quizId: number;
  quiz: Quiz;
}

export interface Controle {
  id: number;
  nom: string;
  ennonce: string;
  solution: string;
  chapitreNum: number[];
}

export interface Module {
  id: number;
  nom: string;
  chapitres: Chapitre[];
  controles: Controle[];
}
export interface RequiredModule {
  Id: number;
  Name: string;
  NiveauScolaire: string;
  Institution: string;
  Seuill: number;
}
export interface moduleInfo {
  moduleID: number;
  nom: string;
  numberOfChapter: number;
  moduleImg: string;
  moduleDescription: string;
  moduleProgram: string;
  checkCount: number;
}
export interface IsEligible {
  isEligible: boolean;
  modules: RequiredModule[];
}
export interface NomDescription {
  nom: string;
  moduleDescription: string;
}
export interface InfoCard {
  Id: number;
  nom: string;
  numberOfChapter: number;
  moduleImg: string;
}
export interface CheckChapterRequest {
  Id: number;
  ControleId: number;
  ExamId: number;
  lastChapter: boolean;
  lastChapterExam: boolean;
  avis: string;
}

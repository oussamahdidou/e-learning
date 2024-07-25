export interface Option {
  id: number;
  nom: string;
  truth: boolean;
  questionId: number;
}
export interface Question {
  id: number;
  nom: string;
  quizId: number;
  options: Option[];
}

export interface Quiz {
  id: number;
  nom: string;
  statue: string;
  questions: Question[];
}

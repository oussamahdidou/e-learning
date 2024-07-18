export interface Option {
  name: string;
  isCorrect: boolean;
}

export interface Question {
  name: string;
  options: Option[];
}

export interface Quiz {
  name: string;
  questions: Question[];
}

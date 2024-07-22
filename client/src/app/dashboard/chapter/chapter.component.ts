import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';
import { Quiz } from '../../interfaces/dashboard';

@Component({
  selector: 'app-chapter',
  templateUrl: './chapter.component.html',
  styleUrl: './chapter.component.css',
})
export class ChapterComponent implements AfterViewInit, OnInit {
  pdfSrc!: string;

  constructor(private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    // Initialize the PDF path here
    this.pdfSrc = '/documents/download_3.pdf';
  }

  ngAfterViewInit(): void {
    // Manually trigger change detection if needed
    this.cdr.detectChanges();
  }
  isLinear = false;
  quiz: Quiz = {
    id: 1,
    nom: 'General Knowledge Quiz',
    statue: 'pending',
    questions: [
      {
        id: 1,
        nom: 'What is the capital of France?',
        quizId: 1,
        options: [
          { id: 1, nom: 'Berlin', truth: false, questionId: 1 },
          { id: 2, nom: 'Madrid', truth: false, questionId: 1 },
          { id: 3, nom: 'Paris', truth: true, questionId: 1 },
        ],
      },
      {
        id: 2,
        nom: 'Which planet is known as the Red Planet?',
        quizId: 1,
        options: [
          { id: 4, nom: 'Earth', truth: false, questionId: 2 },
          { id: 5, nom: 'Mars', truth: true, questionId: 2 },
          { id: 6, nom: 'Jupiter', truth: false, questionId: 2 },
        ],
      },
      {
        id: 3,
        nom: "Who wrote 'To Kill a Mockingbird'?",
        quizId: 1,
        options: [
          { id: 7, nom: 'Harper Lee', truth: true, questionId: 3 },
          { id: 8, nom: 'Mark Twain', truth: false, questionId: 3 },
          { id: 9, nom: 'Jane Austen', truth: false, questionId: 3 },
        ],
      },
      {
        id: 4,
        nom: 'What is the smallest prime number?',
        quizId: 1,
        options: [
          { id: 10, nom: '1', truth: false, questionId: 4 },
          { id: 11, nom: '2', truth: true, questionId: 4 },
          { id: 12, nom: '3', truth: false, questionId: 4 },
        ],
      },
      {
        id: 5,
        nom: 'Who painted the Mona Lisa?',
        quizId: 1,
        options: [
          { id: 13, nom: 'Vincent van Gogh', truth: false, questionId: 5 },
          { id: 14, nom: 'Leonardo da Vinci', truth: true, questionId: 5 },
          { id: 15, nom: 'Pablo Picasso', truth: false, questionId: 5 },
        ],
      },
      {
        id: 6,
        nom: 'What is the boiling point of water?',
        quizId: 1,
        options: [
          { id: 16, nom: '90°C', truth: false, questionId: 6 },
          { id: 17, nom: '100°C', truth: true, questionId: 6 },
          { id: 18, nom: '110°C', truth: false, questionId: 6 },
        ],
      },
      {
        id: 7,
        nom: 'Which element has the chemical symbol O?',
        quizId: 1,
        options: [
          { id: 19, nom: 'Oxygen', truth: true, questionId: 7 },
          { id: 20, nom: 'Osmium', truth: false, questionId: 7 },
          { id: 21, nom: 'Gold', truth: false, questionId: 7 },
        ],
      },
      {
        id: 8,
        nom: 'Who is known as the father of computers?',
        quizId: 1,
        options: [
          { id: 22, nom: 'Charles Babbage', truth: true, questionId: 8 },
          { id: 23, nom: 'Alan Turing', truth: false, questionId: 8 },
          { id: 24, nom: 'John von Neumann', truth: false, questionId: 8 },
        ],
      },
      {
        id: 9,
        nom: 'What is the hardest natural substance on Earth?',
        quizId: 1,
        options: [
          { id: 25, nom: 'Iron', truth: false, questionId: 9 },
          { id: 26, nom: 'Diamond', truth: true, questionId: 9 },
          { id: 27, nom: 'Gold', truth: false, questionId: 9 },
        ],
      },
      {
        id: 10,
        nom: 'Which country is the largest by area?',
        quizId: 1,
        options: [
          { id: 28, nom: 'Canada', truth: false, questionId: 10 },
          { id: 29, nom: 'Russia', truth: true, questionId: 10 },
          { id: 30, nom: 'China', truth: false, questionId: 10 },
        ],
      },
    ],
  };

  addQuestion() {
    this.quiz.questions.push({
      id: 0,
      nom: '',
      quizId: this.quiz.id,
      options: [{ id: 0, nom: '', truth: false, questionId: 0 }],
    });
  }

  deleteQuestion(i: number) {
    if (this.quiz.questions.length > 1) {
      if (i >= 0 && i < this.quiz.questions.length) {
        this.quiz.questions.splice(i, 1);
      }
    }
  }

  addOption(questionIndex: number) {
    const question = this.quiz.questions[questionIndex];
    question.options.push({
      id: 0,
      nom: '',
      truth: false,
      questionId: question.id,
    });
  }

  deleteOption(questionIndex: number, optionIndex: number) {
    // Check if the option array has more than one item
    if (this.quiz.questions[questionIndex].options.length > 1) {
      // Remove the specific option using its index
      this.quiz.questions[questionIndex].options.splice(optionIndex, 1);
    } else {
      console.error('Cannot delete the last option.');
    }
  }

  setCorrectOption(questionIndex: number, optionIndex: number) {
    this.quiz.questions[questionIndex].options.forEach((option, index) => {
      option.truth = index === optionIndex;
    });
  }

  onSubmit() {
    console.log('Quiz:', this.quiz);
  }
}

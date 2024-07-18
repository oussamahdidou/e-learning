import { Component } from '@angular/core';
import { Quiz } from '../../interfaces/dashboard';

@Component({
  selector: 'app-updatequiz',
  templateUrl: './updatequiz.component.html',
  styleUrl: './updatequiz.component.css',
})
export class UpdatequizComponent {
  quiz: Quiz = {
    name: 'General Knowledge Quiz',
    questions: [
      {
        name: 'What is the capital of France?',
        options: [
          { name: 'Berlin', isCorrect: false },
          { name: 'Madrid', isCorrect: false },
          { name: 'Paris', isCorrect: true },
        ],
      },
      {
        name: 'Which planet is known as the Red Planet?',
        options: [
          { name: 'Earth', isCorrect: false },
          { name: 'Mars', isCorrect: true },
          { name: 'Jupiter', isCorrect: false },
        ],
      },
      {
        name: "Who wrote 'To Kill a Mockingbird'?",
        options: [
          { name: 'Harper Lee', isCorrect: true },
          { name: 'Mark Twain', isCorrect: false },
          { name: 'Jane Austen', isCorrect: false },
        ],
      },
      {
        name: 'What is the smallest prime number?',
        options: [
          { name: '1', isCorrect: false },
          { name: '2', isCorrect: true },
          { name: '3', isCorrect: false },
        ],
      },
      {
        name: 'Who painted the Mona Lisa?',
        options: [
          { name: 'Vincent van Gogh', isCorrect: false },
          { name: 'Leonardo da Vinci', isCorrect: true },
          { name: 'Pablo Picasso', isCorrect: false },
        ],
      },
      {
        name: 'What is the boiling point of water?',
        options: [
          { name: '90°C', isCorrect: false },
          { name: '100°C', isCorrect: true },
          { name: '110°C', isCorrect: false },
        ],
      },
      {
        name: 'Which element has the chemical symbol O?',
        options: [
          { name: 'Oxygen', isCorrect: true },
          { name: 'Osmium', isCorrect: false },
          { name: 'Gold', isCorrect: false },
        ],
      },
      {
        name: 'Who is known as the father of computers?',
        options: [
          { name: 'Charles Babbage', isCorrect: true },
          { name: 'Alan Turing', isCorrect: false },
          { name: 'John von Neumann', isCorrect: false },
        ],
      },
      {
        name: 'What is the hardest natural substance on Earth?',
        options: [
          { name: 'Iron', isCorrect: false },
          { name: 'Diamond', isCorrect: true },
          { name: 'Gold', isCorrect: false },
        ],
      },
      {
        name: 'Which country is the largest by area?',
        options: [
          { name: 'Canada', isCorrect: false },
          { name: 'Russia', isCorrect: true },
          { name: 'China', isCorrect: false },
        ],
      },
    ],
  };

  addQuestion() {
    this.quiz.questions.push({
      name: '',
      options: [{ name: '', isCorrect: false }],
    });
  }

  deleteQuestion() {
    if (this.quiz.questions.length > 1) {
      this.quiz.questions.pop();
    }
  }

  addOption(questionIndex: number) {
    this.quiz.questions[questionIndex].options.push({
      name: '',
      isCorrect: false,
    });
  }

  deleteOption(questionIndex: number) {
    if (this.quiz.questions[questionIndex].options.length > 1) {
      this.quiz.questions[questionIndex].options.pop();
    }
  }

  setCorrectOption(questionIndex: number, optionIndex: number) {
    this.quiz.questions[questionIndex].options.forEach((option, index) => {
      option.isCorrect = index === optionIndex;
    });
  }

  onSubmit() {
    console.log('Quiz:', this.quiz);
  }
}

import { Component } from '@angular/core';
import { Quiz } from '../../interfaces/dashboard';

@Component({
  selector: 'app-createquiz',
  templateUrl: './createquiz.component.html',
  styleUrl: './createquiz.component.css',
})
export class CreatequizComponent {
  quiz: Quiz = {
    name: '',
    questions: [{ name: '', options: [{ name: '', isCorrect: false }] }],
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

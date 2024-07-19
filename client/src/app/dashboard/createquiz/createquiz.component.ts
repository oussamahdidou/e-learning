import { Component } from '@angular/core';
import { Quiz } from '../../interfaces/dashboard';

@Component({
  selector: 'app-createquiz',
  templateUrl: './createquiz.component.html',
  styleUrl: './createquiz.component.css',
})
export class CreatequizComponent {
  quiz: Quiz = {
    id: 0,
    nom: '',
    statue: 'Draft',
    questions: [],
  };

  addQuestion() {
    const newQuestionId = this.quiz.questions.length + 1;
    this.quiz.questions.push({
      id: 0,
      nom: '',
      quizId: this.quiz.id,
      options: [{ id: 0, nom: '', truth: false, questionId: 0 }],
    });
  }

  deleteQuestion() {
    if (this.quiz.questions.length > 0) {
      this.quiz.questions.pop();
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

  deleteOption(questionIndex: number) {
    if (this.quiz.questions[questionIndex].options.length > 1) {
      this.quiz.questions[questionIndex].options.pop();
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

import { Component } from '@angular/core';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrl: './quiz.component.css'
})


export class QuizComponent {
  quiz = {
    id: 11,
    nom: 'Sample Quiz',
    statue: 'Active',
    questions: [
      {
        id: 59,
        nom: 'What is the largest planet in our solar system?',
        options: [
          { id: 156, nom: 'Jupiter', truth: true },
          { id: 157, nom: 'Saturn', truth: false },
          { id: 158, nom: 'Mars', truth: false }
        ]
      },
      {
        id: 58,
        nom: 'Who painted the Mona Lisa?',
        options: [
          { id: 153, nom: 'Leonardo da Vinci', truth: true },
          { id: 154, nom: 'Pablo Picasso', truth: false },
          { id: 155, nom: 'Vincent van Gogh', truth: false }
        ]
      },
      {
        id: 57,
        nom: 'What is the capital of France?',
        options: [
          { id: 150, nom: 'Paris', truth: true },
          { id: 151, nom: 'London', truth: false },
          { id: 152, nom: 'Berlin', truth: false }
        ]
      }
    ]
  };

  currentQuestionIndex = 0;
  selectedAnswers: { [questionId: number]: number } = {};
  errorMessage: string | null = null;

  get currentQuestion() {
    return this.quiz.questions[this.currentQuestionIndex];
  }

  selectAnswer(optionId: number) {
    this.selectedAnswers[this.currentQuestion.id] = optionId;
  }

  nextQuestion() {
    if (this.currentQuestionIndex < this.quiz.questions.length - 1) {
      this.currentQuestionIndex++;
    }
  }

  previousQuestion() {
    if (this.currentQuestionIndex > 0) {
      this.currentQuestionIndex--;
    }
  }

  finishQuiz() {
    // Check if all questions have been answered
    const unansweredQuestions = this.quiz.questions.filter(
      (q) => !this.selectedAnswers[q.id]
    );

    if (unansweredQuestions.length > 0) {
      this.errorMessage = 'Please answer all questions before finishing the quiz.';
    } else {
      this.errorMessage = null;
      // Handle quiz submission logic here, e.g., send answers to a server
      console.log('User answers:', this.selectedAnswers);
    }
  }

}

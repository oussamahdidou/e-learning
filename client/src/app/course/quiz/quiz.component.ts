import { Component, OnInit } from '@angular/core';
import { CourseService } from '../../services/course.service';
import { ActivatedRoute } from '@angular/router';

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
  nom : string;
  questions: Question[];
}

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css']
})
export class QuizComponent implements OnInit {

  quiz: Quiz = {
    id: 0,
    nom: '',
    questions: []
  };
  currentQuestionIndex = 0;
  selectedAnswers: { [questionId: number]: number } = {};
  errorMessage: string | null = null;

  constructor(
    private courseService: CourseService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = Number(params.get('id'));
      this.courseService.getQuizByID(id).subscribe(
        (quiz: Quiz | undefined) => {
          if (quiz) {
            this.quiz = quiz;
          } else {
            console.error('Quiz not found');
            // Handle the case where the quiz is not found, e.g., display an error message
          }
        },
        (error) => {
          console.error('Error fetching quiz:', error);
          // Handle the error, e.g., display an error message
        }
      );
    });
  }

  get currentQuestion(): Question {
    return this.quiz.questions[this.currentQuestionIndex] || { id: 0, nom: '', options: [] };
  }

  selectAnswer(optionId: number) {
    if (this.currentQuestion) {
      this.selectedAnswers[this.currentQuestion.id] = optionId;
    }
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
    if (this.quiz) {
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
}


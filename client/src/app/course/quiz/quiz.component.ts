import { Component, OnInit } from '@angular/core';
import { CourseService } from '../../services/course.service';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { Question, Quiz } from '../../interfaces/dashboard';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css'],
})
export class QuizComponent implements OnInit {
  quiz: Quiz = {
    id: 0,
    nom: '',
    questions: [],
  };
  currentQuestionIndex = 0;
  selectedAnswers: { [questionId: number]: number } = {};
  errorMessage: string | null = null;
  isQuizAlreadyPassed: boolean = false;
  note: number = 0;
  isTest: boolean = false;

  constructor(
    private courseService: CourseService,
    private route: ActivatedRoute,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const pathSegment = this.route.snapshot.url[0]?.path;

      if (pathSegment === 'quiz') {
        const quizId = Number(params.get('quizid'));
        if (!isNaN(quizId)) {
          this.loadQuiz(quizId);
        } else {
          console.error('Invalid quiz ID:', quizId);
          this.router.navigate(['/']);
        }
      } else if (pathSegment === 'testniveau') {
        const testNiveauId = Number(params.get('testniveauid'));
        if (!isNaN(testNiveauId)) {
          this.loadTestNiveau(testNiveauId);
        } else {
          console.error('Invalid Test Niveau ID:', testNiveauId);
          this.router.navigate(['/']);
        }
      } else {
        console.error('Invalid route: No valid quiz or testniveau path segment.');
        this.router.navigate(['/']);
      }
    });
  }

  private loadQuiz(quizId: number): void {
    this.courseService.getQuizByID(quizId).subscribe(
      (quiz: Quiz | undefined) => {
        if (quiz) {
          this.quiz = quiz;
          this.getQuizResult(quizId, this.quiz.questions.length);
          console.log('Quiz loaded:', quiz);
        } else {
          console.error('Quiz not found', quizId);
          this.router.navigate(['/']);
        }
      },
      (error) => {
        console.error('Error fetching quiz:', error);
        this.router.navigate(['/']);
      }
    );
  }

  private loadTestNiveau(testNiveauId: number): void {
    this.courseService.getTestNiveau(testNiveauId).subscribe(
      (quiz: Quiz | undefined) => {
        if (quiz) {
          this.quiz = quiz;
          this.quiz.id = testNiveauId;
          this.isTest = true;
          this.getTestNiveauScore(testNiveauId, this.quiz.questions.length);
          console.log('Test Niveau loaded:', quiz);
        } else {
          console.error('Test Niveau not found', testNiveauId);
          this.router.navigate(['/']);
        }
      },
      (error) => {
        console.error('Error fetching Test Niveau:', error);
        this.router.navigate(['/']);
      }
    );
  }


  get currentQuestion(): Question {
    return (
      this.quiz.questions[this.currentQuestionIndex] || {
        id: 0,
        nom: '',
        options: [],
      }
    );
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
        this.errorMessage =
          'Please answer all questions before finishing the quiz.';
      } else {
        this.errorMessage = null;
        console.log('User answers:', this.selectedAnswers);
        let note: number = 0;
        this.quiz.questions.forEach((question) => {
          const selectedOptionId = this.selectedAnswers[question.id];
          const correctOption = question.options.find(
            (option) => option.truth === true
          );

          if (correctOption && correctOption.id === selectedOptionId) {
            note++;
          }
        });

        const resultMessage = `Votre note est :${note} / ${this.quiz.questions.length}`;
        if (this.isTest) {
          this.courseService
            .createTestNiveauScore(this.quiz.id, note)
            .subscribe((res) => {
              this.showResultMessage(resultMessage, res.note);
            });
        } else {
          if (this.isQuizAlreadyPassed) {
            this.courseService
              .updateQuizResult(this.quiz.id, note)
              .subscribe((res) => {
                this.showResultMessage(resultMessage, res.note);
              });
          } else {
            this.courseService
              .createQuizResult(this.quiz.id, note)
              .subscribe((state) => {
                this.showResultMessage(resultMessage, note);
              });
            console.log('your Note:', note);
          }
        }
      }
    }
  }

  private showResultMessage(title: string, note: number) {
    Swal.fire({
      title,
      text: `Vous avez deja passé ce quiz si vous voulez passé ce quiz une autre fois clicker sur ok`,
      icon: 'success',
    });
  }


  getQuizResult(id: number, noteTotal: number) {
    this.courseService.getQuizResultById(id).subscribe((res) => {
      Swal.fire({
        title: `Votre note est :${res.note} / ${noteTotal}`,
        text: `Vous avez deja passé ce quiz si vous voulez passé ce quiz une autre fois clicker sur ok`,
        icon: 'success',
      });
      this.note = res.note;
      this.isQuizAlreadyPassed = true;
      return this.note;
    });
  }

  getTestNiveauScore(moduleId : number, noteTotal: number){
    this.courseService.getTestNiveauScore(moduleId).subscribe(
      (res) => {
        if(res != 0){
          Swal.fire({
            title: `Votre note est :${res} / ${noteTotal}`,
            text: `Vous avez deja passé ce Test de niveau si vous voulez passé ce quiz une autre fois clicker sur ok`,
            icon: 'success',
          });
          this.note = res.note;
          this.isQuizAlreadyPassed = true;
          return this.note;
        }
        return
      }
    )
  }
}

import { Component, OnInit } from '@angular/core';
import { CourseService } from '../../services/course.service';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { Question, Quiz } from '../../interfaces/dashboard';
import { ErrorHandlingService } from '../../services/error-handling.service';
import { SharedDataService } from '../../services/shared-data.service';

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
  id: number = 0;
  courseId: number = 0;

  constructor(
    private courseService: CourseService,
    private route: ActivatedRoute,
    private router: Router,
    private errorHandlingService: ErrorHandlingService,
    private shared: SharedDataService
  ) {}

  ngOnInit(): void {
    this.shared.setActiveDiv('about');
    this.route.parent?.params.subscribe((params) => {
      this.courseId = params['id'];
    });
    this.route.paramMap.subscribe(
      (params) => {
        const pathSegment = this.route.snapshot.url[0]?.path;
        if (pathSegment === 'quiz') {
          const quizId = Number(params.get('quizid'));
          this.id = quizId;
          if (!isNaN(quizId)) {
            this.loadQuiz(quizId);
            this.shared.setActiveDiv(`quiz/${this.id}`);
          } else {
            this.errorHandlingService.handleError(null, 'Invalid quiz ID');
            this.router.navigate(['/']);
          }
        } else if (pathSegment === 'testniveau') {
          const testNiveauId = Number(params.get('testniveauid'));
          if (!isNaN(testNiveauId)) {
            this.loadTestNiveau(testNiveauId);
          } else {
            this.errorHandlingService.handleError(
              null,
              'Invalid Test Niveau ID'
            );
            this.router.navigate(['/']);
          }
        } else {
          this.errorHandlingService.handleError(
            null,
            'Invalid route: No valid quiz or testniveau path segment.'
          );
          this.router.navigate(['/']);
        }
      },
      (error) => {
        this.errorHandlingService.handleError(
          error,
          'Error during route parameter extraction'
        );
        this.router.navigate(['/']);
      }
    );
  }

  private loadQuiz(quizId: number): void {
    this.courseService.getQuizByID(quizId).subscribe(
      (quiz: Quiz | undefined) => {
        if (quiz) {
          this.quiz = quiz;
          this.getQuizResult(quizId, this.quiz.questions.length);
          console.log('Quiz loaded:', quiz);
        } else {
          this.errorHandlingService.handleError(null, 'Quiz Not found');
          this.router.navigate(['/']);
        }
      },
      (error) => {
        this.errorHandlingService.handleError(error, 'Error fetching quiz');
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
          this.errorHandlingService.handleError(null, 'Test Niveau not found');
          this.router.navigate(['/']);
        }
      },
      (error) => {
        this.errorHandlingService.handleError(
          error,
          'Error fetching Test Niveau'
        );
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
            .subscribe(
              (res) => {
                this.showResultMessage(resultMessage, res.note);
              },
              (error) => {
                this.errorHandlingService.handleError(
                  error,
                  'An error occured while saving mark'
                );
              }
            );
        } else {
          if (this.isQuizAlreadyPassed) {
            this.courseService.updateQuizResult(this.quiz.id, note).subscribe(
              (res) => {
                this.showResultMessage(resultMessage, res.note);
              },
              (error) => {
                this.errorHandlingService.handleError(
                  error,
                  'An error occured while updating'
                );
              }
            );
          } else {
            this.courseService.createQuizResult(this.quiz.id, note).subscribe(
              (state) => {
                this.showResultMessage(resultMessage, note);
              },
              (error) => {
                this.errorHandlingService.handleError(
                  error,
                  'An error occured while creating'
                );
              }
            );
            console.log('your Note:', note);
          }
        }
      }
    }
  }

  private showResultMessage(title: string, note: number) {
    Swal.fire({
      title,
      text: `Vous avez deja passé ce quiz si vous voulez passé ce quiz une autre fois cliquer sur ok`,
      icon: 'success',
      preConfirm: () => {
        this.router.navigate([`/course/${this.courseId}/quiz/${this.id}`]);
      },
      footer:
        '<button id="suivantButton" class="swal2-confirm swal2-styled">Suivant</button>',
      didOpen: () => {
        const suivantButton = document.getElementById('suivantButton');

        if (suivantButton) {
          suivantButton.addEventListener('click', () => {
            this.suivantFunction();
            Swal.close();
          });
        }
      },
    });
  }

  getQuizResult(id: number, noteTotal: number) {
    this.courseService.getQuizResultById(id).subscribe((res) => {
      Swal.fire({
        title: `Votre note est :${res.note} / ${noteTotal}`,
        text: `Vous avez deja passé ce quiz si vous voulez passé ce quiz une autre fois cliquer sur ok`,
        icon: 'success',
      });
      this.note = res.note;
      this.isQuizAlreadyPassed = true;
      return this.note;
    });
  }

  getTestNiveauScore(moduleId: number, noteTotal: number) {
    this.courseService.getTestNiveauScore(moduleId).subscribe(
      (res) => {
        if (res != 0) {
          Swal.fire({
            title: `Votre note est :${res} / ${noteTotal}`,
            text: `Vous avez deja passé ce Test de niveau si vous voulez passé ce quiz une autre fois cliquer sur ok`,
            icon: 'success',
          });
          this.note = res.note;
          this.isQuizAlreadyPassed = true;
          return this.note;
        }
        return;
      },
      (error) => {
        this.errorHandlingService.handleError(
          error,
          'Error During Fetching TestNiveau'
        );
      }
    );
  }
  suivantFunction() {
    this.courseService.getChapterId(this.id).subscribe(
      (chapterId) => {
        if (chapterId != null) {
          this.courseService.getControle(chapterId).subscribe((controleId) => {
            if (controleId != null) {
              this.router.navigate([
                `/course/${this.courseId}/exam/${controleId}`,
              ]);
            } else {
              this.courseService.isLastChapter(chapterId).subscribe((state) => {
                if (state) {
                  console.log('this is the last chapter');
                } else {
                  this.router.navigate([
                    `/course/${this.courseId}/cour/${this.id + 1}`,
                  ]);
                }
              });
            }
          });
        }
      },
      (error) => {
        console.log(error);
      }
    );
  }
}

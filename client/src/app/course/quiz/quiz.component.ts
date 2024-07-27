import { Component, OnInit } from '@angular/core';
import { CourseService } from '../../services/course.service';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';

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
  nom: string;
  questions: Question[];
}

interface Chapitre {
  id: number;
  chapitreNum: number;
  nom: string;
  statue: boolean;
  coursPdfPath: string | null;
  videoPath: string | null;
  synthese: string | null;
  schema: string | null;
  premium: boolean;
  quizId: number;
  quiz: Quiz;
}

interface Controle {
  id: number;
  nom: string;
  ennonce: string;
  solution: string;
  chapitreNum: number[];
}

interface Module {
  id: number;
  nom: string;
  chapitres: Chapitre[];
  controles: Controle[];
}
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
    this.route.paramMap.subscribe((params) => {
      const quizId = Number(params.get('quizid'));
      const moduleId = Number(params.get('testniveauid'));
      if (isNaN(quizId) && isNaN(moduleId)) {
        console.error('Invalid ID');
        return;
      }
      if (!isNaN(quizId)) {
        this.courseService.getQuizByID(quizId).subscribe(
          (quiz: Quiz | undefined) => {
            if (quiz) {
              this.quiz = quiz;
              this.getQuizResult(quizId, this.quiz.questions.length);
            } else {
              console.error('Quiz not found', quizId);
            }
          },
          (error) => {
            console.error('Error fetching quiz:', error);
          }
        );
      }
      if (!isNaN(moduleId)) {
        console.log('ID of module received');
        this.courseService.getTestNiveau(moduleId).subscribe(
          (quiz: Quiz | undefined) => {
            if (quiz) {
              this.quiz = quiz;
              this.isTest = true;
            } else {
              console.log('Test Niveau Not found');
            }
          },
          (error) => {
            console.error(
              'An error occured while requestion testniveau',
              error
            );
          }
        );
      }
    });
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
            (option) => (option.truth = 'true')
          );

          if (correctOption && correctOption.id === selectedOptionId) {
            note++;
          }
        });
        if (this.isTest) {
          console.log('the test will be uploaded');
        } else {
          if (this.isQuizAlreadyPassed) {
            this.courseService
              .updateQuizResult(this.quiz.id, note)
              .subscribe((res) => {
                Swal.fire({
                  title: `Votre note est :${res.note} / ${this.quiz.questions.length}`,
                  text: `Vous avez deja passé ce quiz si vous voulez passé ce quiz une autre fois clicker sur ok`,
                  icon: 'success',
                });
              });
          } else {
            this.courseService
              .createQuizResult(this.quiz.id, note)
              .subscribe((state) => {
                Swal.fire({
                  title: `Votre note est :${note} / ${this.quiz.questions.length}`,
                  text: `Vous avez deja passé ce quiz si vous voulez passé ce quiz une autre fois clicker sur ok`,
                  icon: 'success',
                });
              });
            console.log('your Note:', note);
          }
        }
      }
    }
  }

  getQuizResult(id: number, noteTotal: number) {
    this.courseService.getQuizResultById(id).subscribe(
      (res) => {
        Swal.fire({
          title: `Votre note est :${res.note} / ${noteTotal}`,
          text: `Vous avez deja passé ce quiz si vous voulez passé ce quiz une autre fois clicker sur ok`,
          icon: 'success',
        });
        this.note = res.note;
        this.isQuizAlreadyPassed = true;
        return this.note;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}

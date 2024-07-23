import { Component, OnInit } from '@angular/core';
import { CourseService } from '../../services/course.service';
import { ActivatedRoute, Router } from '@angular/router';

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
  ChapitreNum: number;
  nom: string;
  Statue: string;
  CoursPdfPath: string | null;
  VideoPath: string | null;
  Synthese: string | null;
  Schema: string | null;
  Premium: boolean;
  quizId: number;
  quiz: Quiz;
}

interface Controle {
  id: number;
  nom: string;
  ennonce: string;
  solution: string;
  ChapitreNum: number[];
}

interface Module {
  id: number;
  nom: string;
  chapitres: Chapitre[];
  controles?: Controle[];
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
  module: Module | undefined;

  constructor(
    private courseService: CourseService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const id = Number(params.get('id'));
      this.courseService.getQuizByID(id).subscribe(
        (quiz: Quiz | undefined) => {
          if (quiz) {
            this.quiz = quiz;
          } else {
            console.error('Quiz not found');
          }
        },
        (error) => {
          console.error('Error fetching quiz:', error);
        }
      );
    });
    this.courseService.getCourse().subscribe((module) => {
      this.module = module;
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
        // Handle quiz submission logic here, e.g., send answers to a server
        console.log('User answers:', this.selectedAnswers);
        this.route.paramMap.subscribe((params) => {
          const idParam = params.get('id');
          if (idParam) {
            const id = +idParam;
            this.courseService
              .getChapterNumber(id)
              .subscribe((chapterNumber) => {
                console.log(chapterNumber);
                if (chapterNumber !== null) {
                  this.courseService
                    .getControle(chapterNumber)
                    .subscribe((state) => {
                      console.log(state);
                      if (state) this.router.navigate(['/course/exam/', id]);
                      else this.router.navigate(['/course/cour/', id + 1]);
                    });
                } else {
                  console.log('Chapter not found');
                }
              });
          }
        });
      }
    }
  }
}

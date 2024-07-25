import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';
import { Quiz } from '../../interfaces/dashboard';
import { DashboardService } from '../../services/dashboard.service';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-chapter',
  templateUrl: './chapter.component.html',
  styleUrl: './chapter.component.css',
})
export class ChapterComponent implements OnInit {
  host = environment.apiUrl;
  pdfSrc!: string;
  /**
   *
   */
  chapterid!: number;
  constructor(
    private readonly dashboardService: DashboardService,
    private readonly route: ActivatedRoute
  ) {}
  chapitre: any;

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.chapterid = params['id'];
      this.dashboardService.GetChapitrebyid(this.chapterid).subscribe(
        (reponse) => {
          console.log(reponse);
          this.chapitre = reponse;
          this.quiz = this.chapitre.quiz;
        },
        (error) => {}
      );
    });
  }

  isLinear = false;
  quiz: any;

  addQuestion() {
    this.quiz.questions.push({
      id: 0,
      nom: '',
      quizId: this.quiz.id,
      options: [{ id: 0, nom: '', truth: false }],
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
    });
  }

  deleteOption(questionIndex: number, optionIndex: number) {
    // Check if the option array has more than one item
    if (this.quiz.questions[questionIndex].options.length > 1) {
      // Remove the specific option using its index
      this.quiz.questions[questionIndex].options.splice(optionIndex, 1);
    }
  }

  setCorrectOption(questionIndex: number, optionIndex: number) {
    this.quiz.questions[questionIndex].options.forEach(
      (option: any, index: any) => {
        option.truth = index === optionIndex;
      }
    );
  }
  transformQuizObject(quiz: any) {
    return {
      nom: quiz.nom,
      questions: quiz.questions.map((question: any) => ({
        id: question.id,
        nom: question.nom,
        options: question.options.map((option: any) => {
          const { questionId, ...rest } = option;
          return rest;
        }),
      })),
    };
  }

  onSubmit() {
    console.log('Quiz:', this.transformQuizObject(this.quiz));
    this.dashboardService
      .updatequiz(this.quiz.id, this.transformQuizObject(this.quiz))
      .subscribe(
        (response) => {
          console.log(response);
        },
        (error) => {}
      );
  }
}

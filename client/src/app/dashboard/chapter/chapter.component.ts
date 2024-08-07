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
import Swal from 'sweetalert2';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-chapter',
  templateUrl: './chapter.component.html',
  styleUrl: './chapter.component.css',
})
export class ChapterComponent implements OnInit {
  delete() {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.isConfirmed) {
        this.dashboardService.deletechapitre(this.chapterid).subscribe(
          (response) => {
            Swal.fire({
              title: 'Deleted!',
              text: 'Your file has been deleted.',
              icon: 'success',
            });
            window.location.href = `/dashboard/module/${this.chapitre.moduleId}`;
          },
          (error) => {
            Swal.fire({
              title: 'Deleted!',
              text: error.error,
              icon: 'error',
            });
          }
        );
      }
    });
  }
  chapterid!: number;

  host = environment.apiUrl;
  pdfSrc!: string;

  constructor(
    private readonly dashboardService: DashboardService,
    private readonly route: ActivatedRoute,
    public authservice: AuthService
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
        (error) => {
          Swal.fire(`error`, `${error.error}`, `error`);
        }
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
          Swal.fire(`succes`, `quiz updated successfuly`, `success`);
        },
        (error) => {
          Swal.fire(`error`, `${error.error}`, `error`);
        }
      );
  }
  SelectSynthese(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('File', file);
      formData.append('Id', this.chapterid.toString());
      this.dashboardService.updatechapitreSynthese(formData).subscribe(
        (response) => {
          this.chapitre.synthese = response.synthese;
        },
        (error) => {
          Swal.fire(`error`, `${error.error}`, `error`);
        }
      );
    }
  }
  SelectSchema(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('File', file);
      formData.append('Id', this.chapterid.toString());
      this.dashboardService.updatechapitreSchema(formData).subscribe(
        (response) => {
          this.chapitre.schema = response.schema;
        },
        (error) => {
          Swal.fire(`error`, `${error.error}`, `error`);
        }
      );
    }
  }
  SelectVideo(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('File', file);
      formData.append('Id', this.chapterid.toString());
      this.dashboardService.updatechapitreVideo(formData).subscribe(
        (response) => {
          this.chapitre.videoPath = response.videoPath;
        },
        (error) => {
          Swal.fire(`error`, `${error.error}`, `error`);
        }
      );
    }
  }
  SelectPdf(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('File', file);
      formData.append('Id', this.chapterid.toString());
      this.dashboardService.updatechapitrePdf(formData).subscribe(
        (response) => {
          this.chapitre.coursPdfPath = response.coursPdfPath;
        },
        (error) => {
          Swal.fire(`error`, `${error.error}`, `error`);
        }
      );
    }
  }
  modifierNom() {
    Swal.fire({
      title: 'Edit Controle  Name',
      input: 'text',
      inputLabel: 'Controle Name',
      inputValue: this.chapitre.nom,
      showCancelButton: true,
      confirmButtonText: 'Save',
      cancelButtonText: 'Cancel',
      preConfirm: (newName) => {
        if (!newName) {
          Swal.showValidationMessage('Please enter a valid name');
        }
        return newName;
      },
    }).then((result) => {
      if (result.isConfirmed) {
        this.dashboardService
          .updateinstitution(result.value, this.chapterid)
          .subscribe(
            (response) => {
              this.chapitre.nom = response.nom;
              console.log(response);
              Swal.fire(
                'Saved!',
                'Institution name has been updated.',
                'success'
              );
            },
            (error) => {
              Swal.fire(`error`, `${error.error}`, `error`);
            }
          );
      }
    });
  }
  refuser() {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes !',
    }).then((result) => {
      if (result.isConfirmed) {
        this.dashboardService.refuserchapitre(this.chapterid).subscribe(
          (response) => {
            console.log(response);
            this.chapitre.statue = response.statue;
            Swal.fire({
              title: 'Refuser!',
              text: 'Your file has been Refuser.',
              icon: 'success',
            });
          },
          (error) => {
            Swal.fire(`error`, `${error.error}`, `error`);
          }
        );
      }
    });
  }
  approuver() {
    if (
      this.chapitre.quiz.statue === 'Denied' ||
      this.chapitre.quiz.statue === 'Pending'
    ) {
      Swal.fire(
        'info',
        'il faut accepter le quiz avant d`accepter le chapitre entier',
        'info'
      );
    } else {
      Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
      }).then((result) => {
        if (result.isConfirmed) {
          this.dashboardService.approuverchapitre(this.chapterid).subscribe(
            (response) => {
              console.log(response);
              this.chapitre.statue = response.statue;
              Swal.fire({
                title: 'Accepter!',
                text: 'Your file has been Accepter.',
                icon: 'success',
              });
            },
            (error) => {
              Swal.fire(`error`, `${error.error}`, `error`);
            }
          );
        }
      });
    }
  }
  refuserquiz() {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes !',
    }).then((result) => {
      if (result.isConfirmed) {
        this.dashboardService.refuserquiz(this.chapitre.quiz.id).subscribe(
          (response) => {
            console.log(response);
            this.chapitre.quiz.statue = response.statue;
            Swal.fire({
              title: 'Refuser!',
              text: 'Your file has been Refuser.',
              icon: 'success',
            });
          },
          (error) => {
            Swal.fire(`error`, `${error.error}`, `error`);
          }
        );
      }
    });
  }
  approuverquiz() {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes',
    }).then((result) => {
      if (result.isConfirmed) {
        this.dashboardService.approuverquiz(this.chapitre.quiz.id).subscribe(
          (response) => {
            console.log(response);
            this.chapitre.quiz.statue = response.statue;
            Swal.fire({
              title: 'Accepter!',
              text: 'Your file has been Accepter.',
              icon: 'success',
            });
          },
          (error) => {
            Swal.fire(`error`, `${error.error}`, `error`);
          }
        );
      }
    });
  }
}

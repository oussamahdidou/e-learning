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
        // Show loading modal
        Swal.fire({
          title: 'Deleting...',
          text: 'Please wait while we process your request.',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        this.dashboardService.deletechapitre(this.chapterid).subscribe(
          (response) => {
            Swal.fire({
              title: 'Deleted!',
              text: 'Your file has been deleted.',
              icon: 'success',
            }).then(() => {
              window.location.href = `/dashboard/module/${this.chapitre.moduleId}`;
            });
          },
          (error) => {
            Swal.fire({
              title: 'Error!',
              text: error.error,
              icon: 'error',
            });
          }
        );
      }
    });
  }
  teacherCourse: any;
  studentCourse: any;
  convertLink() {
    if (
      this.chapitre.videoPath.includes('youtube.com/watch?v=') ||
      this.chapitre.videoPath.includes('youtu.be/')
    ) {
      this.isYoutubeLink = true;
      if (this.chapitre.videoPath.includes('youtube.com/watch?v=')) {
        const videoId = this.chapitre.videoPath.split('v=')[1].split('&')[0]; // Extract video ID
        this.chapitre.videoPath = `https://www.youtube.com/embed/${videoId}`;
      } else if (this.chapitre.videoPath.includes('youtu.be/')) {
        const videoId = this.chapitre.videoPath.split('youtu.be/')[1];
        this.chapitre.videoPath = `https://www.youtube.com/embed/${videoId}`;
      }
    } else {
    }
  }
  chapterid!: number;
  constructor(
    private readonly dashboardService: DashboardService,
    private readonly route: ActivatedRoute,
    public authservice: AuthService
  ) {}
  chapitre: any;
  isYoutubeLink: boolean = false;
  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.chapterid = params['id'];
      this.dashboardService.GetChapitrebyid(this.chapterid).subscribe(
        (reponse) => {
          // console.log(reponse);
          this.chapitre = reponse;
          this.teacherCourse = reponse.cours.find(
            (course: any) => course.type === 'Teacher'
          );
          this.studentCourse = reponse.cours.find(
            (course: any) => course.type === 'Student'
          );
          console.log(this.studentCourse);
          this.convertLink();
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
    // console.log('Quiz:', this.transformQuizObject(this.quiz));

    // Show loading modal
    Swal.fire({
      title: 'Updating...',
      text: 'Please wait while we process your request.',
      allowOutsideClick: false,
      didOpen: () => {
        Swal.showLoading();
      },
    });

    this.dashboardService
      .updatequiz(this.quiz.id, this.transformQuizObject(this.quiz))
      .subscribe(
        (response) => {
          Swal.fire('Success', 'Quiz updated successfully', 'success');
        },
        (error) => {
          Swal.fire('Error', `${error.error}`, 'error');
        }
      );
  }
  SelectStudentParagraphe(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('ParagrapheContenu', file);
      formData.append('CoursId', this.studentCourse.id);

      // Show loading modal
      Swal.fire({
        title: 'Uploading...',
        text: 'Please wait while the file is being uploaded.',
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });

      this.dashboardService.createparagraphe(formData).subscribe(
        (response) => {
          // Close the loading modal and show success message
          Swal.fire('Success', 'File uploaded successfully', 'success');
          this.studentCourse.paragraphes.push(response);
        },
        (error) => {
          // Close the loading modal and show error message
          Swal.fire('Error', `${error.error}`, 'error');
        }
      );
    }
  }
  SelectTeacherParagraphe(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('ParagrapheContenu', file);
      formData.append('CoursId', this.teacherCourse.id);

      // Show loading modal
      Swal.fire({
        title: 'Uploading...',
        text: 'Please wait while the file is being uploaded.',
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });

      this.dashboardService.createparagraphe(formData).subscribe(
        (response) => {
          // Close the loading modal and show success message
          Swal.fire('Success', 'File uploaded successfully', 'success');
          this.teacherCourse.paragraphes.push(response);
        },
        (error) => {
          // Close the loading modal and show error message
          Swal.fire('Error', `${error.error}`, 'error');
        }
      );
    }
  }
  SelectSynthese(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('File', file);
      formData.append('Id', this.chapterid.toString());

      // Show loading modal
      Swal.fire({
        title: 'Uploading...',
        text: 'Please wait while the file is being uploaded.',
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });

      this.dashboardService.updatechapitreSynthese(formData).subscribe(
        (response) => {
          // Close the loading modal and show success message
          Swal.fire('Success', 'File uploaded successfully', 'success');
          this.chapitre.synthese = response.synthese;
        },
        (error) => {
          // Close the loading modal and show error message
          Swal.fire('Error', `${error.error}`, 'error');
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

      // Show loading modal
      Swal.fire({
        title: 'Uploading...',
        text: 'Please wait while the file is being uploaded.',
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });

      this.dashboardService.updatechapitreSchema(formData).subscribe(
        (response) => {
          // Close the loading modal and show success message
          Swal.fire('Success', 'File uploaded successfully', 'success');
          this.chapitre.schema = response.schema;
        },
        (error) => {
          // Close the loading modal and show error message
          Swal.fire('Error', `${error.error}`, 'error');
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

      // Show loading modal
      Swal.fire({
        title: 'Uploading...',
        text: 'Please wait while the video is being uploaded.',
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });

      this.dashboardService.updatechapitreVideo(formData).subscribe(
        (response) => {
          // Close the loading modal and show success message
          Swal.fire('Success', 'Video uploaded successfully', 'success');
          this.chapitre.videoPath = response.videoPath;
        },
        (error) => {
          // Close the loading modal and show error message
          Swal.fire('Error', `${error.error}`, 'error');
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

      // Show loading modal
      Swal.fire({
        title: 'Uploading...',
        text: 'Please wait while the PDF is being uploaded.',
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });

      this.dashboardService.updatechapitrePdf(formData).subscribe(
        (response) => {
          // Close the loading modal and show success message
          Swal.fire('Success', 'PDF uploaded successfully', 'success');
          // console.log('and this is the response ', response);
          this.chapitre.coursPdfPath = response.coursPdfPath;
        },
        (error) => {
          // Close the loading modal and show error message
          Swal.fire('Error', `${error.error}`, 'error');
        }
      );
    }
  }

  modifierNom() {
    Swal.fire({
      title: 'Edit Chapitre Name',
      input: 'text',
      inputLabel: 'Chapitre Name',
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
        // Show loading modal
        Swal.fire({
          title: 'Saving...',
          text: 'Please wait while the name is being updated.',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        this.dashboardService
          .updatechapitrenom(result.value, this.chapterid)
          .subscribe(
            (response) => {
              this.chapitre.nom = response.nom;
              Swal.fire('Saved!', 'Chapitre name has been updated.', 'success');
            },
            (error) => {
              Swal.fire('Error', `${error.error}`, 'error');
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
      confirmButtonText: 'Yes!',
    }).then((result) => {
      if (result.isConfirmed) {
        // Show loading modal
        Swal.fire({
          title: 'Processing...',
          text: 'Please wait while your request is being processed.',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        this.dashboardService.refuserchapitre(this.chapterid).subscribe(
          (response) => {
            this.chapitre.statue = response.statue;
            Swal.fire({
              title: 'Refused!',
              text: 'Your file has been refused.',
              icon: 'success',
            });
          },
          (error) => {
            Swal.fire('Error', `${error.error}`, 'error');
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
        'Info',
        'You need to approve the quiz before accepting the entire chapter.',
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
          // Show loading modal
          Swal.fire({
            title: 'Processing...',
            text: 'Please wait while your request is being processed.',
            allowOutsideClick: false,
            didOpen: () => {
              Swal.showLoading();
            },
          });

          this.dashboardService.approuverchapitre(this.chapterid).subscribe(
            (response) => {
              this.chapitre.statue = response.statue;
              Swal.fire({
                title: 'Accepted!',
                text: 'The chapter has been accepted.',
                icon: 'success',
              });
            },
            (error) => {
              Swal.fire('Error', `${error.error}`, 'error');
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
      confirmButtonText: 'Yes!',
    }).then((result) => {
      if (result.isConfirmed) {
        // Show loading modal
        Swal.fire({
          title: 'Processing...',
          text: 'Please wait while your request is being processed.',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        this.dashboardService.refuserquiz(this.chapitre.quiz.id).subscribe(
          (response) => {
            this.chapitre.quiz.statue = response.statue;
            Swal.fire({
              title: 'Refused!',
              text: 'The quiz has been refused.',
              icon: 'success',
            });
          },
          (error) => {
            Swal.fire('Error', `${error.error}`, 'error');
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
        // Show loading modal
        Swal.fire({
          title: 'Processing...',
          text: 'Please wait while your request is being processed.',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        this.dashboardService.approuverquiz(this.chapitre.quiz.id).subscribe(
          (response) => {
            this.chapitre.quiz.statue = response.statue;
            Swal.fire({
              title: 'Accepted!',
              text: 'The quiz has been accepted.',
              icon: 'success',
            });
          },
          (error) => {
            Swal.fire('Error', `${error.error}`, 'error');
          }
        );
      }
    });
  }
}

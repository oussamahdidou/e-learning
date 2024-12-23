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
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-chapter',
  templateUrl: './chapter.component.html',
  styleUrl: './chapter.component.css',
})
export class ChapterComponent implements OnInit {
  modifierNumero() {
    Swal.fire({
      title: 'Edit Chapitre Numero',
      input: 'number',
      inputLabel: 'Chapitre Numero',
      inputValue: this.chapitre.chapitreNum,
      showCancelButton: true,
      confirmButtonText: 'Save',
      cancelButtonText: 'Cancel',
      preConfirm: (newName) => {
        if (!newName) {
          Swal.showValidationMessage('Please enter a valid Numero');
        }
        return newName;
      },
    }).then((result) => {
      if (result.isConfirmed) {
        // Show loading modal
        Swal.fire({
          title: 'Saving...',
          text: 'Please wait while the numero is being updated.',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        this.dashboardService
          .updateChapitreNumero(this.chapterid, result.value)
          .subscribe(
            (response) => {
              this.chapitre.chapitreNum = response.chapitreNum;
              Swal.fire(
                'Saved!',
                'Chapitre numero has been updated.',
                'success'
              );
            },
            (error) => {
              Swal.fire('Error', `${error.error}`, 'error');
            }
          );
      }
    });
  }
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

  chapterid!: number;
  constructor(
    private readonly dashboardService: DashboardService,
    private readonly route: ActivatedRoute,
    public authservice: AuthService
  ) {}
  chapitre: any;

  getFileType(filePath: string): string {
    const extension = filePath.split('.').pop()?.toLowerCase();
    switch (extension) {
      case 'pdf':
        return 'pdf';
      case 'doc':
      case 'docx':
        return 'word';
      case 'ppt':
      case 'pptx':
        return 'powerpoint';
      case 'jpg':
      case 'jpeg':
      case 'png':
      case 'gif':
        return 'image';
      default:
        return 'unknown';
    }
  }
  getViewerUrl(filePath: string): string {
    return `https://docs.google.com/viewer?url=${filePath}&embedded=true`;
  }
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
          console.log(reponse);
          //  this.convertLink();
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
  AddSynthese(event: any) {
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

      this.dashboardService.addsynthese(formData).subscribe(
        (response) => {
          // Close the loading modal and show success message
          Swal.fire('Success', 'File uploaded successfully', 'success');
          this.chapitre.syntheses.push(response);
        },
        (error) => {
          // Close the loading modal and show error message
          Swal.fire('Error', `${error.error}`, 'error');
        }
      );
    }
  }

  AddSchema(event: any) {
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

      this.dashboardService.addschema(formData).subscribe(
        (response) => {
          // Close the loading modal and show success message
          Swal.fire('Success', 'File uploaded successfully', 'success');
          this.chapitre.schemas.push(response);
        },
        (error) => {
          // Close the loading modal and show error message
          Swal.fire('Error', `${error.error}`, 'error');
        }
      );
    }
  }

  AddVideo(event: any) {
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

      this.dashboardService.addvideo(formData).subscribe(
        (response) => {
          // Close the loading modal and show success message
          Swal.fire('Success', 'Video uploaded successfully', 'success');
          this.chapitre.videos.push(response);
          // this.convertLink(); // Update the video path with the new URL`
        },
        (error) => {
          // Close the loading modal and show error message
          Swal.fire('Error', `${error.error}`, 'error');
        }
      );
    }
  }
  videoUrl!: string;
  AddVideoWithLink() {
    if (this.videoUrl) {
      // Show loading modal
      Swal.fire({
        title: 'Updating...',
        text: 'Please wait while the video URL is being updated.',
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });

      this.dashboardService
        .addvideolink(this.chapterid, this.videoUrl)
        .subscribe(
          (response) => {
            // Close the loading modal and show success message
            Swal.fire('Success', 'Video updated successfully', 'success');
            this.chapitre.videos.push(response);
            this.videoUrl = '';
            // Update the video path with the new URL
          },
          (error) => {
            // Close the loading modal and show error message
            Swal.fire('Error', `${error.error}`, 'error');
          }
        );
    } else {
      Swal.fire('Error', 'Please provide a valid video URL', 'error');
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
  deleteparagraphe(id: number) {
    // Step 1: Show confirmation modal
    Swal.fire({
      title: 'Are you sure?',
      text: 'Do you want to delete this paragraph?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        // Step 2: Show loading modal
        Swal.fire({
          title: 'Deleting...',
          text: 'Please wait while the paragraph is being deleted.',
          allowOutsideClick: false,
          allowEscapeKey: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        // Step 3: Call the delete API
        this.dashboardService.deleteparagraphe(id).subscribe(
          (response) => {
            // Step 4: Close loading modal and show success message
            Swal.close();
            Swal.fire({
              title: 'Deleted!',
              text: 'The paragraph has been deleted successfully.',
              icon: 'success',
            });
            this.studentCourse.paragraphes =
              this.studentCourse.paragraphes.filter(
                (cours: any) => cours.id !== id
              );
            this.teacherCourse.paragraphes =
              this.studentCourse.paragraphes.filter(
                (cours: any) => cours.id !== id
              );
          },
          (error) => {
            // Step 5: Close loading modal and show error message
            Swal.close();
            Swal.fire({
              title: 'Error!',
              text: 'There was an error deleting the paragraph. Please try again later.',
              icon: 'error',
            });
          }
        );
      }
    });
  }
  supprimervideo(id: number) {
    // Step 1: Show confirmation modal
    Swal.fire({
      title: 'Are you sure?',
      text: 'Do you want to delete this video?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        // Step 2: Show loading modal
        Swal.fire({
          title: 'Deleting...',
          text: 'Please wait while the video is being deleted.',
          allowOutsideClick: false,
          allowEscapeKey: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        // Step 3: Call the delete API
        this.dashboardService.deletevideo(id).subscribe(
          (response) => {
            // Step 4: Close loading modal and show success message
            Swal.close();
            Swal.fire({
              title: 'Deleted!',
              text: 'The paragraph has been deleted successfully.',
              icon: 'success',
            });
            this.chapitre.videos = this.chapitre.videos.filter(
              (cours: any) => cours.id !== id
            );
          },
          (error) => {
            // Step 5: Close loading modal and show error message
            Swal.close();
            Swal.fire({
              title: 'Error!',
              text: 'There was an error deleting the paragraph. Please try again later.',
              icon: 'error',
            });
          }
        );
      }
    });
  }

  supprimerschema(id: number) {
    // Step 1: Show confirmation modal
    Swal.fire({
      title: 'Are you sure?',
      text: 'Do you want to delete this schema?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        // Step 2: Show loading modal
        Swal.fire({
          title: 'Deleting...',
          text: 'Please wait while the schema is being deleted.',
          allowOutsideClick: false,
          allowEscapeKey: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        // Step 3: Call the delete API
        this.dashboardService.deleteschema(id).subscribe(
          (response) => {
            // Step 4: Close loading modal and show success message
            Swal.close();
            Swal.fire({
              title: 'Deleted!',
              text: 'The paragraph has been deleted successfully.',
              icon: 'success',
            });
            this.chapitre.schemas = this.chapitre.schemas.filter(
              (cours: any) => cours.id !== id
            );
          },
          (error) => {
            // Step 5: Close loading modal and show error message
            Swal.close();
            Swal.fire({
              title: 'Error!',
              text: 'There was an error deleting the paragraph. Please try again later.',
              icon: 'error',
            });
          }
        );
      }
    });
  }

  supprimersynthese(id: number) {
    // Step 1: Show confirmation modal
    Swal.fire({
      title: 'Are you sure?',
      text: 'Do you want to delete this synthese?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        // Step 2: Show loading modal
        Swal.fire({
          title: 'Deleting...',
          text: 'Please wait while the synthese is being deleted.',
          allowOutsideClick: false,
          allowEscapeKey: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        // Step 3: Call the delete API
        this.dashboardService.deletesynthese(id).subscribe(
          (response) => {
            // Step 4: Close loading modal and show success message
            Swal.close();
            Swal.fire({
              title: 'Deleted!',
              text: 'The paragraph has been deleted successfully.',
              icon: 'success',
            });
            this.chapitre.syntheses = this.chapitre.syntheses.filter(
              (cours: any) => cours.id !== id
            );
          },
          (error) => {
            // Step 5: Close loading modal and show error message
            Swal.close();
            Swal.fire({
              title: 'Error!',
              text: 'There was an error deleting the paragraph. Please try again later.',
              icon: 'error',
            });
          }
        );
      }
    });
  }
}

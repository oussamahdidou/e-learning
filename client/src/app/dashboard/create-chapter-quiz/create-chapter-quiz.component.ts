import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { DashboardService } from '../../services/dashboard.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-create-chapter-quiz',
  templateUrl: './create-chapter-quiz.component.html',
  styleUrls: ['./create-chapter-quiz.component.css'],
})
export class CreateChapterQuizComponent {
  isLinear = true;
  chapterFormGroup: FormGroup;
  quizFormGroup: FormGroup;
  moduleId: number = 0;

  constructor(
    private _formBuilder: FormBuilder,
    private readonly route: ActivatedRoute,
    private readonly dashboardservice: DashboardService
  ) {
    this.route.params.subscribe((params) => {
      this.moduleId = params['id'];
    });

    this.chapterFormGroup = this._formBuilder.group({
      nom: [''],
      number: [''],
      studentCourseParagraphs: this._formBuilder.array([]),
      professorCourseParagraphs: this._formBuilder.array([]),
      videoType: ['file'],
      coursVideoFile: [null],
      coursVideoLink: [''],
      schema: [''],
      synthese: [''],
    });

    this.quizFormGroup = this._formBuilder.group({
      quizName: [''],
      questions: this._formBuilder.array([]),
    });
  }

  get questions(): FormArray {
    return this.quizFormGroup.get('questions') as FormArray;
  }

  addQuestion() {
    this.questions.push(
      this._formBuilder.group({
        nom: [''],
        options: this._formBuilder.array([
          this._formBuilder.group({
            nom: [''],
            truth: [false],
          }),
        ]),
      })
    );
  }

  removeQuestion(index: number) {
    this.questions.removeAt(index);
  }

  getOptions(questionIndex: number): FormArray {
    return this.questions.at(questionIndex)?.get('options') as FormArray;
  }

  addOption(questionIndex: number) {
    this.getOptions(questionIndex).push(
      this._formBuilder.group({
        nom: [''],
        truth: [false],
      })
    );
  }

  removeOption(questionIndex: number, optionIndex: number) {
    this.getOptions(questionIndex).removeAt(optionIndex);
  }

  onFileChange(event: any, field: string) {
    const file = event.target.files[0];
    this.chapterFormGroup.patchValue({ [field]: file });
  }

  validateQuizData(quizData: any) {
    // Ensure that truth is a boolean
    quizData.questions.forEach((question: any) => {
      question.options.forEach((option: any) => {
        option.truth = !!option.truth; // Convert to boolean if it's not
      });
    });
    return quizData;
  }

  get studentCourseParagraphs() {
    return this.chapterFormGroup.get('studentCourseParagraphs') as FormArray;
  }

  get professorCourseParagraphs() {
    return this.chapterFormGroup.get('professorCourseParagraphs') as FormArray;
  }

  // Methods to add/remove paragraphs
  addStudentParagraph() {
    this.studentCourseParagraphs.push(
      this._formBuilder.group({
        file: [''],
      })
    );
  }

  removeStudentParagraph(index: number) {
    this.studentCourseParagraphs.removeAt(index);
  }

  addProfessorParagraph() {
    this.professorCourseParagraphs.push(
      this._formBuilder.group({
        file: [''],
      })
    );
  }

  removeProfessorParagraph(index: number) {
    this.professorCourseParagraphs.removeAt(index);
  }

  // Method to handle video type change
  onVideoTypeChange(event: any) {
    const value = event.target.value;
    if (value === 'file') {
      this.chapterFormGroup.get('coursVideoLink')!.clearValidators();
    } else {
      this.chapterFormGroup.get('coursVideoFile')!.clearValidators();
    }
    this.chapterFormGroup.get('coursVideoFile')!.updateValueAndValidity();
    this.chapterFormGroup.get('coursVideoLink')!.updateValueAndValidity();
  }

  // Methods to check video type
  isVideoFile() {
    return this.chapterFormGroup.get('videoType')?.value === 'file';
  }

  isVideoLink() {
    return this.chapterFormGroup.get('videoType')?.value === 'link';
  }

  // Submit handler
  // onSubmit() {
  //   if (this.chapterFormGroup.valid || this.quizFormGroup.valid) {
  //     const formData = new FormData();

  //     // Append text fields if they are provided
  //     if (this.chapterFormGroup.get('nom')!.value) {
  //       formData.append('Nom', this.chapterFormGroup.get('nom')!.value);
  //     }

  //     if (this.chapterFormGroup.get('number')!.value) {
  //       formData.append('Number', this.chapterFormGroup.get('number')!.value);
  //     }

  //     // Append student course paragraphs
  //     this.studentCourseParagraphs.controls.forEach((control, index) => {
  //       if (control.get('file')!.value) {
  //         formData.append(
  //           `StudentCourseParagraphs`,
  //           control.get('file')!.value
  //         );
  //       }
  //     });

  //     // Append professor course paragraphs
  //     this.professorCourseParagraphs.controls.forEach((control, index) => {
  //       if (control.get('file')!.value) {
  //         formData.append(
  //           `ProfessorCourseParagraphs`,
  //           control.get('file')!.value
  //         );
  //       }
  //     });

  //     // Append video file or link if provided
  //     if (
  //       this.isVideoFile() &&
  //       this.chapterFormGroup.get('coursVideoFile')!.value
  //     ) {
  //       formData.append(
  //         'CoursVideoFile',
  //         this.chapterFormGroup.get('coursVideoFile')!.value
  //       );
  //     } else if (
  //       this.isVideoLink() &&
  //       this.chapterFormGroup.get('coursVideoLink')!.value
  //     ) {
  //       formData.append(
  //         'CoursVideoLink',
  //         this.chapterFormGroup.get('coursVideoLink')!.value
  //       );
  //     }

  //     // Append schema and synthese files if provided
  //     if (this.chapterFormGroup.get('schema')!.value) {
  //       formData.append('Schema', this.chapterFormGroup.get('schema')!.value);
  //     }

  //     if (this.chapterFormGroup.get('synthese')!.value) {
  //       formData.append(
  //         'Synthese',
  //         this.chapterFormGroup.get('synthese')!.value
  //       );
  //     }

  //     // Submit the form data (this would be an API call)
  //     console.log('Form Submitted:', formData);
  //     // Example: this.dashboardservice.submitChapter(formData).subscribe(response => console.log(response));
  //   } else {
  //     console.error('Form is invalid');
  //   }
  // }
  onSubmit() {
    //   if (this.chapterFormGroup.valid && this.quizFormGroup.valid) {
    const formData = new FormData();

    // Append text fields if they are provided
    if (this.chapterFormGroup.get('nom')!.value) {
      formData.append('Nom', this.chapterFormGroup.get('nom')!.value);
    }

    if (this.chapterFormGroup.get('number')!.value) {
      formData.append(
        'Number',
        this.chapterFormGroup.get('number')!.value.toString()
      );
    }

    // Append student course paragraphs
    this.studentCourseParagraphs.controls.forEach((control, index) => {
      if (control.get('file')!.value) {
        formData.append(`StudentCourseParagraphs`, control.get('file')!.value);
      }
    });

    // Append professor course paragraphs
    this.professorCourseParagraphs.controls.forEach((control, index) => {
      if (control.get('file')!.value) {
        formData.append(
          `ProfessorCourseParagraphs`,
          control.get('file')!.value
        );
      }
    });

    // Append video file or link if provided
    if (
      this.isVideoFile() &&
      this.chapterFormGroup.get('coursVideoFile')!.value
    ) {
      formData.append(
        'CoursVideoFile',
        this.chapterFormGroup.get('coursVideoFile')!.value
      );
    } else if (
      this.isVideoLink() &&
      this.chapterFormGroup.get('coursVideoLink')!.value
    ) {
      formData.append(
        'CoursVideoLink',
        this.chapterFormGroup.get('coursVideoLink')!.value
      );
    }

    // Append schema and synthese files if provided
    if (this.chapterFormGroup.get('schema')!.value) {
      formData.append('Schema', this.chapterFormGroup.get('schema')!.value);
    }

    if (this.chapterFormGroup.get('synthese')!.value) {
      formData.append('Synthese', this.chapterFormGroup.get('synthese')!.value);
    }

    // Append quiz data
    let quizData = this.quizFormGroup.value;
    quizData = this.validateQuizData(quizData); // Ensure data is valid
    formData.append('ModuleId', this.moduleId.toString());
    console.log(JSON.stringify(quizData.questions));

    // Show loading modal
    Swal.fire({
      title: 'Processing...',
      text: 'Please wait while we process your request.',
      allowOutsideClick: false,
      didOpen: () => {
        Swal.showLoading();
      },
    });

    this.dashboardservice
      .createquiz({
        nom: quizData.quizName,
        statue: 'Pending',
        questions: quizData.questions,
      })
      .subscribe(
        (response) => {
          formData.append('QuizId', response.id.toString());
          this.dashboardservice.createchapter(formData).subscribe(
            (response) => {
              Swal.fire({
                title: 'Success!',
                text: 'Chapter created successfully.',
                icon: 'success',
              }).then(() => {
                window.location.href = `/dashboard/module/${this.moduleId}`;
              });
            },
            (error) => {
              Swal.fire('Error', `${error.error}`, 'error');
              console.error('Error response:', error);
            }
          );
        },
        (error) => {
          Swal.fire('Error', `${error.error}`, 'error');
          console.error('Quiz creation error:', error);
        }
      );
  }
}

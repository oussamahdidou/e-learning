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
      schema: [null],
      synthese: [null],
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

  onFileChange(event: any, field: string, index?: number) {
    const file = event.target.files[0];
    if (file) {
      // Check if field refers to an array of form controls
      if (
        field === 'studentCourseParagraphs' ||
        field === 'professorCourseParagraphs'
      ) {
        if (index !== undefined) {
          const formArray = this.chapterFormGroup.get(field) as FormArray;
          if (formArray && formArray.at(index)) {
            // Safely update form control value
            formArray.at(index).patchValue({ fileData: file });
          }
        }
      } else {
        const control = this.chapterFormGroup.get(field);
        if (control) {
          control.setValue(file); // Safely set the value of the control
        }
      }
    }
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
        fileData: [null], // Added to store the file
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
        fileData: [null], // Added to store the file
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

  onSubmit() {
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
    this.studentCourseParagraphs.controls.forEach((control) => {
      if (control.get('fileData')!.value) {
        const file = control.get('fileData')!.value;
        formData.append('StudentCourseParagraphs', file);
      }
    });

    // Append professor course paragraphs
    this.professorCourseParagraphs.controls.forEach((control) => {
      if (control.get('fileData')!.value) {
        const file = control.get('fileData')!.value;
        formData.append('ProfessorCourseParagraphs', file);
      }
    });

    // Append video file or link if provided
    const videoFile = this.chapterFormGroup.get('coursVideoFile')!.value;
    if (videoFile) {
      formData.append('CoursVideoFile', videoFile);
    }

    const videoLink = this.chapterFormGroup.get('coursVideoLink')!.value;
    if (videoLink) {
      formData.append('CoursVideoLink', videoLink);
    }

    // Append schema and synthese files if provided
    if (this.chapterFormGroup.get('schema')!.value) {
      formData.append('Schema', this.chapterFormGroup.get('schema')!.value);
    }

    if (this.chapterFormGroup.get('synthese')!.value) {
      formData.append('Synthese', this.chapterFormGroup.get('synthese')!.value);
    }

    console.log('Form Data Entries:');
    formData.forEach((value, key) => {
      console.log(`${key}: ${value}`);
    });

    // Append quiz data
    let quizData = this.quizFormGroup.value;
    quizData = this.validateQuizData(quizData); // Ensure data is valid

    formData.append('ModuleId', this.moduleId.toString());

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
                console.log(response);
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

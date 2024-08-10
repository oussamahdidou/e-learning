import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Quiz } from '../../interfaces/dashboard';
import { ActivatedRoute } from '@angular/router';
import { DashboardService } from '../../services/dashboard.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-create-chapter-quiz',
  templateUrl: './create-chapter-quiz.component.html',
  styleUrl: './create-chapter-quiz.component.css',
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
      nom: ['', Validators.required],
      number: ['', Validators.required],
      coursPdf: [null, Validators.required],
      coursVideo: [null, Validators.required],
      schema: [null, Validators.required],
      synthese: [null, Validators.required],
    });

    this.quizFormGroup = this._formBuilder.group({
      quizName: ['', Validators.required],
      questions: this._formBuilder.array([]),
    });
  }

  get questions(): FormArray {
    return this.quizFormGroup.get('questions') as FormArray;
  }

  addQuestion() {
    this.questions.push(
      this._formBuilder.group({
        nom: ['', Validators.required],
        options: this._formBuilder.array([
          this._formBuilder.group({
            nom: ['', Validators.required],
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
    return this.questions.at(questionIndex).get('options') as FormArray;
  }

  addOption(questionIndex: number) {
    this.getOptions(questionIndex).push(
      this._formBuilder.group({
        nom: ['', Validators.required],
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

  onSubmit() {
    if (this.chapterFormGroup.valid && this.quizFormGroup.valid) {
      const formData = new FormData();
      formData.append(
        'ChapitreNum',
        this.chapterFormGroup.get('number')?.value.toString()
      );
      formData.append('Nom', this.chapterFormGroup.get('nom')?.value);
      formData.append('Premium', 'true'); // Ensure boolean is correctly formatted

      // Append files
      const files = [
        { key: 'CoursPdf', file: this.chapterFormGroup.get('coursPdf')?.value },
        { key: 'Video', file: this.chapterFormGroup.get('coursVideo')?.value },
        { key: 'Synthese', file: this.chapterFormGroup.get('synthese')?.value },
        { key: 'Schema', file: this.chapterFormGroup.get('schema')?.value },
      ];
      files.forEach(({ key, file }) => {
        if (file) {
          formData.append(key, file, file.name);
        }
      });

      // Append quiz data
      let quizData = this.quizFormGroup.value;
      quizData = this.validateQuizData(quizData); // Ensure data is valid
      formData.append('ModuleId', this.moduleId.toString());
      console.log(JSON.stringify(quizData.questions));

      this.dashboardservice
        .createquiz({
          nom: quizData.quizName,
          statue: 'Pending',
          questions: quizData.questions,
        })
        .subscribe(
          (reponse) => {
            formData.append('QuizId', reponse.id.toString());
            this.dashboardservice.createchapter(formData).subscribe(
              (response) => {
                console.log(response);
                window.location.href = `/dashboard/module/${this.moduleId}`;
              },
              (error) => {
                Swal.fire(`error`, `${error.error}`, `error`);
                console.error('Error response:', error);
              }
            );
          },
          (error) => {
            Swal.fire(`error`, `${error.error}`, `error`);
            console.error('Quiz creation error:', error);
          }
        );
    }
  }
}

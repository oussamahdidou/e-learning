import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Quiz } from '../../interfaces/dashboard';

@Component({
  selector: 'app-create-chapter-quiz',
  templateUrl: './create-chapter-quiz.component.html',
  styleUrl: './create-chapter-quiz.component.css',
})
export class CreateChapterQuizComponent {
  isLinear = true;
  chapterFormGroup: FormGroup;
  quizFormGroup: FormGroup;

  constructor(private _formBuilder: FormBuilder) {
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
        id: [0], // Initialize with default value
        nom: ['', Validators.required],
        quizId: [0], // Initialize with default value
        options: this._formBuilder.array([
          this._formBuilder.group({
            id: [0], // Initialize with default value
            nom: ['', Validators.required],
            truth: [false],
            questionId: [0], // Initialize with default value
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
        id: [0], // Initialize with default value
        nom: ['', Validators.required],
        truth: [false],
        questionId: [this.questions.at(questionIndex).get('id')?.value], // Link to question ID
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

  onSubmit() {
    if (this.chapterFormGroup.valid && this.quizFormGroup.valid) {
      const chapterData = this.chapterFormGroup.value;
      const quizData = this.quizFormGroup.value;

      const quiz: Quiz = {
        id: 0, // Default value or replace with actual ID logic
        nom: quizData.quizName,
        statue: 'Draft', // Or any other default status
        questions: quizData.questions.map((q: any) => ({
          id: q.id,
          nom: q.nom,
          quizId: 0, // Default value or replace with actual quiz ID logic
          options: q.options.map((o: any) => ({
            id: o.id,
            nom: o.nom,
            truth: o.truth,
            questionId: q.id,
          })),
        })),
      };

      console.log('Chapter Data:', chapterData);
      console.log('Quiz Data:', quiz);
    }
  }
}

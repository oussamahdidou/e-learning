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
      videos: this._formBuilder.array([]),

      schemas: this._formBuilder.array([]),
      syntheses: this._formBuilder.array([]),
    });

    this.quizFormGroup = this._formBuilder.group({
      quizName: [''],
      questions: this._formBuilder.array([]),
    });
  }

  get questions() {
    return this.quizFormGroup.get('questions') as FormArray;
  }

  addQuestion() {
    const questionForm = this._formBuilder.group({
      nom: '',
      options: this._formBuilder.array([
        this._formBuilder.group({
          nom: '',
          truth: false,
        }),
        this._formBuilder.group({
          nom: '',
          truth: false,
        }),
      ]),
    });

    this.questions.push(questionForm);
  }

  removeQuestion(index: number) {
    this.questions.removeAt(index);
  }

  getOptions(questionIndex: number) {
    return this.questions.at(questionIndex).get('options') as FormArray;
  }

  addOption(questionIndex: number) {
    const optionForm = this._formBuilder.group({
      nom: '',
      truth: false,
    });

    this.getOptions(questionIndex).push(optionForm);
  }

  removeOption(questionIndex: number, optionIndex: number) {
    this.getOptions(questionIndex).removeAt(optionIndex);
  }

  get studentCourseParagraphs() {
    return this.chapterFormGroup.get('studentCourseParagraphs') as FormArray;
  }

  addStudentParagraph() {
    const paragraphForm = this._formBuilder.group({
      fileData: [null],
    });

    this.studentCourseParagraphs.push(paragraphForm);
  }

  removeStudentParagraph(index: number) {
    this.studentCourseParagraphs.removeAt(index);
  }

  get professorCourseParagraphs() {
    return this.chapterFormGroup.get('professorCourseParagraphs') as FormArray;
  }

  addProfessorParagraph() {
    const paragraphForm = this._formBuilder.group({
      fileData: [null],
    });

    this.professorCourseParagraphs.push(paragraphForm);
  }

  removeProfessorParagraph(index: number) {
    this.professorCourseParagraphs.removeAt(index);
  }

  get videos() {
    return this.chapterFormGroup.get('videos') as FormArray;
  }

  addVideo() {
    const videoForm = this._formBuilder.group({
      videoType: ['file'], // Default to 'file'
      coursVideoFile: [null],
      coursVideoLink: [null],
    });

    this.videos.push(videoForm);
  }

  removeVideo(index: number) {
    this.videos.removeAt(index);
  }

  isVideoFile(index: number): boolean {
    const videoType = this.videos.at(index).get('videoType')?.value;
    return videoType === 'file';
  }

  isVideoLink(index: number): boolean {
    const videoType = this.videos.at(index).get('videoType')?.value;
    return videoType === 'link';
  }

  onVideoTypeChange(event: any, index: number) {
    const videoType = event.target.value;
    this.videos.at(index).get('videoType')?.setValue(videoType);
  }

  get schemas() {
    return this.chapterFormGroup.get('schemas') as FormArray;
  }

  addSchema() {
    const schemaForm = this._formBuilder.group({
      file: [''],
    });

    this.schemas.push(schemaForm);
  }

  removeSchema(index: number) {
    this.schemas.removeAt(index);
  }

  get syntheses() {
    return this.chapterFormGroup.get('syntheses') as FormArray;
  }

  addSynthese() {
    const syntheseForm = this._formBuilder.group({
      file: [''],
    });

    this.syntheses.push(syntheseForm);
  }

  removeSynthese(index: number) {
    this.syntheses.removeAt(index);
  }

  onStudentCourseFileChange(event: any, index: number) {
    const file = event.target.files[0];
    if (file) {
      const formArray = this.chapterFormGroup.get(
        'studentCourseParagraphs'
      ) as FormArray;
      formArray.at(index).get('fileData')?.setValue(file);
      console.log(`Student Course Paragraph ${index}:`, file);
    }
  }

  onProfessorCourseFileChange(event: any, index: number) {
    const file = event.target.files[0];
    if (file) {
      const formArray = this.chapterFormGroup.get(
        'professorCourseParagraphs'
      ) as FormArray;
      formArray.at(index).get('fileData')?.setValue(file);
      console.log(`Professor Course Paragraph ${index}:`, file);
    }
  }

  onVideoFileChange(event: any, index: number) {
    const file = event.target.files[0];
    if (file) {
      const formArray = this.chapterFormGroup.get('videos') as FormArray;
      formArray.at(index).get('coursVideoFile')?.setValue(file);
      console.log(`Video File ${index}:`, file);
    }
  }

  onSchemaFileChange(event: any, index: number) {
    const file = event.target.files[0];
    if (file) {
      const formArray = this.chapterFormGroup.get('schemas') as FormArray;
      formArray.at(index).get('file')?.setValue(file);
      console.log(`Schema File ${index}:`, file);
    }
  }

  onSyntheseFileChange(event: any, index: number) {
    const file = event.target.files[0];
    if (file) {
      const formArray = this.chapterFormGroup.get('syntheses') as FormArray;
      formArray.at(index).get('file')?.setValue(file);
      console.log(`Synthese File ${index}:`, file);
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
  onSubmit() {
    // Append chapter data
    const formData = new FormData();

    // Append text fields
    formData.append('Nom', this.chapterFormGroup.get('nom')?.value);
    formData.append('Number', this.chapterFormGroup.get('number')?.value);

    // Append student course paragraphs
    this.chapterFormGroup
      .get('studentCourseParagraphs')
      ?.value.forEach((paragraph: any, index: number) => {
        formData.append(`StudentCourseParagraphs`, paragraph.fileData);
      });

    // Append professor course paragraphs
    this.chapterFormGroup
      .get('professorCourseParagraphs')
      ?.value.forEach((paragraph: any, index: number) => {
        formData.append(`ProfessorCourseParagraphs`, paragraph.fileData);
      });

    // Append video files
    this.chapterFormGroup
      .get('videos')
      ?.value.forEach((video: any, index: number) => {
        if (video.coursVideoFile) {
          formData.append(`Videos`, video.coursVideoFile);
        }
        if (video.coursVideoLink) {
          formData.append(`VideosLink`, video.coursVideoLink);
        }
      });

    // Append schemas
    this.chapterFormGroup
      .get('schemas')
      ?.value.forEach((schema: any, index: number) => {
        formData.append(`Schemas`, schema.file);
      });

    // Append syntheses
    this.chapterFormGroup
      .get('syntheses')
      ?.value.forEach((synthese: any, index: number) => {
        formData.append(`Syntheses`, synthese.file);
      });
    formData.append('ModuleId', this.moduleId.toString());

    console.log('Form Data Entries:');
    formData.forEach((value, key) => {
      console.log(`${key}: ${value}`);
    });

    // Append quiz data
    let quizData = this.quizFormGroup.value;
    quizData = this.validateQuizData(quizData); // Ensure data is valid

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

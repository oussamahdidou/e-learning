import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, FormArray } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-createcontrole',
  templateUrl: './createcontrole.component.html',
  styleUrl: './createcontrole.component.css',
})
export class CreatecontroleComponent implements OnInit {
  controleForm: FormGroup;
  chapitreList: string[] = [
    'Chapitre 1',
    'Chapitre 2',
    'Chapitre 3',
    'Chapitre 4',
  ];
  enonceFile: File | null = null;
  solutionFile: File | null = null;

  constructor(private fb: FormBuilder) {
    this.controleForm = this.fb.group({
      nomControle: [''],
      enonce: [null],
      solution: [null],
      examFinal: [false],
      chapitres: this.fb.array([]),
    });

    this.addChapitreCheckboxes();
  }

  ngOnInit(): void {}

  private addChapitreCheckboxes() {
    this.chapitreList.forEach(() =>
      this.chapitres.push(new FormControl(false))
    );
  }

  get chapitres(): FormArray {
    return this.controleForm.get('chapitres') as FormArray;
  }

  onFileChange(event: any, type: string) {
    const file = event.target.files[0];
    if (file && file.type === 'application/pdf') {
      if (type === 'enonce') {
        this.enonceFile = file;
      } else if (type === 'solution') {
        this.solutionFile = file;
      }
    } else {
      Swal.fire({
        title: 'Warning',
        text: 'veuiller selectionner un fichier pdf ',
        icon: 'warning',
      });
    }
  }

  onExamFinalChange(event: any) {
    this.chapitres.controls.forEach((control) =>
      control.setValue(event.target.checked)
    );
  }

  onSubmit() {
    if (this.controleForm.valid) {
      const formData = new FormData();
      formData.append(
        'nomControle',
        this.controleForm.get('nomControle')?.value
      );
      if (this.enonceFile) {
        formData.append('enonce', this.enonceFile);
      }
      if (this.solutionFile) {
        formData.append('solution', this.solutionFile);
      }
      formData.append('examFinal', this.controleForm.get('examFinal')?.value);
      const chapitresSelected = this.chapitreList.filter(
        (chapitre, i) => this.chapitres.at(i).value
      );
      formData.append('chapitres', JSON.stringify(chapitresSelected));

      formData.forEach((value, key) => {
        console.log(`${key}: ${value}`);
      });
    }
  }
}

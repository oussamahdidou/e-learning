import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, FormArray } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';
import { DashboardService } from '../../services/dashboard.service';

@Component({
  selector: 'app-createcontrole',
  templateUrl: './createcontrole.component.html',
  styleUrls: ['./createcontrole.component.css'],
})
export class CreatecontroleComponent implements OnInit {
  controleForm: FormGroup;
  moduleId: number = 0;
  chapitreList: any[] = [];
  enonceFile: File | null = null;
  solutionFile: File | null = null;

  constructor(
    private fb: FormBuilder,
    private readonly route: ActivatedRoute,
    private readonly dashboardservice: DashboardService
  ) {
    this.controleForm = this.fb.group({
      nomControle: [''],
      enonce: [null],
      solution: [null],
      examFinal: [false],
      chapitres: this.fb.array([]),
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.moduleId = params['id'];
      this.dashboardservice
        .GetChaptersForControleByModule(this.moduleId)
        .subscribe(
          (reponse) => {
            this.chapitreList = reponse;
            console.log(reponse);
            this.addChapitreCheckboxes();
          },
          (error) => {}
        );
    });
  }

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
        text: 'Veuillez sélectionner un fichier PDF',
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
      formData.append('Nom', this.controleForm.get('nomControle')?.value);
      if (this.enonceFile) {
        formData.append('Ennonce', this.enonceFile);
      }
      if (this.solutionFile) {
        formData.append('Solution', this.solutionFile);
      }

      const chapitresSelected = this.chapitreList
        .filter((_, i) => this.chapitres.at(i).value)
        .map((chapitre) => chapitre.id);
      chapitresSelected.forEach((chapter: number) => {
        formData.append('Chapters', chapter.toString());
      });
      this.dashboardservice.CreateControle(formData).subscribe(
        (response) => {
          console.log(response);
          window.location.href = `/dashboard/module/${this.moduleId}`;
        },
        (error) => {
          console.log(error);
        }
      );
      formData.forEach((value, key) => {
        console.log(`${key}: ${value}`);
      });
    }
  }
}

import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { InstitutionService } from './../../services/institution.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-objets-pedagogiques',
  templateUrl: './objets-pedagogiques.component.html',
  styleUrl: './objets-pedagogiques.component.css',
})
export class ObjetsPedagogiquesComponent implements OnInit {
  objetsPedagogiques: any[] = [];
  niveauScolaireId!: number;

  constructor(
    private route: ActivatedRoute,
    private objetsPedagogiquesService: InstitutionService
  ) {}

  ngOnInit(): void {
    this.niveauScolaireId = this.route.snapshot.params['id'];
    this.getObjetsPedagogiques(this.niveauScolaireId);
  }

  getObjetsPedagogiques(id: number): void {
    this.objetsPedagogiquesService.getObjetsPedagogiques(id).subscribe(
      (response) => {
        this.objetsPedagogiques = response;
      },
      (error) => {
        console.error('Error fetching pedagogical objects', error);
      }
    );
  }
}

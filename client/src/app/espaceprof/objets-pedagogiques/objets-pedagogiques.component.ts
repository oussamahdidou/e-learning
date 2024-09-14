import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { InstitutionService } from './../../services/institution.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

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
    private objetsPedagogiquesService: InstitutionService,
    public authservice: AuthService
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
  Delete(id: number) {
    this.objetsPedagogiquesService.DeleteElementPedagogique(id).subscribe(
      (response) => {
        this.objetsPedagogiques = this.objetsPedagogiques.filter(
          (item) => item.id !== id
        );
      },
      (error) => {}
    );
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { InstitutionService } from './../../services/institution.service';

@Component({
  selector: 'app-niveauscolaires',
  templateUrl: './niveauscolaires.component.html',
  styleUrls: ['./niveauscolaires.component.css'],
})
export class NiveauScolairesComponent implements OnInit {
  institution: any = null;

  constructor(
    private route: ActivatedRoute,
    private institutionService: InstitutionService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      const id = +params['id'];
      this.loadInstitution(id);
    });
  }

  loadInstitution(id: number): void {
    this.institutionService.getNiveauScolaire(id).subscribe(
      (data) => {
        this.institution = data;
      },
      (error) => {
        console.error("Erreur lors du chargement de l'institution", error);
      }
    );
  }
}

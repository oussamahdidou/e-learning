import { Component, OnInit } from '@angular/core';
import { InstitutionService } from './../../services/institution.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-institutions',
  templateUrl: './institutions.component.html',
  styleUrl: './institutions.component.css',
})
export class InstitutionsComponent implements OnInit {
  institutions: any[] = [];
  selectedNiveauScolaire: any = null;

  constructor(private institutionService: InstitutionService) {}

  ngOnInit(): void {
    this.institutionService.getInstitutions().subscribe(
      (data) => {
        this.institutions = data;
        console.log(data);
      },
      (error) => {
        console.error('Erreur lors du chargement des institutions', error);
      }
    );
  }
}

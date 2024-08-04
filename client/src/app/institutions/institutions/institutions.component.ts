import { Component, Input, OnInit } from '@angular/core';
import { InstitutionService } from './../../services/institution.service';
import { Router } from '@angular/router';
import { IsEligible } from '../../interfaces/dashboard';
import { SharedDataService } from '../../services/shared-data.service';

@Component({
  selector: 'app-institutions',
  templateUrl: './institutions.component.html',
  styleUrl: './institutions.component.css',
})
export class InstitutionsComponent implements OnInit {
  institutions: any[] = [];
  selectedNiveauScolaire: any = null;
  data: IsEligible | null = null;

  constructor(
    private institutionService: InstitutionService,
    private sharedDataService: SharedDataService
  ) {}

  ngOnInit(): void {
    this.loadInstitutions();
    this.sharedDataService.data$.subscribe((data) => {
      this.data = data;
      // if (this.data != null) this.sharedDataService.resetData();
    });
  }

  loadInstitutions(): void {
    this.institutionService.getInstitutions().subscribe(
      (data) => {
        this.institutions = data;
      },
      (error) => {
        console.error('Erreur lors du chargement des institutions', error);
      }
    );
  }
}

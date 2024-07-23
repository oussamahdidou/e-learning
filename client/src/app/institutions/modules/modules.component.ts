import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { InstitutionService } from './../../services/institution.service';

@Component({
  selector: 'app-modules',
  templateUrl: './modules.component.html',
  styleUrls: ['./modules.component.css']
})
export class ModulesComponent implements OnInit {
  niveauScolaire: any = null;

  constructor(
    private route: ActivatedRoute,
    private institutionService: InstitutionService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = +params['id'];
      this.loadNiveauScolaire(id);
    });
  }

  loadNiveauScolaire(id: number): void {
    this.institutionService.getModules(id).subscribe(
      (data) => {
        this.niveauScolaire = data;
      },
      (error) => {
        console.error('Erreur lors du chargement du niveau scolaire et des modules', error);
      }
    );
  }
}
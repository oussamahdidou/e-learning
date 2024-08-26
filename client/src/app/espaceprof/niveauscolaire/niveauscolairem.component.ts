import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { InstitutionService } from './../../services/institution.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ObjetsPedagogiquesComponent } from '../objets-pedagogiques/objets-pedagogiques.component';

@Component({
  selector: 'app-niveauscolairem',
  templateUrl: './niveauscolairem.component.html',
  styleUrl: './niveauscolairem.component.css'
})
export class NiveauscolairemComponent implements OnInit {
  institution: any = null;
  dialog: any;

  constructor(
   
    private route: ActivatedRoute,
    private institutionService: InstitutionService,
    private http: HttpClient
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
        console.error('Erreur lors du chargement de l\'institution', error);
      }
    );
  }

  /////////////////////

  showObjetsPedagogiques(item: any) {
    if (item && item.id) {
      this.http.get<any[]>(`${environment.apiUrl}/api/ElementPedagogique/${item.id}`).subscribe(
        (response) => {
          if (response && response.length > 0) {
            this.openObjetsPedagogiquesDialog(response);
          } else {
            console.error('Aucun élément pédagogique trouvé');
            // Optionnel : Afficher un message à l'utilisateur
          }
        },
        (error) => {
          console.error('Erreur lors de la récupération des éléments pédagogiques', error);
          // Optionnel : Afficher un message d'erreur à l'utilisateur
        }
      );
    } else {
      console.error('ID du niveau scolaire non disponible', item);
    }
  }

  private openObjetsPedagogiquesDialog(objets: any[]) {
    const dialogRef = this.dialog.open(ObjetsPedagogiquesComponent, {
      width: '500px',
      data: { objets: objets }
    });

    dialogRef.afterClosed().subscribe((result: { lien: string; nom: string; }) => {
      if (result) {
        this.startDownload(result.lien, result.nom);
      }
    });
  }

  private startDownload(url: string, fileName: string) {
    const link = document.createElement('a');
    link.href = url;
    link.target = '_blank';
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }
}
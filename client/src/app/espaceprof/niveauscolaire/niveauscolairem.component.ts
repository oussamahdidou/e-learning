import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { InstitutionService } from './../../services/institution.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ObjetsPedagogiquesComponent } from '../objets-pedagogiques/objets-pedagogiques.component';
import { AuthService } from '../../services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { CreateobjetdialogComponent } from '../createobjetdialog/createobjetdialog.component';
import { EspaceprofService } from '../../services/espaceprof.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-niveauscolairem',
  templateUrl: './niveauscolairem.component.html',
  styleUrl: './niveauscolairem.component.css',
})
export class NiveauscolairemComponent implements OnInit {
  institution: any = null;

  constructor(
    private route: ActivatedRoute,
    private institutionService: InstitutionService,
    private http: HttpClient,
    public authservice: AuthService,
    public dialog: MatDialog,
    private readonly espaceprofservice: EspaceprofService
  ) {}

  openModal(id: number): void {
    const dialogRef = this.dialog.open(CreateobjetdialogComponent, {
      width: '700px',
      data: { id },
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      if (result) {
        console.log('Modal result:', result);
        if (result) {
          const formdata = new FormData();
          formdata.append('Nom', result.textInput);
          formdata.append('Lien', result.file);
          formdata.append('NiveauScolaireId', result.id);
          this.espaceprofservice.createobjet(formdata).subscribe(
            (response) => {
              // Close the loading modal
              Swal.close();

              // Show success message
              Swal.fire({
                icon: 'success',
                title: 'Success!',
                text: 'Your object has been created successfully.',
                confirmButtonText: 'OK',
              });
            },
            (error) => {
              // Close the loading modal
              Swal.close();

              // Show error message
              Swal.fire({
                icon: 'error',
                title: 'Error!',
                text: 'There was an error creating your object. Please try again.',
                confirmButtonText: 'OK',
              });
            }
          );

          // Show loading modal
          Swal.fire({
            title: 'Loading...',
            text: 'Please wait while your object is being created.',
            allowOutsideClick: false,
            didOpen: () => {
              Swal.showLoading();
            },
          });
        }
      }
    });
  }

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

  /////////////////////

  showObjetsPedagogiques(item: any) {
    if (item && item.id) {
      this.http
        .get<any[]>(`${environment.apiUrl}/api/ElementPedagogique/${item.id}`)
        .subscribe(
          (response) => {
            if (response && response.length > 0) {
              this.openObjetsPedagogiquesDialog(response);
            } else {
              console.error('Aucun élément pédagogique trouvé');
              // Optionnel : Afficher un message à l'utilisateur
            }
          },
          (error) => {
            console.error(
              'Erreur lors de la récupération des éléments pédagogiques',
              error
            );
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
      data: { objets: objets },
    });

    dialogRef
      .afterClosed()
      .subscribe((result: { lien: string; nom: string }) => {
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

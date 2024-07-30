import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../../services/dashboard.service';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../../../environments/environment';
import Swal from 'sweetalert2';
import { MatDialog } from '@angular/material/dialog';
import { UpdateControleChaptersDialogComponent } from '../update-controle-chapters-dialog/update-controle-chapters-dialog.component';

@Component({
  selector: 'app-controle',
  templateUrl: './controle.component.html',
  styleUrl: './controle.component.css',
})
export class ControleComponent implements OnInit {
  SelectSolution(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('Solution', file);
      formData.append('Id', this.controleid.toString());
      this.dashboardservice.updatecontroleSolution(formData).subscribe(
        (response) => {
          this.controle.solution = response.solution;
        },
        (error) => {}
      );
    }
  }
  SelectEnnonce(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('Ennonce', file);
      formData.append('Id', this.controleid.toString());
      this.dashboardservice.updatecontroleEnnonce(formData).subscribe(
        (response) => {
          this.controle.ennonce = response.ennonce;
        },
        (error) => {}
      );
    }
  }
  modifierNom() {
    Swal.fire({
      title: 'Edit Controle  Name',
      input: 'text',
      inputLabel: 'Controle Name',
      inputValue: this.controle.nom,
      showCancelButton: true,
      confirmButtonText: 'Save',
      cancelButtonText: 'Cancel',
      preConfirm: (newName) => {
        if (!newName) {
          Swal.showValidationMessage('Please enter a valid name');
        }
        return newName;
      },
    }).then((result) => {
      if (result.isConfirmed) {
        this.dashboardservice
          .updateinstitution(result.value, this.controleid)
          .subscribe(
            (response) => {
              this.controle.nom = response.nom;
              console.log(response);
              Swal.fire(
                'Saved!',
                'Institution name has been updated.',
                'success'
              );
            },
            (error) => {}
          );
      }
    });
  }

  controleid: number = 0;
  host = environment.apiUrl;
  controle: any;
  constructor(
    public dialog: MatDialog,
    private readonly dashboardservice: DashboardService,
    private readonly route: ActivatedRoute
  ) {}
  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.controleid = params['id'];
      this.dashboardservice.getcontrolebyid(this.controleid).subscribe(
        (response) => {
          console.log(response);
          this.controle = response;
        },
        (error) => {}
      );
    });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(UpdateControleChaptersDialogComponent, {
      width: '500px',
      height: '500px',
      data: { id: this.controleid }, // Pass the ID of the page here
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed', result);
      if (result) {
        this.dashboardservice
          .updatecontrolechapitres(result, this.controleid)
          .subscribe(
            (response) => {
              console.log(response);
            },
            (error) => {}
          );
      }
    });
  }
  refuser() {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes !',
    }).then((result) => {
      if (result.isConfirmed) {
        this.dashboardservice.refusercontrole(this.controleid).subscribe(
          (response) => {
            console.log(response);
            this.controle.status = response.status;
            Swal.fire({
              title: 'Refuser!',
              text: 'Your file has been Refuser.',
              icon: 'success',
            });
          },
          (error) => {}
        );
      }
    });
  }
  approuver() {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes',
    }).then((result) => {
      if (result.isConfirmed) {
        this.dashboardservice.approuvercontrole(this.controleid).subscribe(
          (response) => {
            console.log(response);
            this.controle.status = response.status;
            Swal.fire({
              title: 'Accepter!',
              text: 'Your file has been Accepter.',
              icon: 'success',
            });
          },
          (error) => {}
        );
      }
    });
  }
}

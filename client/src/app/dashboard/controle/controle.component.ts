import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../../services/dashboard.service';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../../../environments/environment';
import Swal from 'sweetalert2';
import { MatDialog } from '@angular/material/dialog';
import { UpdateControleChaptersDialogComponent } from '../update-controle-chapters-dialog/update-controle-chapters-dialog.component';
import { AuthService } from '../../services/auth.service';

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

      // Show loading modal
      Swal.fire({
        title: 'Uploading...',
        text: 'Please wait while the solution is being uploaded.',
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });

      this.dashboardservice.updatecontroleSolution(formData).subscribe(
        (response) => {
          this.controle.solution = response.solution;
          Swal.fire({
            title: 'Uploaded!',
            text: 'The solution has been uploaded successfully.',
            icon: 'success',
          });
        },
        (error) => {
          Swal.fire('Error', `${error.error}`, 'error');
        }
      );
    }
  }

  SelectEnnonce(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('Ennonce', file);
      formData.append('Id', this.controleid.toString());

      // Show loading modal
      Swal.fire({
        title: 'Uploading...',
        text: 'Please wait while the announcement is being uploaded.',
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });

      this.dashboardservice.updatecontroleEnnonce(formData).subscribe(
        (response) => {
          this.controle.ennonce = response.ennonce;
          Swal.fire({
            title: 'Uploaded!',
            text: 'The announcement has been uploaded successfully.',
            icon: 'success',
          });
        },
        (error) => {
          Swal.fire('Error', `${error.error}`, 'error');
        }
      );
    }
  }

  modifierNom() {
    Swal.fire({
      title: 'Edit Controle Name',
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
        // Show loading modal
        Swal.fire({
          title: 'Processing...',
          text: 'Please wait while your request is being processed.',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        this.dashboardservice
          .updateinstitution(result.value, this.controleid)
          .subscribe(
            (response) => {
              this.controle.nom = response.nom;
              Swal.fire({
                title: 'Saved!',
                text: 'Institution name has been updated.',
                icon: 'success',
              });
            },
            (error) => {
              Swal.fire('Error', `${error.error}`, 'error');
            }
          );
      }
    });
  }

  controleid: number = 0;
  controle: any;
  constructor(
    public dialog: MatDialog,
    private readonly dashboardservice: DashboardService,
    private readonly route: ActivatedRoute,
    public authservice: AuthService
  ) {}
  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.controleid = params['id'];
      this.dashboardservice.getcontrolebyid(this.controleid).subscribe(
        (response) => {
          console.log(response);
          this.controle = response;
        },
        (error) => {
          Swal.fire(`error`, `${error.error}`, `error`);
        }
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
        // Show loading modal
        Swal.fire({
          title: 'Processing...',
          text: 'Please wait while the chapters are being updated.',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        this.dashboardservice
          .updatecontrolechapitres(result, this.controleid)
          .subscribe(
            (response) => {
              console.log(response);
              Swal.fire({
                title: 'Updated!',
                text: 'The chapters have been updated successfully.',
                icon: 'success',
              });
            },
            (error) => {
              Swal.fire('Error', `${error.error}`, 'error');
            }
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
      confirmButtonText: 'Yes!',
    }).then((result) => {
      if (result.isConfirmed) {
        // Show loading modal
        Swal.fire({
          title: 'Processing...',
          text: 'Please wait while we process your request.',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        this.dashboardservice.refusercontrole(this.controleid).subscribe(
          (response) => {
            this.controle.status = response.status;
            Swal.fire({
              title: 'Rejected!',
              text: 'The file has been rejected.',
              icon: 'success',
            });
          },
          (error) => {
            Swal.fire('Error', `${error.error}`, 'error');
          }
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
        // Show loading modal
        Swal.fire({
          title: 'Processing...',
          text: 'Please wait while we process your request.',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        this.dashboardservice.approuvercontrole(this.controleid).subscribe(
          (response) => {
            this.controle.status = response.status;
            Swal.fire({
              title: 'Accepted!',
              text: 'Your file has been accepted.',
              icon: 'success',
            });
          },
          (error) => {
            Swal.fire('Error', `${error.error}`, 'error');
          }
        );
      }
    });
  }
}

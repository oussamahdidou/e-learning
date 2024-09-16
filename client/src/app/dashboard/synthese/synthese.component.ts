import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';
import { AuthService } from '../../services/auth.service';
import { DashboardService } from '../../services/dashboard.service';

@Component({
  selector: 'app-synthese',
  templateUrl: './synthese.component.html',
  styleUrl: './synthese.component.css',
})
export class SyntheseComponent implements OnInit {
  refuser() {
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
          text: 'Please wait while your request is being processed.',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        this.dashboardservice.RefuserSynthese(this.syntheseId).subscribe(
          (response) => {
            this.synthese.status = response.status;
            Swal.fire({
              title: 'Accepted!',
              text: 'The chapter has been accepted.',
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
          text: 'Please wait while your request is being processed.',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        this.dashboardservice.ApprouverSynthese(this.syntheseId).subscribe(
          (response) => {
            this.synthese.status = response.status;
            Swal.fire({
              title: 'Accepted!',
              text: 'The chapter has been accepted.',
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
  constructor(
    private readonly route: ActivatedRoute,
    private readonly dashboardservice: DashboardService,
    public readonly authservice: AuthService
  ) {}
  syntheseId: number = 0;
  synthese: any;
  getViewerUrl(filePath: string): string {
    return `https://docs.google.com/viewer?url=${filePath}&embedded=true`;
  }

  getFileType(filePath: string): string {
    const extension = filePath.split('.').pop()?.toLowerCase();
    switch (extension) {
      case 'pdf':
        return 'pdf';
      case 'doc':
      case 'docx':
        return 'word';
      case 'ppt':
      case 'pptx':
        return 'powerpoint';
      case 'jpg':
      case 'jpeg':
      case 'png':
      case 'gif':
        return 'image';
      default:
        return 'unknown';
    }
  }
  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.syntheseId = params['id'];
      this.dashboardservice.getsynthese(this.syntheseId).subscribe(
        (response) => {
          this.synthese = response;
          console.log(this.synthese);
        },
        (error) => {}
      );
    });
  }
  Selectsynthese(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('File', file);
      formData.append('Id', this.syntheseId.toString());

      // Show loading modal
      Swal.fire({
        title: 'Uploading...',
        text: 'Please wait while the file is being uploaded.',
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });

      this.dashboardservice.updatechapitreSynthese(formData).subscribe(
        (response) => {
          // Close the loading modal and show success message
          Swal.fire('Success', 'File uploaded successfully', 'success');
          this.synthese = response;
        },
        (error) => {
          // Close the loading modal and show error message
          Swal.fire('Error', `${error.error}`, 'error');
        }
      );
    }
  }
  ModifierNom() {
    Swal.fire({
      title: 'Enter a new name',
      input: 'text',
      inputValue: this.synthese.nom,
      showCancelButton: true,
      confirmButtonText: 'Save',
      cancelButtonText: 'Cancel',
      inputValidator: (value) => {
        if (!value) {
          return 'You need to write something!';
        }
        return null;
      },
    }).then((result) => {
      if (result.isConfirmed) {
        this.dashboardservice
          .updateSyntheseName(this.synthese.id, result.value)
          .subscribe(
            (response) => {
              this.synthese.nom = result.value;
              Swal.fire(
                'Updated!',
                `The name has been changed to: ${this.synthese.nom}`,
                'success'
              );
            },
            (error) => {}
          );
      }
    });
  }
}

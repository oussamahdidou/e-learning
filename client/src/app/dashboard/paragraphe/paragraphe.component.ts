import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DashboardService } from '../../services/dashboard.service';
import { AuthService } from '../../services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-paragraphe',
  templateUrl: './paragraphe.component.html',
  styleUrl: './paragraphe.component.css',
})
export class ParagrapheComponent {
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

        this.dashboardservice.RefuserParagraphe(this.paragrapheid).subscribe(
          (response) => {
            this.paragraphe.status = response.status;
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

        this.dashboardservice.ApprouverParagraphe(this.paragrapheid).subscribe(
          (response) => {
            this.paragraphe.status = response.status;
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
  getViewerUrl(filePath: string): string {
    return `https://docs.google.com/viewer?url=${filePath}&embedded=true`;
  }
  getpptviewrUrl(filePath: string): string {
    return `https://view.officeapps.live.com/op/embed.aspx?src=${filePath}`;
  }
  SelectParagraphe(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('File', file);
      formData.append('Id', this.paragrapheid.toString());

      // Show loading modal
      Swal.fire({
        title: 'Uploading...',
        text: 'Please wait while the file is being uploaded.',
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });

      this.dashboardservice.updateparagraphe(formData).subscribe(
        (response) => {
          // Close the loading modal and show success message
          Swal.fire('Success', 'File uploaded successfully', 'success');
          this.paragraphe.contenu = response.contenu;
        },
        (error) => {
          // Close the loading modal and show error message
          Swal.fire('Error', `${error.error}`, 'error');
        }
      );
    }
  }
  paragrapheid!: number;
  paragraphe: any;
  constructor(
    private readonly route: ActivatedRoute,
    private readonly dashboardservice: DashboardService,
    public readonly authservice: AuthService
  ) {
    this.route.params.subscribe((params) => {
      this.paragrapheid = params['id'];
      dashboardservice.getparagraphebyid(this.paragrapheid).subscribe(
        (response) => {
          this.paragraphe = response;
          console.log(this.paragraphe);
        },
        (error) => {}
      );
    });
  }

  ModifierNom() {
    Swal.fire({
      title: 'Enter a new name',
      input: 'text',
      inputValue: this.paragraphe.nom,
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
          .updateParagrapheName(this.paragraphe.id, result.value)
          .subscribe(
            (response) => {
              this.paragraphe.nom = result.value;
              Swal.fire(
                'Updated!',
                `The name has been changed to: ${this.paragraphe.nom}`,
                'success'
              );
            },
            (error) => {}
          );
      }
    });
  }
}

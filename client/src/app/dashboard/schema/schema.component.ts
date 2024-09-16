import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { DashboardService } from '../../services/dashboard.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-schema',
  templateUrl: './schema.component.html',
  styleUrl: './schema.component.css',
})
export class SchemaComponent implements OnInit {
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

        this.dashboardservice.RefuserSchema(this.schemaId).subscribe(
          (response) => {
            this.schema.status = response.status;
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

        this.dashboardservice.ApprouverSchema(this.schemaId).subscribe(
          (response) => {
            this.schema.status = response.status;
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
  schemaId: number = 0;
  schema: any;
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
      this.schemaId = params['id'];
      this.dashboardservice.getschema(this.schemaId).subscribe(
        (response) => {
          this.schema = response;
          console.log(this.schema);
        },
        (error) => {}
      );
    });
  }
  SelectSchema(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('File', file);
      formData.append('Id', this.schemaId.toString());

      // Show loading modal
      Swal.fire({
        title: 'Uploading...',
        text: 'Please wait while the file is being uploaded.',
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });

      this.dashboardservice.updatechapitreSchema(formData).subscribe(
        (response) => {
          // Close the loading modal and show success message
          Swal.fire('Success', 'File uploaded successfully', 'success');
          this.schema = response;
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
      inputValue: this.schema.nom,
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
          .updateSchemaName(this.schema.id, result.value)
          .subscribe(
            (response) => {
              this.schema.nom = result.value;
              Swal.fire(
                'Updated!',
                `The name has been changed to: ${this.schema.nom}`,
                'success'
              );
            },
            (error) => {}
          );
      }
    });
  }
}

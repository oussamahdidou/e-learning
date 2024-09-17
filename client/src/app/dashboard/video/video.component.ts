import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DashboardService } from '../../services/dashboard.service';
import Swal from 'sweetalert2';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-video',
  templateUrl: './video.component.html',
  styleUrl: './video.component.css',
})
export class VideoComponent implements OnInit {
  modifierNumero() {
    Swal.fire({
      title: 'Edit video Numero',
      input: 'number',
      inputLabel: 'video Numero',
      inputValue: this.video.objetNumber,
      showCancelButton: true,
      confirmButtonText: 'Save',
      cancelButtonText: 'Cancel',
      preConfirm: (newName) => {
        if (!newName) {
          Swal.showValidationMessage('Please enter a valid Numero');
        }
        return newName;
      },
    }).then((result) => {
      if (result.isConfirmed) {
        // Show loading modal
        Swal.fire({
          title: 'Saving...',
          text: 'Please wait while the numero is being updated.',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        this.dashboardservice
          .updateVideoNumero(this.videoId, result.value)
          .subscribe(
            (response) => {
              this.video.objetNumber = response.objetNumber;
              Swal.fire('Saved!', 'video numero has been updated.', 'success');
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

        this.dashboardservice.RefuserVideo(this.videoId).subscribe(
          (response) => {
            this.video.status = response.status;
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

        this.dashboardservice.ApprouverVideo(this.videoId).subscribe(
          (response) => {
            this.video.status = response.status;
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
  videoId: number = 0;
  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.videoId = params['id'];
      this.dashboardservice.getvideo(this.videoId).subscribe(
        (response) => {
          this.video = response;
          console.log(this.video);

          this.convertLink();
        },
        (error) => {}
      );
    });
  }
  isYoutubeLink: boolean = false;
  video: any;
  convertLink() {
    if (
      this.video.link.includes('youtube.com/watch?v=') ||
      this.video.link.includes('youtu.be/')
    ) {
      this.isYoutubeLink = true;
      if (this.video.link.includes('youtube.com/watch?v=')) {
        const videoId = this.video.link.split('v=')[1].split('&')[0]; // Extract video ID
        this.video.link = `https://www.youtube.com/embed/${videoId}`;
      } else if (this.video.link.includes('youtu.be/')) {
        const videoId = this.video.link.split('youtu.be/')[1];
        this.video.link = `https://www.youtube.com/embed/${videoId}`;
      }
    } else {
      this.isYoutubeLink = false;
    }
  }
  SelectVideo(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('File', file);
      formData.append('Id', this.videoId.toString());

      // Show loading modal
      Swal.fire({
        title: 'Uploading...',
        text: 'Please wait while the video is being uploaded.',
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });

      this.dashboardservice.updatechapitreVideo(formData).subscribe(
        (response) => {
          // Close the loading modal and show success message
          Swal.fire('Success', 'Video uploaded successfully', 'success');
          this.video.link = response.link;
          this.convertLink(); // Update the video path with the new URL`
        },
        (error) => {
          console.log(error);
          Swal.fire('Error', `${error.error}`, 'error');
        }
      );
    }
  }
  videoUrl!: string;
  updateVideoWithLink() {
    if (this.videoUrl) {
      // Show loading modal
      Swal.fire({
        title: 'Updating...',
        text: 'Please wait while the video URL is being updated.',
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });

      this.dashboardservice
        .updatechapitreVideoWithLink(this.videoId, this.videoUrl)
        .subscribe(
          (response) => {
            // Close the loading modal and show success message
            Swal.fire('Success', 'Video updated successfully', 'success');
            this.video.link = response.link;
            this.convertLink(); // Update the video path with the new URL
          },
          (error) => {
            // Close the loading modal and show error message
            Swal.fire('Error', `${error.error}`, 'error');
          }
        );
    } else {
      Swal.fire('Error', 'Please provide a valid video URL', 'error');
    }
  }
  ModifierNom() {
    Swal.fire({
      title: 'Enter a new name',
      input: 'text',
      inputValue: this.video.nom,
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
          .updateVideoName(this.video.id, result.value)
          .subscribe(
            (response) => {
              this.video.nom = result.value;
              Swal.fire(
                'Updated!',
                `The name has been changed to: ${this.video.nom}`,
                'success'
              );
            },
            (error) => {}
          );
      }
    });
  }
}

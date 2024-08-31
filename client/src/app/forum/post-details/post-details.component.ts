import { Component, OnInit } from '@angular/core';

import { InstitutionService } from '../../services/institution.service';

interface Poste {
  id: number;
  titre: string;
  content: string;
  image?: string;
  fichier?: string;
  createdAt: Date;
  appUserId?: string;
  comments: Comment[];
}

interface Comment {
  Titre: string;
}
import { ForumServiceService } from '../../services/forum-service.service';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-post-details',
  templateUrl: './post-details.component.html',
  styleUrl: './post-details.component.css',
})
export class PostDetailsComponent implements OnInit {
  editcomment(comment: any) {
    Swal.fire({
      title: 'Edit comment Name',
      input: 'textarea',
      inputLabel: 'comment Name',
      inputValue: comment.titre,
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
        this.forumservice.updatecomment(comment.id, result.value).subscribe(
          (response) => {
            comment.titre = response.titre;
            console.log(response);
            Swal.fire('Saved!', 'comment name has been updated.', 'success');
          },
          (error) => {
            console.log(error);

            Swal.fire(`error`, `${error.error}`, `error`);
          }
        );
      }
    });
  }
  deletecomment(id: number) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.isConfirmed) {
        this.forumservice.deletecomment(id).subscribe(
          (response) => {
            Swal.fire({
              title: 'Deleted!',
              text: 'Your file has been deleted.',
              icon: 'success',
            });
            this.comments = this.comments.filter(
              (comment: any) => comment.id !== id
            );
          },
          (error) => {
            console.log(error);

            Swal.fire({
              title: 'Deleted!',
              text: error.error,
              icon: 'error',
            });
          }
        );
      }
    });
  }
  id: any;
  AddComment() {
    this.authservice.$isloggedin.subscribe(($isloggedin) => {
      if ($isloggedin) {
        this.forumservice.AddComment(this.posteid, this.comment).subscribe(
          (response) => {
            this.comments.unshift(response);
            this.comment = '';
          },
          (error) => {}
        );
      } else {
        this.comment = '';
        Swal.fire({
          title: 'Please login',
          text: 'You need to be logged',
          timer: 3000,
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Login',
        }).then((result) => {
          if (result.isConfirmed) {
            window.location.href = '/auth/login';
          }
        });
      }
    });
  }
  comment: string = '';
  constructor(
    private readonly forumservice: ForumServiceService,
    private readonly route: ActivatedRoute,
    public readonly authservice: AuthService
  ) {}
  selectedImage: File | null = null;
  selectedFile: File | null = null;
  titre: string = '';
  content: string = '';
  posteid!: number;
  poste: any;
  page: number = 1;
  comments: any = [];
  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.posteid = params['id'];
      this.forumservice.GetpostById(this.posteid).subscribe(
        (response) => {
          this.poste = response;
          console.log(this.poste);

          this.titre = this.poste.titre;
          this.content = this.poste.content;
        },
        (error) => {}
      );
      this.forumservice.GetPostComments(this.posteid, this.page).subscribe(
        (response) => {
          this.comments = response;
          this.page++;
        },
        (error) => {}
      );
    });
  }
  onScroll() {
    this.forumservice.GetPostComments(this.posteid, this.page).subscribe(
      (response) => {
        const newItems = response;
        this.comments = [...this.comments, ...newItems];
        this.page++;
        console.log(this.comments);
      },
      (error) => {}
    );
  }
  onImageSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length) {
      this.selectedImage = input.files[0];
      this.poste.image = input.files[0];
    }
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length) {
      this.selectedFile = input.files[0];
      this.poste.file = input.files[0];
    }
  }
  updatePost(): void {
    if (this.titre && this.content) {
      const formData = new FormData();
      formData.append('Id', this.poste.id);
      formData.append('Titre', this.titre);
      formData.append('Content', this.content);
      if (this.selectedImage) formData.append('Image', this.selectedImage);
      if (this.selectedFile) formData.append('Fichier', this.selectedFile);

      formData.append('AppUserId', this.authservice.token.unique_name);

      // Show loading modal
      Swal.fire({
        title: 'Updating Post...',
        text: 'Please wait while the post is being updated.',
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });

      this.forumservice.Update(formData).subscribe(
        (response) => {
          console.log(formData);
          console.log('Post Updated successfully:', response);
          Swal.fire({
            title: 'Success',
            text: `Post updated successfully.`,
            icon: 'success',
          }).then(() => {
            window.location.href = `/forum/posts`;
          });
        },
        (error) => {
          console.log(formData);
          console.error('Post update failed:', error.message);
          Swal.fire({
            title: 'Error',
            text: 'Post update failed. Please try again.',
            icon: 'error',
          });
        }
      );
    }
  }
  Supprimer(id: number) {
    Swal.fire({
      title: 'Vous êtes sûr?',
      text: 'Votre poste sera supprimé',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Oui',
      cancelButtonText: 'Non',
    }).then((result) => {
      if (result.isConfirmed) {
        // Affiche la modal de chargement
        Swal.fire({
          title: 'Suppression...',
          text: 'Veuillez patienter svp.',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        this.forumservice.Delete(id).subscribe(
          (response) => {
            Swal.fire({
              title: 'Succès',
              text: 'Votre poste a été supprimé',
              icon: 'success',
            }).then(() => {
              window.location.href = `/forum/posts`;
            });
          },
          (error) => {
            console.error('Erreur de suppression:', error.message);
            Swal.fire({
              title: 'Erreur',
              text: 'La suppression a échoué. Veuillez réessayer.',
              icon: 'error',
            });
          }
        );
      }
    });
  }
}

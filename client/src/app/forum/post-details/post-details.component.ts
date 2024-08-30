import { Component, OnInit } from '@angular/core';
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
  constructor(
    private readonly forumservice: ForumServiceService,
    private readonly route: ActivatedRoute,
    private readonly authservice: AuthService
  ) {}
  selectedImage: File | null = null;
  selectedFile: File | null = null;
  titre: string='';
  content: string='';
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
          
          this.titre=this.poste.titre;
          this.content=this.poste.content;
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
      if(this.selectedImage)
        formData.append('Image', this.selectedImage);
      if(this.selectedFile)
        formData.append('Fichier', this.selectedFile);
      
      formData.append('AppUserId', this.authservice.token.unique_name);
      this.forumservice.Update(formData).subscribe(
        (response) => {
          console.log(formData);
          console.log('Post Updated successful:', response);
          Swal.fire({
            title: 'Success',
            text: `Post Updated successful `,
            icon: 'success',
          }).then(() => {
            window.location.href = `/forum/posts/`;
          });
        },
        (error) => {
          console.log(formData);
          console.error('Post Updated failed:', error.message);
          Swal.fire({
            title: 'Error',
            text:  'Post updated failed. Please try again.' ,
            icon: 'error',
          });
        }
      );
    }
  }
  Supprimer(id: number){
    Swal.fire({
      title: 'Vous etes sure?',
      text: "Votre poste sera supprimer",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Oui',
      cancelButtonText: 'Non',
    }).then((result) => {

      if (result.isConfirmed) {
        Swal.fire({
          title: 'Suppression...',
          text: 'Attendez svp.',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });
      
        this.forumservice.Delete(id).subscribe(
          (response) => {
            Swal.fire({
              title: 'Success',
              text: 'Votre poste a ete supprimer',
              icon: 'success',
            }).then(() => {
              window.location.href = `/forum/posts`;
            });
          },
          (error) => {}
        )
      }
    })
  }
}

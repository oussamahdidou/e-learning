import { Component, OnInit } from '@angular/core';
import { ForumServiceService } from '../../services/forum-service.service';
import { AuthService } from '../../services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-post-create',
  templateUrl: './post-create.component.html',
  styleUrl: './post-create.component.css'
})

export class PostCreateComponent implements OnInit{
  // posts: Post[] = [];

  titre: string='';
  content: string='';
  posteid!: number;
  postes: any;
  page: number = 1;
  comments: any = [];
  selectedImage: File | null = null;
  selectedFile: File | null = null;
  // userId: string = 'user-id'; // Replace this with the actual user ID
  
  constructor(private readonly forumservice: ForumServiceService,private readonly authservice: AuthService) {
    
  }
  // userName: string= this.authservice.token.unique_name;
  
  ngOnInit(): void {
    
    this.loadPosts();
  }

  loadPosts(): void {
    this.forumservice.GetUserPosts().subscribe(
      (response) => {
        this.postes = response.reverse();
      },
      (error) => {}
    );
  }
  onImageSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length) {
      this.selectedImage = input.files[0];
    }
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length) {
      this.selectedFile = input.files[0];
    }
  }
  addPost(): void {
    if (this.titre && this.content) {
      const formData = new FormData();
      formData.append('Titre', this.titre);
      formData.append('Content', this.content);
      if(this.selectedImage)
        formData.append('Image', this.selectedImage);
      if(this.selectedFile)
        formData.append('Fichier', this.selectedFile);
      
      formData.append('AppUserId', this.authservice.token.unique_name);
      this.forumservice.Add(formData).subscribe(
        (response) => {
          console.log(formData);
          console.log('Post added successful:', response);
          Swal.fire({
            title: 'Success',
            text: `Post added successful `,
            icon: 'success',
          }).then(() => {
            window.location.href = `/forum/create`;
          });
        },
        (error) => {
          console.log(formData);
          console.error('Post added failed:', error.message);
          Swal.fire({
            title: 'Error',
            text:  'Post added failed. Please try again.' ,
            icon: 'error',
          });
        }
      );
    }
    // if (this.newPost.titre && this.newPost.content) {
    //   this.forumservice.addPost(this.newPost).subscribe(post => {
    //     this.posts.push(post);
    //     this.newPost = { id: 0, titre: '', content: '', createdAt: new Date() }; // Reset the form
    //   });
    // }
  }
}



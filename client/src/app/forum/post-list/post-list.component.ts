import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ForumServiceService } from '../../services/forum-service.service';
import { AuthService } from '../../services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrl: './post-list.component.css',
})
export class PostListComponent implements OnInit {
  queryParams: any;
  sortby!: string;
  query!: string;
  searchinput!: string;
  selectedImage!: File;
  selectedFile!: File;
  titre: any;
  content: any;
  constructor(
    private route: ActivatedRoute,
    private readonly router: Router,
    private readonly forumservice: ForumServiceService,
    public authservice: AuthService
  ) {}
  page: number = 1;
  posts: any[] = [];
  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.loading = true;

      this.queryParams = params;
      this.searchinput = this.queryParams.query;
      if (params) {
        this.forumservice
          .GetAllPosts(
            this.searchinput,
            this.page,
            this.queryParams.sortby || 'recent'
          )
          .subscribe(
            (response) => {
              this.posts = response;
              console.log(this.posts);
              this.page++;
              this.loading = false;
            },
            (error) => {
              this.loading = false;
            }
          );
      }
    });
  }
  description: string = `Passionate and versatile Fullstack Software Engineer with a knack for crafting efficient, scalable, and user-centric applications. With a strong background in Business Intelligence, I bring data to life, transforming raw information into actionable insights. My expertise in web design ensures that every project not only functions seamlessly but also captivates with visually compelling and intuitive interfaces. Dedicated to continuous learning and innovation, I thrive on challenges and consistently strive to push the boundaries of technology, delivering solutions that exceed expectations.`;
  search() {
    if (this.query && this.query.length > 3) {
      this.router.navigate(['/forum/posts'], {
        queryParams: { query: this.query },
      });
      window.location.href = `/forum/posts?query=${this.query}`;
    }
  }
  onSortChange(sortValue: string) {
    this.sortby = sortValue; // Update the selected value
    console.log('SortBy changed to:', this.sortby); // Debugging line

    // Redirect with the selected sort value
    // this.router.navigate(['/forum/posts'], {
    //   queryParams: { sortby: this.sortby, query: this.searchinput || '' },
    // });
    window.location.href = `/forum/posts?sortby=${this.sortby}&query=${
      this.searchinput || ''
    }`;
  }
  loading = false;
  skeletonTheme = {
    height: '50px',
    borderRadius: '4px',
    width: '100%',
    animation: 'pulse',
  };

  onScroll() {
    if (this.loading) return; // Prevent multiple requests
    this.loading = true;
    this.forumservice
      .GetAllPosts(
        this.searchinput,
        this.page,
        this.queryParams.sortby || 'recent'
      )
      .subscribe(
        (response) => {
          const newItems = response;
          this.posts = [...this.posts, ...newItems];
          this.page++;
          console.log(this.posts);
          this.loading = false;
        },
        (error) => {
          this.loading = false;
        }
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
      if (this.selectedImage) formData.append('Image', this.selectedImage);
      if (this.selectedFile) formData.append('Fichier', this.selectedFile);

      formData.append('AppUserId', this.authservice.token.unique_name);

      // Show loading modal
      Swal.fire({
        title: 'Processing...',
        text: 'Please wait while your post is being added.',
        icon: 'info',
        allowOutsideClick: false,
        showConfirmButton: false,
        didOpen: () => {
          Swal.showLoading();
        },
      });

      this.forumservice.Add(formData).subscribe(
        (response) => {
          console.log(formData);
          console.log('Post added successful:', response);

          // Update modal to show success message
          Swal.fire({
            title: 'Success',
            text: 'Post added successfully!',
            icon: 'success',
            confirmButtonText: 'OK',
          }).then(() => {
            window.location.href = `/forum/create`;
          });
        },
        (error) => {
          console.log(formData);
          console.error('Post added failed:', error.message);

          // Update modal to show error message
          Swal.fire({
            title: 'Error',
            text: 'Post added failed. Please try again.',
            icon: 'error',
            confirmButtonText: 'OK',
          });
        }
      );
    }
  }
}

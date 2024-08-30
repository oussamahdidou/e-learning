import { Component, OnInit } from '@angular/core';
import { ForumServiceService } from '../../services/forum-service.service';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-post-details',
  templateUrl: './post-details.component.html',
  styleUrl: './post-details.component.css',
})
export class PostDetailsComponent implements OnInit {
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
    private readonly authservice: AuthService
  ) {}
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
}

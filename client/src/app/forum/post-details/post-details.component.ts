import { Component, OnInit } from '@angular/core';
import { ForumServiceService } from '../../services/forum-service.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-post-details',
  templateUrl: './post-details.component.html',
  styleUrl: './post-details.component.css',
})
export class PostDetailsComponent implements OnInit {
  constructor(
    private readonly forumservice: ForumServiceService,
    private readonly route: ActivatedRoute
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

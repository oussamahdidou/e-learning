import { Component, OnInit } from '@angular/core';
import { InstitutionService } from '../../services/institution.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ForumServiceService } from '../../services/forum-service.service';

@Component({
  selector: 'app-userpost',
  templateUrl: './userpost.component.html',
  styleUrl: './userpost.component.css',
})
export class UserpostComponent implements OnInit {
  onScroll() {
    this.forumservice.GetUserPosts(this.page).subscribe(
      (response) => {
        const newItems = response;
        this.userPosts = [...this.userPosts, ...newItems];
        this.page++;
      },
      (error) => {
        console.error('Error fetching user posts:', error);
      }
    );
  }
  queryParams: any;
  sortby!: string;
  query!: string;
  searchinput!: string;

  userPosts: any[] = [];
  page: number = 1;

  constructor(
    private route: ActivatedRoute,
    private readonly router: Router,
    private readonly forumservice: ForumServiceService
  ) {}

  ngOnInit(): void {
    this.loadUserPosts();
  }

  loadUserPosts() {
    this.forumservice.GetUserPosts(this.page).subscribe(
      (response) => {
        this.userPosts = response;
        console.log('User posts:', this.userPosts);
        this.page++;
      },
      (error) => {
        console.error('Error fetching user posts:', error);
      }
    );
  }
}

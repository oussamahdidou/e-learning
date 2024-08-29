import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ForumServiceService } from '../../services/forum-service.service';

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
  constructor(
    private route: ActivatedRoute,
    private readonly router: Router,
    private readonly forumservice: ForumServiceService
  ) {}
  page: number = 1;
  posts: any[] = [];
  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
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
            },
            (error) => {}
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
  onScroll() {
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
        },
        (error) => {}
      );
  }
}

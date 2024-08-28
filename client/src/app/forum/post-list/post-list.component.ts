import { Component, OnInit } from '@angular/core';

interface Post {
  id: number;
  title: string;
  type: string;
}

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrl: './post-list.component.css'
})
export class PostListComponent implements OnInit {

  searchQuery: string = '';
  selectedType: string = 'All';

  mockPosts: Post[] = [
    { id: 1, title: 'commet utilisee le plan comptable', type: 'comptabilite' },
    { id: 2, title: 'commet chercher dans le plan comptable', type: 'comptabilite' },
    { id: 3, title: 'commet chercher dans le plan comptable', type: 'plus recent' }
  ];

  filteredPosts: Post[] = [];

  ngOnInit() {
    this.filterPosts();
  }

  onSearch() {
    this.filterPosts(); 
  }

  filterPosts() {
    this.filteredPosts = this.mockPosts.filter(post =>
      post.title.toLowerCase().includes(this.searchQuery.toLowerCase()) &&
      (this.selectedType === 'All' || post.type === this.selectedType)
    );
  }
}

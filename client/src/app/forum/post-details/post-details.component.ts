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
 Titre:  string ;
}

@Component({
  selector: 'app-post-details',
  templateUrl: './post-details.component.html',
  styleUrls: ['./post-details.component.css']
})
export class PostDetailsComponent implements OnInit {
  posts: Poste[] = [];
  error: string = '';
  loading: boolean = false;
  constructor( private institutionService: InstitutionService) { }

  ngOnInit() {
    this.loadUserPosts();
  }

  loadUserPosts() {
    this.loading = true;
    this.institutionService.getUserPosts().subscribe({
      next: (response: Poste[]) => {
        this.posts = response;
        this.loading = false;
      },
      error: (err) => {
        this.error = "Erreur lors du chargement des posts : " + err.message;
        this.loading = false;
      }
    });
  }

  getFormattedDate(date: Date): string {
    return new Date(date).toLocaleDateString('fr-MA', {
      year: 'numeric',
    month: 'long',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit'
    });
  }
}
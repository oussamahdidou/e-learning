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
  styleUrl: './post-details.component.css',
})
export class PostDetailsComponent {}

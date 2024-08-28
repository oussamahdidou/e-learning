import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PostListComponent } from './post-list/post-list.component';
import { PostCreateComponent } from './post-create/post-create.component';
import { PostDetailsComponent } from './post-details/post-details.component';
import { MatIconModule } from '@angular/material/icon'; 

import { NavbarComponent } from './navbar/navbar.component';


const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        component: PostListComponent,
        title: 'PostList',
      },
      {
        path: 'posts/:id',
        component: PostDetailsComponent,
        title: 'PostDetails',
      },
      {
        path: 'create',
        component: PostCreateComponent,
        title: 'createpost',
      },
    ],
  },
];

@NgModule({
  declarations: [
    PostListComponent,
    PostCreateComponent,
    PostDetailsComponent,
    NavbarComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    MatIconModule,
    RouterModule.forChild(routes)
  ],
  //////
  exports: [
    NavbarComponent,
   
  ]
})
export class ForumModule { }
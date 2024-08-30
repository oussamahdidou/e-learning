import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PostListComponent } from './post-list/post-list.component';
import { PostCreateComponent } from './post-create/post-create.component';
import { PostDetailsComponent } from './post-details/post-details.component';
import { MatIconModule } from '@angular/material/icon';

import { NavbarComponent } from './navbar/navbar.component';
import { TruncatePipe } from '../pipes/truncate.pipe';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { UserpostComponent } from './userpost/userpost.component';
import { InfiniteScrollDirective } from 'ngx-infinite-scroll';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '', // Define the route for PostListComponent
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
      {
        path: 'user',
        component: UserpostComponent,
        title: 'userpost',
      },
    ],
  },
];

@NgModule({
  declarations: [
    PostListComponent,
    PostCreateComponent,
    PostDetailsComponent,
    NavbarComponent,
    UserpostComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    InfiniteScrollDirective,
    MatIconModule,
    RouterModule.forChild(routes),
    TruncatePipe,
    MatInputModule,
    MatSelectModule,
    MatOptionModule,
  ],
  //////
  exports: [NavbarComponent],
})
export class ForumModule {}

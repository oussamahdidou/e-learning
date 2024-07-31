import { NgModule } from '@angular/core';
import { ExtraOptions, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'institutions',
    loadChildren: () =>
      import('./institutions/institutions.module').then(
        (m) => m.InstitutionsModule
      ),
  },
  {
    path: 'course',
    loadChildren: () =>
      import('./course/course.module').then((m) => m.CourseModule),
  },
  {
    path: 'dashboard',
    canActivate:[AdminGuardService],
    loadChildren: () =>
      import('./dashboard/dashboard.module').then((m) => m.DashboardModule),
  },

  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path:'profile',
    loadChildren:() =>
       import('./profile/profile.module').then((m)=> m.ProfileModule),
  },
  {
    path: '**',
    component: HomeComponent,
    redirectTo: '',
  },
];

import { HomeComponent } from './home/home.component';
import { AuthService } from './services/auth.service';
import { AdminGuardService } from './services/admin-guard.service';

const routerOptions: ExtraOptions = {
  anchorScrolling: 'enabled', // Enable anchor scrolling
  scrollPositionRestoration: 'enabled', // Optional: Restores scroll position on navigation
};
@NgModule({
  imports: [RouterModule.forRoot(routes, routerOptions)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

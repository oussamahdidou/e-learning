import { Injectable } from '@angular/core';
import {
  Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from '@angular/router';
import { combineLatest, map } from 'rxjs';
import Swal from 'sweetalert2';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class GranteddashboardService {
  constructor(private _router: Router, private authService: AuthService) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return combineLatest([
      this.authService.$IsTeacher,
      this.authService.$IsAdmin,
      this.authService.$IsGranted,
    ]).pipe(
      map(([isTeacher, isAdmin, isGranted]) => {
        if ((isTeacher || isAdmin) && isGranted) {
          return true;
        } else {
          Swal.fire({
            title: 'UnAuthorized',
            text: `Attend que tu soit approuver par l'admin`,
            icon: 'error',
          });
          this._router.navigate(['/']);
          return false;
        }
      })
    );
  }
}

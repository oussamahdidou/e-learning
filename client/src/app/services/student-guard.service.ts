import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import {
  ActivatedRouteSnapshot,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})
export class StudentGuardService {
  constructor(private _router: Router, private authService: AuthService) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    this.authService.$IsStudent.subscribe(($IsStudent) => {
      if ($IsStudent) {
        return true;
      } else {
        Swal.fire({
          title: 'UnAuthorized',
          text: `You should be a Student`,
          icon: 'error',
        });
        this._router.navigate(['/auth/login']);
        return false;
      }
    });
  }
}

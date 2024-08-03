import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './services/auth.service';
import Swal from 'sweetalert2';
import { combineLatest, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DashboardGuardService {

  constructor(private _router:Router,private authService: AuthService) { }
  canActivate(route:ActivatedRouteSnapshot,state:RouterStateSnapshot){
   
    return combineLatest([
      this.authService.$IsTeacher,
      this.authService.$IsAdmin
    ]).pipe(
      map(([isTeacher, isAdmin])  => {
      if (isTeacher || isAdmin) {
        return true;
      } else {
        Swal.fire({
          title: 'UnAuthorized',
          text: `You should be a Teacher or Admin`,
          icon: 'error',
        });
        this._router.navigate(["/"]);
      return false;
      }
    }));
  }
}

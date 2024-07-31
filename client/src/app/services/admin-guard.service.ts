import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AdminGuardService {

  constructor(private _router:Router,private authService: AuthService) { }

  canActivate(route:ActivatedRouteSnapshot,state:RouterStateSnapshot){
   
    this.authService.$IsAdmin.subscribe(isAdmin => {
      if (isAdmin) {
        return true;
      } else {
        Swal.fire({
          title: 'UnAuthorized',
          text: `You should be an Admin`,
          icon: 'error',
        });
        this._router.navigate(["/"]);
      return false;
      }
    });
  }
}

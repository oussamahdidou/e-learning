import { Injectable } from '@angular/core';
import { AuthService } from './services/auth.service';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService {

  constructor(private _router:Router,private authService: AuthService) { }
  canActivate(route:ActivatedRouteSnapshot,state:RouterStateSnapshot){
    this.authService.$isloggedin.subscribe(isLoggedin => {
      if (isLoggedin) {
        return true;
      } else {
        Swal.fire({
          title: 'UnAuthorized',
          text: `You should logged in first`,
          icon: 'error',
        });
        this._router.navigate(["/auth/login"]);
      return false;
      }
    });
    
  }
}

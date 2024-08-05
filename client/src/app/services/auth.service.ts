import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private jwtHelper = new JwtHelperService();
  private _$isLoggedin = new BehaviorSubject(false);
  $isloggedin = this._$isLoggedin.asObservable();
  private _$IsAdmin = new BehaviorSubject(false);
  $IsAdmin = this._$IsAdmin.asObservable();
  private _$IsTeacher = new BehaviorSubject(false);
  $IsTeacher = this._$IsTeacher.asObservable();
  private _$IsStudent = new BehaviorSubject(false);
  $IsStudent = this._$IsStudent.asObservable();
  jwt: string = '';
  token: any;
  headers: any | undefined;

  constructor(private http: HttpClient, private router: Router) {
    if (
      localStorage.getItem('token') &&
      !this.jwtHelper.isTokenExpired(localStorage.getItem('token'))
    ) {
      this._$isLoggedin.next(true);
      this.jwt = localStorage.getItem('token') || '';
      this.token = this.getUser(this.jwt);
      console.log(this.token);
      this.updateRoleStates(this.token.role);
      this.headers = new HttpHeaders().set(
        'Authorization',
        'Bearer ' + this.jwt
      );
    } else {
      this.resetRoleStates();
    }
  }

  getUser(token: string) {
    return this.jwtHelper.decodeToken(token);
  }

  logout() {
    localStorage.removeItem('token');
    this.resetRoleStates();
  }

  login(username: string, password: string): Observable<any> {
    return this.http
      .post(`${environment.apiUrl}/api/Account/Login`, { username, password })
      .pipe(
        tap<any>(
          (response) => {
            localStorage.setItem('token', response['token']);
            this._$isLoggedin.next(true);
            const userRole = this.getUser(response['token']).role;
            this.updateRoleStates(userRole);
            this.redirectUser(userRole);
          },
          (error) => {
            console.log(error);
          }
        )
      );
  }

  private updateRoleStates(role: string) {
    this._$IsAdmin.next(role === 'Admin');
    this._$IsTeacher.next(role === 'Teacher');
    this._$IsStudent.next(role === 'Student');
  }

  private resetRoleStates() {
    this._$isLoggedin.next(false);
    this._$IsAdmin.next(false);
    this._$IsTeacher.next(false);
    this._$IsStudent.next(false);
  }

  private redirectUser(role: string) {
    if (role === 'Admin') {
      this.router.navigate(['/dashboard']);
    } else if (role === 'Teacher') {
      this.router.navigate(['/dashboard']);
    } else if (role === 'Student') {
      this.router.navigate(['/institutions']);
    }
  }

  registeruser(
    userName: string,
    email: string,
    password: string,
    confirmPassword: string
  ): Observable<any> {
    return this.http
      .post(`${environment.apiUrl}/api/Account/Register`, {
        userName,
        email,
        password,
        confirmPassword,
      })
      .pipe(
        tap<any>(
          (response) => {},
          (error) => {
            console.log('error : 1111111' + error);
          }
        )
      );
  }

  forgotPassword(email: string): Observable<any> {
    return this.http
      .post(`${environment.apiUrl}/api/Account/forgotpassword`, {
        email,
      })
      .pipe(
        tap<any>(
          (response) => {},
          (error) => {
            console.log(error);
          }
        )
      );
  }

  resetPassword(
    password: string,
    confirmpassword: string,
    email: string,
    token: string
  ): Observable<any> {
    return this.http
      .post(`${environment.apiUrl}/api/Account/resetpassword`, {
        password,
        confirmpassword,
        email,
        token,
      })
      .pipe(
        tap<any>(
          (response) => {},
          (error) => {
            console.log(error);
          }
        )
      );
  }

  verifyEmail(email: string, token: string): Observable<any> {
    const params = new HttpParams().set('email', email).set('token', token);
    return this.http
      .get(`${environment.apiUrl}/api/Account/emailconfirmation`, {
        params,
      })
      .pipe(
        tap<any>(
          (response) => {},
          (error) => {
            console.log(error);
          }
        )
      );
  }
}

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from '../../environments/environment';
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
  jwt: string = '';
  token: any;
  headers: any | undefined;
  constructor(private http: HttpClient) {
    if (
      localStorage.getItem('token') &&
      !this.jwtHelper.isTokenExpired(localStorage.getItem('token'))
    ) {
      this._$isLoggedin.next(true);
      this.jwt = localStorage.getItem('token') || '';
      this.token = this.getUser(this.jwt);
      console.log(this.token);
      if (this.token && this.token.role === 'Admin') {
        this._$IsAdmin.next(true);
        this._$IsTeacher.next(false);
      } else if (this.token && this.token.role === 'Teacher') {
        this._$IsTeacher.next(true);
        this._$IsAdmin.next(false);
      }
      this.headers = new HttpHeaders().set(
        'Authorization',
        'Bearer ' + this.jwt
      );
    } else {
      this._$isLoggedin.next(false);
      this._$IsTeacher.next(false);
      this._$IsAdmin.next(false);
    }
  }
  getUser(token: string) {
    return this.jwtHelper.decodeToken(token);
  }
  logout() {
    localStorage.removeItem('token');
    this._$isLoggedin.next(false);
    this._$IsAdmin.next(false);
  }
  login(username: string, password: string): Observable<any> {
    return this.http
      .post(`${environment.apiUrl}/Account/Login`, { username, password })
      .pipe(
        tap<any>(
          (response) => {
            localStorage.setItem('token', response['token']);
            this._$isLoggedin.next(true);
            if (this.getUser(response['token']).role === 'Admin') {
              this._$IsAdmin.next(true);
              this._$IsTeacher.next(false);
            } else if (this.getUser(response['token']).role === 'Teacher') {
              this._$IsTeacher.next(true);
              this._$IsAdmin.next(false);
            }
          },
          (error) => {
            console.log(error);
          }
        )
      );
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

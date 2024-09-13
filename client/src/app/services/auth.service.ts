import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
  HttpParams,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
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
  private _$IsGranted = new BehaviorSubject(false);
  $IsGranted = this._$IsGranted.asObservable();
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
      
      this._$IsGranted.next(this.token.Granted);
      console.log("Is USer Granted : " +this._$IsGranted.value);
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
      window.location.href = `/dashboard`;
    } else if (role === 'Teacher') {
      this.jwt = localStorage.getItem('token') || '';
      this.token = this.getUser(this.jwt);
      window.location.href = `/dashboard/profile/${this.token.unique_name}`;
    } else if (role === 'Student') {
      window.location.href = `/institutions`;
    }
  }

  registeruser(
    Nom: string,
    Prenom: string,
    DateDeNaissance: Date,
    Etablissement: string,
    Branche: string,
    Niveaus: string,
    userName: string,
    email: string,
    tuteuremail: string,
    phonenumber: string,
    password: string,
    confirmPassword: string
  ): Observable<any> {
    return this.http
      .post(`${environment.apiUrl}/api/Account/Register`, {
        Nom,
        Prenom,
        DateDeNaissance,
        Etablissement,
        Branche,
        Niveaus,
        userName,
        email,
        tuteuremail,
        phonenumber,
        password,
        confirmPassword,
      })
      .pipe(
        tap<any>(
          (response) => {},
          (error) => {
            console.log('error : 1111111' + error.error);
          }
        )
      );
  }

  teacherregisteruser(formData: FormData): Observable<any> {
    return this.http
      .post(`${environment.apiUrl}/api/Account/TeacherRegister`, formData, {})
      .pipe(
        // catchError(this.handleError)
        tap<any>(
          (response) => {},
          (error) => {
            formData.forEach((element) => {
              console.log('data : ' + element);
            });

            console.log('error : 1111111' + error.message);
            console.log('error : 2222222' + error.name);
            console.log('error : 3333333' + error.stack);
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

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred!';

    if (error.error instanceof ErrorEvent) {
      // Client-side or network error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Backend returned an unsuccessful response code
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
      if (error.error && error.error.Message) {
        errorMessage = error.error.Message; // Custom error message from backend
      }
    }
    console.log('error :::' + errorMessage);
    return throwError(() => new Error(errorMessage));
  }
  deleteuser(id: string): Observable<any> {
    return this.http.delete(
      `${environment.apiUrl}/api/Account/deleteuser/${id}`
    );
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(private http: HttpClient, private router:Router) { }

  getLearning(): Observable<any>{
    const token = localStorage.getItem('token');
    return this.http
    .get<any>(`${environment.apiUrl}/api/UserCenter`,{
      headers:{
        Authorization: `Bearer ${token}`,
      }
    })
    .pipe(
      tap((response) => {
        return response;
      })
    );
  }
}

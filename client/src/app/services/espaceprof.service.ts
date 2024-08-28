import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class EspaceprofService {
  constructor(
    private http: HttpClient,
    private readonly authservice: AuthService
  ) {}

  getInstitutions(): Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiUrl}/api/Institution`);
  }

  //niveaux scolaire
  getNiveauScolaire(id: number): Observable<any> {
    return this.http.get<any>(`${environment.apiUrl}/api/Institution/${id}`);
  }

  getModules(id: number): Observable<any> {
    return this.http.get<any>(
      `${environment.apiUrl}/api/ElementPedagogique/${id}`
    );
  }

  createobjet(form: any): Observable<any> {
    return this.http.post(
      `${environment.apiUrl}/api/ElementPedagogique`,
      form,
      {
        headers: this.authservice.headers,
      }
    );
  }
}

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';

export interface Post {
  id: number;
  titre: string;
  content: string;
  image: string;
  fichier: string;
  createdAt: string;
  appUserId: string;
  comments: any[]; // You might want to create a more specific type for comments
}

@Injectable({
  providedIn: 'root',
})
export class InstitutionService {
  constructor(private http: HttpClient,private authService: AuthService) {}

  getInstitutions(): Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiUrl}/api/Institution`);
  }

  //niveaux scolaire
  getNiveauScolaire(id: number): Observable<any> {
    return this.http.get<any>(`${environment.apiUrl}/api/Institution/${id}`);
  }

  getModules(id: number): Observable<any> {
    return this.http.get<any>(`${environment.apiUrl}/api/NiveauScolaire/${id}`);
  }

  getObjetsPedagogiques(id: number): Observable<any> {
    console.log(
      `Calling API: ${environment.apiUrl}/api/ElementPedagogique/${id}`
    );
    return this.http.get<any>(
      `${environment.apiUrl}/api/ElementPedagogique/${id}`
    );
  }

  createElementPedagogique(elementDto: any): Observable<any> {
    return this.http.post<any>(
      `${environment.apiUrl}/api/ElementPedagogique`,
      elementDto
    );
  }
}
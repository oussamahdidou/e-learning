import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class InstitutionService {
  constructor(private http: HttpClient) {}

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
  ////////////////


  getObjetsPedagogiques(id: number): Observable<any> {
    console.log(`Calling API: ${environment.apiUrl}/api/ElementPedagogique/${id}`);
    return this.http.get<any>(`${environment.apiUrl}/api/ElementPedagogique/${id}`);
  }
  
  
    createElementPedagogique(elementDto: any): Observable<any> {
      return this.http.post<any>(`${environment.apiUrl}/api/ElementPedagogique`, elementDto);
    }
}

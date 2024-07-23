import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class DashboardService {
  constructor(private readonly http: HttpClient) {}
  createinstitution(name: string): Observable<any> {
    return this.http.post(`${environment.apiUrl}/Institution`, {
      name: name,
    });
  }
  getinstitutions(): Observable<any> {
    return this.http.get(`${environment.apiUrl}/Institution`);
  }
  updateinstitution(name: string, id: number): Observable<any> {
    return this.http.put(`${environment.apiUrl}/Institution`, {
      id: id,
      nom: name,
    });
  }
  getniveauscolaires(id: number): Observable<any> {
    return this.http.get(`${environment.apiUrl}/Institution/${id}`);
  }
  createniveauscolaire(name: string, id: number): Observable<any> {
    return this.http.post(`${environment.apiUrl}/NiveauScolaire`, {
      nom: name,
      institutionId: id,
    });
  }
  updateniveauscolaire(name: string, id: number): Observable<any> {
    return this.http.put(`${environment.apiUrl}/NiveauScolaire`, {
      niveauScolaireId: id,
      nom: name,
    });
  }
  getModules(id: number): Observable<any> {
    return this.http.get(`${environment.apiUrl}/NiveauScolaire/${id}`);
  }
  createModule(name: string, id: number): Observable<any> {
    return this.http.post(`${environment.apiUrl}/Module`, {
      nom: name,
      niveauScolaireId: id,
    });
  }
  updateModule(name: string, id: number): Observable<any> {
    return this.http.put(`${environment.apiUrl}/Module`, {
      nom: name,
      moduleId: id,
    });
  }
  createchapter(chapter: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}/Chapitre`, chapter);
  }
  createquiz(quiz: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}/Quiz/Create`, quiz);
  }
}

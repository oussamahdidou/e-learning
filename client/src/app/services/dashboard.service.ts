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
    return this.http.post(`${environment.apiUrl}/api/Institution`, {
      name: name,
    });
  }
  getinstitutions(): Observable<any> {
    return this.http.get(`${environment.apiUrl}/api/Institution`);
  }
  updateinstitution(name: string, id: number): Observable<any> {
    return this.http.put(`${environment.apiUrl}/api/Institution`, {
      id: id,
      nom: name,
    });
  }
  getniveauscolaires(id: number): Observable<any> {
    return this.http.get(`${environment.apiUrl}/api/Institution/${id}`);
  }
  createniveauscolaire(name: string, id: number): Observable<any> {
    return this.http.post(`${environment.apiUrl}/api/NiveauScolaire`, {
      nom: name,
      institutionId: id,
    });
  }
  updateniveauscolaire(name: string, id: number): Observable<any> {
    return this.http.put(`${environment.apiUrl}/api/NiveauScolaire`, {
      niveauScolaireId: id,
      nom: name,
    });
  }
  getModules(id: number): Observable<any> {
    return this.http.get(`${environment.apiUrl}/api/NiveauScolaire/${id}`);
  }
  createModule(name: string, id: number): Observable<any> {
    return this.http.post(`${environment.apiUrl}/api/Module`, {
      nom: name,
      niveauScolaireId: id,
    });
  }
  updateModule(name: string, id: number): Observable<any> {
    return this.http.put(`${environment.apiUrl}/api/Module`, {
      nom: name,
      moduleId: id,
    });
  }
  createchapter(chapter: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}/api/Chapitre`, chapter);
  }
  createquiz(quiz: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}/api/Quiz/Create`, quiz);
  }
  getrequiredmodules(id: number): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/ModuleRequirement/RequiredModules/${id}`
    );
  }
  createmodulerequirements(module: any): Observable<any> {
    return this.http.post(
      `${environment.apiUrl}/api/ModuleRequirement`,
      module
    );
  }
}

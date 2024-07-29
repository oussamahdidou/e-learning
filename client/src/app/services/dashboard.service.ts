import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class DashboardService {
  constructor(
    private readonly http: HttpClient,
    private readonly authservice: AuthService
  ) {}
  createinstitution(name: string): Observable<any> {
    return this.http.post(
      `${environment.apiUrl}/api/Institution`,
      {
        name: name,
      },
      { headers: this.authservice.headers }
    );
  }
  getinstitutions(): Observable<any> {
    return this.http.get(`${environment.apiUrl}/api/Institution`, {
      headers: this.authservice.headers,
    });
  }
  updateinstitution(name: string, id: number): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Institution`,
      {
        id: id,
        nom: name,
      },
      { headers: this.authservice.headers }
    );
  }
  getniveauscolaires(id: number): Observable<any> {
    return this.http.get(`${environment.apiUrl}/api/Institution/${id}`, {
      headers: this.authservice.headers,
    });
  }
  createniveauscolaire(name: string, id: number): Observable<any> {
    return this.http.post(
      `${environment.apiUrl}/api/NiveauScolaire`,
      {
        nom: name,
        institutionId: id,
      },
      { headers: this.authservice.headers }
    );
  }
  updateniveauscolaire(name: string, id: number): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/NiveauScolaire`,
      {
        niveauScolaireId: id,
        nom: name,
      },
      { headers: this.authservice.headers }
    );
  }
  getModules(id: number): Observable<any> {
    return this.http.get(`${environment.apiUrl}/api/NiveauScolaire/${id}`, {
      headers: this.authservice.headers,
    });
  }
  createModule(name: string, id: number): Observable<any> {
    return this.http.post(
      `${environment.apiUrl}/api/Module`,
      {
        nom: name,
        niveauScolaireId: id,
      },
      { headers: this.authservice.headers }
    );
  }
  updateModule(name: string, id: number): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Module`,
      {
        nom: name,
        moduleId: id,
      },
      { headers: this.authservice.headers }
    );
  }
  createchapter(chapter: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}/api/Chapitre`, chapter, {
      headers: this.authservice.headers,
    });
  }
  createquiz(quiz: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}/api/Quiz/Create`, quiz, {
      headers: this.authservice.headers,
    });
  }
  getrequiredmodules(id: number): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/ModuleRequirement/RequiredModules/${id}`,
      { headers: this.authservice.headers }
    );
  }
  createmodulerequirements(module: any): Observable<any> {
    return this.http.post(
      `${environment.apiUrl}/api/ModuleRequirement`,
      module,
      { headers: this.authservice.headers }
    );
  }
  GetChaptersForControleByModule(id: number): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Dashboard/GetChaptersForControleByModule/${id}`,
      { headers: this.authservice.headers }
    );
  }
  GetDashboardChaptersByModule(id: number): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Dashboard/GetChaptersByModule/${id}`,
      { headers: this.authservice.headers }
    );
  }

  CreateControle(controle: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}/api/Controle`, controle, {
      headers: this.authservice.headers,
    });
  }
  GetChapitrebyid(id: number): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Dashboard/DashboardChapter/${id}`,
      { headers: this.authservice.headers }
    );
  }
  updatequiz(id: number, quiz: any): Observable<any> {
    return this.http.put(`${environment.apiUrl}/api/Quiz/Update/${id}`, quiz, {
      headers: this.authservice.headers,
    });
  }
  getcontrolesbymodule(id: number): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Controle/GetControleByModule/${id}`,
      { headers: this.authservice.headers }
    );
  }
  getteachers(): Observable<any> {
    return this.http.get(`${environment.apiUrl}/api/Dashboard/Teachers`, {
      headers: this.authservice.headers,
    });
  }
  grantaccess(id: string): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}
/api/Dashboard/Grant/${id}`,
      {},
      { headers: this.authservice.headers }
    );
  }
  removeaccessgrant(id: string): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}
/api/Dashboard/RemoveGrant/${id}`,
      {},
      { headers: this.authservice.headers }
    );
  }
  getobjectspourapprouver(): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/
api/Dashboard/GetObjectspourApprouver`,
      { headers: this.authservice.headers }
    );
  }
  getcontrolebyid(id: number): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Controle/GetControleById/${id}`,
      { headers: this.authservice.headers }
    );
  }
  updatecontrolename(name: string, id: number): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Controle/UpdateControleName`,
      {
        id: id,
        name: name,
      },
      { headers: this.authservice.headers }
    );
  }
  updatecontroleEnnonce(ennonce: any): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Controle/UpdateControleEnnonce`,
      ennonce,
      { headers: this.authservice.headers }
    );
  }
  updatecontroleSolution(solution: any): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Controle/UpdateControleSolution`,
      solution,
      { headers: this.authservice.headers }
    );
  }
  getchapitrestoupdatecontroles(id: number): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Dashboard/GetChapitresToUpdateControles/${id}`,

      { headers: this.authservice.headers }
    );
  }
  updatecontrolechapitres(chapitres: any[], id: number): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Dashboard/Updatecontrolechapters/${id}`,
      chapitres,
      { headers: this.authservice.headers }
    );
  }
}

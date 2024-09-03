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
  //********************************************** controle ************************* */
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
  approuvercontrole(id: number): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Controle/Approuver/${id}`,
      {}
    );
  }
  refusercontrole(id: number): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Controle/Refuser/${id}`,
      {}
    );
  }
  //**************************************** chapitre ********************************** */
  updatechapitrePdf(Pdf: any): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Chapitre/UpdateChapitrePdf`,
      Pdf,
      { headers: this.authservice.headers }
    );
  }
  updatechapitreVideo(video: any): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Chapitre/UpdateChapitreVideo`,
      video,
      { headers: this.authservice.headers }
    );
  }
  updatechapitreSchema(schema: any): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Chapitre/UpdateChapitreSchema`,
      schema,
      { headers: this.authservice.headers }
    );
  }
  updatechapitreSynthese(synthese: any): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Chapitre/UpdateChapitreSynthese`,
      synthese,
      { headers: this.authservice.headers }
    );
  }
  updatechapitrenom(value: any, chapterid: number): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Chapitre/UpdateChapterName`,
      {
        id: chapterid,
        nom: value,
      }
    );
  }
  approuverchapitre(id: number): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Chapitre/Approuver/${id}`,
      {},
      { headers: this.authservice.headers }
    );
  }
  refuserchapitre(id: number): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Chapitre/Refuser/${id}`,
      {},
      { headers: this.authservice.headers }
    );
  }
  //********************************************** exam ************************* */
  createexam(exam: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}/api/ExamFinal`, exam, {
      headers: this.authservice.headers,
    });
  }
  getexambymodule(id: number): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/ExamFinal/GetExamFinaleByModule/${id}`,
      {
        headers: this.authservice.headers,
      }
    );
  }
  updateexamennonce(exam: any): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/ExamFinal/UpdateExamEnnonce`,
      exam,
      {
        headers: this.authservice.headers,
      }
    );
  }
  updateexamsolution(exam: any): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/ExamFinal/UpdateExamSolution`,
      exam,
      {
        headers: this.authservice.headers,
      }
    );
  }
  approuverexam(id: number): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/ExamFinal/Approuver/${id}`,
      {},
      { headers: this.authservice.headers }
    );
  }
  refuserexam(id: number): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/ExamFinal/Refuser/${id}`,
      {},
      { headers: this.authservice.headers }
    );
  }
  getmostcheckedmodules(): Observable<any> {
    return this.http.get(`${environment.apiUrl}/api/Dashboard/ModulesCharts`, {
      headers: this.authservice.headers,
    });
  }
  getleastcheckedmodules(): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Dashboard/LeastModulesCharts`,
      {
        headers: this.authservice.headers,
      }
    );
  }
  gettoptestniveaumodules(): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Dashboard/TopModulesTestNiveau`,
      {
        headers: this.authservice.headers,
      }
    );
  }
  getworsttestniveaumodules(): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Dashboard/WorstModulesTestNiveau`,
      {
        headers: this.authservice.headers,
      }
    );
  }
  getstats(): Observable<any> {
    return this.http.get(`${environment.apiUrl}/api/Dashboard/GetStats`, {
      headers: this.authservice.headers,
    });
  }
  approuverquiz(id: number): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Quiz/Approuver/${id}`,
      {},
      { headers: this.authservice.headers }
    );
  }
  refuserquiz(id: number): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Quiz/Refuser/${id}`,
      {},
      { headers: this.authservice.headers }
    );
  }
  deleteinstitution(id: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/api/Institution/${id}`, {
      headers: this.authservice.headers,
    });
  }
  deleteniveauscolaire(id: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/api/NiveauScolaire/${id}`, {
      headers: this.authservice.headers,
    });
  }
  deletemodule(id: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/api/Module/${id}`, {
      headers: this.authservice.headers,
    });
  }
  deletechapitre(id: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/api/Chapitre/${id}`, {
      headers: this.authservice.headers,
    });
  }
  deleteexamfinal(id: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/api/ExamFinal/${id}`, {
      headers: this.authservice.headers,
    });
  }
  deletecontrole(id: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/api/Controle/${id}`, {
      headers: this.authservice.headers,
    });
  }
  deleterequiredmodule(
    TargetModuleId: number,
    RequiredModuleId: number
  ): Observable<any> {
    return this.http.delete(
      `${environment.apiUrl}/api/ModuleRequirement/${TargetModuleId}/${RequiredModuleId}`,
      { headers: this.authservice.headers }
    );
  }
  editrequiredmodule(
    TargetModuleId: number,
    RequiredModuleId: number,
    seuill: number
  ): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/ModuleRequirement`,
      {
        targetModuleId: TargetModuleId,
        requiredModuleId: RequiredModuleId,
        seuill: seuill,
      },
      { headers: this.authservice.headers }
    );
  }
  getmoduleinfo(id: number): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Module/GetModuleinfo/${id}`,
      {
        headers: this.authservice.headers,
      }
    );
  }
  updatemoduleimage(form: any): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Module/UpdateModuleImage`,
      form,
      {
        headers: this.authservice.headers,
      }
    );
  }
  updatemoduleprogram(form: any): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Module/UpdateModuleProgram`,
      form,
      {
        headers: this.authservice.headers,
      }
    );
  }
  updatemoduledescription(
    moduleid: number,
    description: string
  ): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Module/UpdateModuleDescription`,
      {
        moduleId: moduleid,
        description: description,
      },
      {
        headers: this.authservice.headers,
      }
    );
  }
  createparagraphe(paragraphe: any): Observable<any> {
    return this.http.post(
      `${environment.apiUrl}/api/Chapitre/CreateParagraphe`,
      paragraphe,
      {
        headers: this.authservice.headers,
      }
    );
  }
  updateparagraphe(paragraphe: any): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Chapitre/UpdateParagraphe`,
      paragraphe,
      {
        headers: this.authservice.headers,
      }
    );
  }
  getparagraphebyid(id: number): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Chapitre/Paragraphe/${id}`,
      {
        headers: this.authservice.headers,
      }
    );
  }
  getTeacherbyid(id: string): Observable<any> {
    return this.http.get(`${environment.apiUrl}/api/Dashboard/Teacher/${id}`, {
      headers: this.authservice.headers,
    });
  }
  getteacherobjects(id: string): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Dashboard/GetTeacherObjects/${id}`,
      {
        headers: this.authservice.headers,
      }
    );
  }
  gettopteachers(): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Dashboard/TopTeachersContributors`,
      {
        headers: this.authservice.headers,
      }
    );
  }
  getworstteachers(): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Dashboard/WorstTeachersContributors`,
      {
        headers: this.authservice.headers,
      }
    );
  }
  getmodulesniveauscolaires(id: number): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Module/GetModulesNiveauScolaire/${id}`,
      {
        headers: this.authservice.headers,
      }
    );
  }
  createmoduleniveauscolaire(
    moduleId: number,
    niveauScolaireId: number
  ): Observable<any> {
    return this.http.post(
      `${environment.apiUrl}/api/Module/CreateModuleNiveauScolaire`,
      {
        moduleId: moduleId,
        niveauScolaireId: niveauScolaireId,
      },
      {
        headers: this.authservice.headers,
      }
    );
  }
  deletemoduleniveauscolaire(
    moduleId: number,
    niveauScolaireId: number
  ): Observable<any> {
    return this.http.delete(
      `${environment.apiUrl}/api/Module/DeleteNiveauScolaireModule/${moduleId}/${niveauScolaireId}`,
      {
        headers: this.authservice.headers,
      }
    );
  }
  updatechapitreVideoWithLink(id: number, link: string): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Chapitre/UpdateChapitreVideoLink`,
      {
        chapitreId: id,
        link: link,
      },
      {
        headers: this.authservice.headers,
      }
    );
  }
  deleteparagraphe(id: number): Observable<any> {
    return this.http.delete(
      `${environment.apiUrl}/api/Paragraphes/${id}`,

      {
        headers: this.authservice.headers,
      }
    );
  }
  getschema(id: number): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Chapitre/Schema/${id}`,

      {
        headers: this.authservice.headers,
      }
    );
  }
  getvideo(id: number): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Chapitre/Video/${id}`,

      {
        headers: this.authservice.headers,
      }
    );
  }
  getsynthese(id: number): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Chapitre/Synthese/${id}`,

      {
        headers: this.authservice.headers,
      }
    );
  }
  addschema(form: any): Observable<any> {
    return this.http.post(
      `${environment.apiUrl}/api/Chapitre/AddSchema`,
      form,
      {
        headers: this.authservice.headers,
      }
    );
  }
  addvideo(form: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}/api/Chapitre/AddVideo`, form, {
      headers: this.authservice.headers,
    });
  }
  addvideolink(chapterid: number, videoUrl: string): Observable<any> {
    return this.http.post(
      `${environment.apiUrl}/api/Chapitre/AddVideoLink`,
      {
        chapitreId: chapterid,
        link: videoUrl,
      },
      {
        headers: this.authservice.headers,
      }
    );
  }
  addsynthese(form: any): Observable<any> {
    return this.http.post(
      `${environment.apiUrl}/api/Chapitre/AddSynthese`,
      form,
      {
        headers: this.authservice.headers,
      }
    );
  }
  deletesynthese(id: number): Observable<any> {
    return this.http.delete(
      `${environment.apiUrl}/api/Chapitre/Synthese/${id}`,
      {
        headers: this.authservice.headers,
      }
    );
  }
  deleteschema(id: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/api/Chapitre/Schema/${id}`, {
      headers: this.authservice.headers,
    });
  }
  deletevideo(id: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/api/Chapitre/Video/${id}`, {
      headers: this.authservice.headers,
    });
  }
}

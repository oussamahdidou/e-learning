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
}

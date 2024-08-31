import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ForumServiceService {
  constructor(
    private readonly authservice: AuthService,
    private readonly http: HttpClient
  ) {}
  GetAllPosts(
    query: string,
    pagenumber: number,
    sort: string
  ): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Poste/gelallposts?Query=${
        query || ''
      }&PageNumber=${pagenumber}&SortBy=${sort || 'recent'}`,
      {
        headers: this.authservice.headers,
      }
    );
  }
  /////////////////
  GetUserPosts(): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Poste/getuserposts`,
      {
        headers: this.authservice.headers,
      }
    );
}
}
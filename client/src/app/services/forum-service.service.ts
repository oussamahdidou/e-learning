import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Observable, tap } from 'rxjs';
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
  GetpostById(id: number): Observable<any> {
    return this.http.get(`${environment.apiUrl}/api/Poste/getposte/${id}`, {
      headers: this.authservice.headers,
    });
  }
  GetUserPosts(page: number): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Poste/getuserposts?Page=${page}`,
      {
        headers: this.authservice.headers,
      }
    );
  }
  GetPostComments(id: number, page: number): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/api/Comment/${id}?Page=${page}`,
      {
        headers: this.authservice.headers,
      }
    );
  }
  Delete(id: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/api/Poste/${id}`, {
      headers: this.authservice.headers,
    });
  }
  Add(formData: FormData): Observable<any> {
    return this.http
      .post(`${environment.apiUrl}/api/Poste`, formData, {
        headers: this.authservice.headers,
      })
      .pipe(
        tap<any>(
          (response) => {},
          (error) => {
            console.log('error : 1111111' + error.message);
            console.log('error : 2222222' + error.name);
            console.log('error : 3333333' + error.stack);
          }
        )
      );
  }
  Update(formData: FormData): Observable<any> {
    return this.http
      .put(`${environment.apiUrl}/api/Poste/`, formData, {
        headers: this.authservice.headers,
      })
      .pipe(
        tap<any>(
          (response) => {},
          (error) => {}
        )
      );
  }
  AddComment(id: number, content: string): Observable<any> {
    return this.http.post(
      `${environment.apiUrl}/api/Comment`,
      {
        text: content,
        posteId: id,
        userId: 'string',
      },
      {
        headers: this.authservice.headers,
      }
    );
  }
  updatecomment(id: number, text: string): Observable<any> {
    return this.http.put(
      `${environment.apiUrl}/api/Comment`,
      {
        text: text,
        commentId: id,
      },
      {
        headers: this.authservice.headers,
      }
    );
  }
  deletecomment(id: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/api/Comment/${id}`, {
      headers: this.authservice.headers,
    });
  }
}

import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SharedDataService {
  private dataSubject = new BehaviorSubject<any>(null);
  data$ = this.dataSubject.asObservable();

  private activeDivSubject = new BehaviorSubject<string>('');
  activeDiv$ = this.activeDivSubject.asObservable();

  private devoirPdfUrl = new BehaviorSubject<string>('');
  devoir$ = this.devoirPdfUrl.asObservable();

  setData(data: any) {
    this.dataSubject.next(data);
  }
  resetData() {
    this.dataSubject.next(null);
  }
  setActiveDiv(route: string) {
    this.activeDivSubject.next(route);
  }
  resetActiveDiv(route: string) {
    this.activeDivSubject.next('');
  }
  setDevoir(data: any) {
    this.devoirPdfUrl.next(data);
  }
  resetDevoir() {
    this.devoirPdfUrl.next('');
  }
}

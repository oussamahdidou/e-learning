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
}

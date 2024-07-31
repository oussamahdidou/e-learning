import { TestBed } from '@angular/core/testing';

import { TeacherGuardService } from '../services/teacher-guard.service';

describe('TeacherGuardService', () => {
  let service: TeacherGuardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TeacherGuardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

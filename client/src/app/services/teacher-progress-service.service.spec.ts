import { TestBed } from '@angular/core/testing';

import { TeacherProgressServiceService } from './teacher-progress-service.service';

describe('TeacherProgressServiceService', () => {
  let service: TeacherProgressServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TeacherProgressServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

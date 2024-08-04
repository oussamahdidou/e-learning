import { TestBed } from '@angular/core/testing';

import { StudentGuardService } from './student-guard.service';

describe('StudentGuardService', () => {
  let service: StudentGuardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StudentGuardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { GrantedstudentService } from './grantedstudent.service';

describe('GrantedstudentService', () => {
  let service: GrantedstudentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GrantedstudentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

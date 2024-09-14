import { TestBed } from '@angular/core/testing';

import { GranteddashboardService } from './granteddashboard.service';

describe('GranteddashboardService', () => {
  let service: GranteddashboardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GranteddashboardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

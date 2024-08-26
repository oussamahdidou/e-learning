import { TestBed } from '@angular/core/testing';

import { EspaceprofService } from './espaceprof.service';

describe('EspaceprofService', () => {
  let service: EspaceprofService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EspaceprofService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { SharedListingsService } from './shared-listings.service';

describe('SharedListingsService', () => {
  let service: SharedListingsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SharedListingsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

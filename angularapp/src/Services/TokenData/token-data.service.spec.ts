import { TestBed } from '@angular/core/testing';

import { TokenDataService } from './token-data.service';

describe('TokenDataService', () => {
  let service: TokenDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TokenDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { MyApiService } from './services/my-api.service';

describe('MyApiService', () => {
  let service: MyApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MyApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

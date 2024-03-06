import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApprequestsComponent } from './apprequests.component';

describe('ApprequestsComponent', () => {
  let component: ApprequestsComponent;
  let fixture: ComponentFixture<ApprequestsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ApprequestsComponent]
    });
    fixture = TestBed.createComponent(ApprequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

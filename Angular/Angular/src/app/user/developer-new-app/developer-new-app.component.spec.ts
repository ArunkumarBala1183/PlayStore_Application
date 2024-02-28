import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeveloperNewAppComponent } from './developer-new-app.component';

describe('DeveloperNewAppComponent', () => {
  let component: DeveloperNewAppComponent;
  let fixture: ComponentFixture<DeveloperNewAppComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeveloperNewAppComponent]
    });
    fixture = TestBed.createComponent(DeveloperNewAppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

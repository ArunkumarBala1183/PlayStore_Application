import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeveloperMyAppsComponent } from './developer-my-apps.component';

describe('DeveloperMyAppsComponent', () => {
  let component: DeveloperMyAppsComponent;
  let fixture: ComponentFixture<DeveloperMyAppsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeveloperMyAppsComponent]
    });
    fixture = TestBed.createComponent(DeveloperMyAppsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

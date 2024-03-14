import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppliationlogsComponent } from './appliationlogs.component';

describe('AppliationlogsComponent', () => {
  let component: AppliationlogsComponent;
  let fixture: ComponentFixture<AppliationlogsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AppliationlogsComponent]
    });
    fixture = TestBed.createComponent(AppliationlogsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

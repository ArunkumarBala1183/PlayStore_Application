import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpecificAppComponent } from './specific-app.component';

describe('SpecificAppComponent', () => {
  let component: SpecificAppComponent;
  let fixture: ComponentFixture<SpecificAppComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SpecificAppComponent]
    });
    fixture = TestBed.createComponent(SpecificAppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

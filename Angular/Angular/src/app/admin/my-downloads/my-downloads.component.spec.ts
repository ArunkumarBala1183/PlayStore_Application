import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyDownloadsComponent } from './my-downloads.component';

describe('MyDownloadsComponent', () => {
  let component: MyDownloadsComponent;
  let fixture: ComponentFixture<MyDownloadsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MyDownloadsComponent]
    });
    fixture = TestBed.createComponent(MyDownloadsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

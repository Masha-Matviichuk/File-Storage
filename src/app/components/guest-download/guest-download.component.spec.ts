import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GuestDownloadComponent } from './guest-download.component';

describe('GuestDownloadComponent', () => {
  let component: GuestDownloadComponent;
  let fixture: ComponentFixture<GuestDownloadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GuestDownloadComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GuestDownloadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

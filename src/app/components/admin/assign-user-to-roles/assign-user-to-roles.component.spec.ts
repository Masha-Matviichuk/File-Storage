import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignUserToRolesComponent } from './assign-user-to-roles.component';

describe('AssignUserToRolesComponent', () => {
  let component: AssignUserToRolesComponent;
  let fixture: ComponentFixture<AssignUserToRolesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AssignUserToRolesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AssignUserToRolesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

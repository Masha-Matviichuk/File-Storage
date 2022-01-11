import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { catchError } from 'rxjs';
import { Role } from 'src/app/models/role';
import { AdminService } from 'src/app/services/admin.service';
import { CommonService } from 'src/app/services/common.service';


@Component({
  selector: 'app-assign-user-to-roles',
  templateUrl: './assign-user-to-roles.component.html',
  styleUrls: ['./assign-user-to-roles.component.css']
})
export class AssignUserToRolesComponent implements OnInit {
  roles: Role[];
  Email: string = '';
  Role: string = '';
  selectedRole: string = '';
  error: boolean;
  

  constructor(private _commonService: CommonService,
    private router: Router,
    private _adminService: AdminService) {
    
   }

  ngOnInit(): void {
    this.getRoles();
    

  }

getRoles(){
this._commonService.getRoles().subscribe(roles => this.roles=roles);
}

chooseRole(event: any){
  this.selectedRole = event.target.id;
  console.log(this.selectedRole);
}

assignRole(email: string, role: string){
  
    this._adminService.AssignUserToRole(email, role).subscribe(() => {
    
      this.Email='';
      this.Role='';
      this.error = false;
      this.router.navigate(["/files"]);
      
  },  (error:HttpErrorResponse) => {
       this.error=true;
       this.router.navigate(["/getRoles"]);});
      
}

}


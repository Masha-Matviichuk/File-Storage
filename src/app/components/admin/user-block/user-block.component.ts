import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/services/admin.service';

@Component({
  selector: 'app-user-block',
  templateUrl: './user-block.component.html',
  styleUrls: ['./user-block.component.css']
})
export class UserBlockComponent implements OnInit {

  id: string;
  days: string;
  error: number;
  

  constructor(private router: Router,
    private _adminService: AdminService) { }

  ngOnInit(): void {
  }


  blockUser(id: string, days: string){
    this._adminService.blockUser(id, days).subscribe(() => {
      this.router.navigate(["/users"]);
    },  (error:HttpErrorResponse) => {
      this.error=error.status;
      this.router.navigate(["/users"]);});
  }
}

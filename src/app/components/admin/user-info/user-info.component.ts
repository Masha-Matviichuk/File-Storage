import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserInfo } from 'src/app/models/user-info';
import { AdminService } from 'src/app/services/admin.service';


@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.css']
})
export class UserInfoComponent implements OnInit {

  user: UserInfo;

  constructor(
    private route: ActivatedRoute,
    private _adminService: AdminService) { }

  ngOnInit(){
    this.getUserInfo();
  }

  getUserInfo(){
    
    const id = +this.route.snapshot.paramMap.get('id')!;
    this._adminService.getUserInfo(id).subscribe(user => this.user={
      id: user.id,
      firstName: user.firstName,
      lastName: user.lastName,
      email: user.email,
      phoneNumber: user.phoneNumber,
      roles: user.roles,
      isBanned: user.isBanned
    });
}
}

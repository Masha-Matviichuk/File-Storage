import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfo } from 'src/app/models/user-info';
import { AdminService } from 'src/app/services/admin.service';


@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

   users: UserInfo[];
   searchString: string;
   selectedUser: UserInfo;
   user: UserInfo;

  constructor(
    private router: Router,
    private _adminService: AdminService) { }

  ngOnInit(): void {
    this._adminService.getUsers().subscribe(users => this.users = users);
  }


  onUserSearch(id: string){
    if(id != null && id !=''){
      this.users = [];
      this._adminService.findUser(+id).subscribe(user =>
        this.users.push(user))
    }else{
      this._adminService.getUsers().subscribe(users =>this.users=users);
    }
  }

  onDeleteClick(id: number) {
    var confirmed = confirm("Are you sure that you want to delete this user?");
    if(!confirmed) return;

    this._adminService.DeleteUser(id)
    .subscribe(
      () => {
        this.router.navigate(["/users"]);}
    );
  }
}

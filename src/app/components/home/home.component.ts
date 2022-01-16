import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { CommonService } from 'src/app/services/common.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  role: boolean;

  constructor(private _authService: AuthService,
    private _commonService: CommonService,
    private router: Router) { }

  ngOnInit(): void {
    this.userIsAdmin();
  }

  userAuthorized() {
    return this._authService.UserAuthorized();
  }

  logout() {
    this._authService.RemoveLocalUserData();
    this.router.navigate(['/signin']);
  }

  userIsAdmin() {
    if (this.userAuthorized()) {
      this._commonService.GetUserRoles().subscribe(role => this.role=role.some(x => x === "admin"));
    }
  }
}

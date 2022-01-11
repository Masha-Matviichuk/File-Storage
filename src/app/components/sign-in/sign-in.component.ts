import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UserInfo } from 'src/app/models/user-info';

import { UserLogin } from 'src/app/models/user-signIn';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  user: UserLogin;
  err: boolean;
  //token: any;


  constructor( private authService: AuthService,
    private router: Router) { }

  ngOnInit() {
    this.resetForm();
  }


  resetForm(form?: NgForm) {
    if(form != null) form.reset();
    this.user = {
      Email: "",
      Password: ""
    }
  }

  onSubmit(form: NgForm) {
    this.authService.AuthenticateUser(form.value)
    .subscribe((data: any) =>{ localStorage.setItem("userToken", JSON.stringify(data).slice(1, -1));
    this.router.navigate(['/files']);
  this.err = false},
    (err: HttpErrorResponse)=>{
      this.err = true;
    });
  }
}

import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UserSignUp } from 'src/app/models/user-signUp';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  user: UserSignUp;
  err: boolean;


  constructor(private authService: AuthService,
    private router: Router) { }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form?: NgForm) {
    if(form != null) form.reset();
    this.user = {
      FirstName: "",
      LastName: "",
      Email: "",
      PhoneNumber: "",
      Password: "",
      PasswordConfirm: ""
    }
  }

  onSubmit(form: NgForm) {
    this.authService.RegisterUser(form.value)
    .subscribe((data: any) => {
      
        this.resetForm(form);
        this.err = false;
        this.router.navigate(["/signin"]);
    },
    (err: HttpErrorResponse)=>{
      this.err = true;
    });
    
  }

}

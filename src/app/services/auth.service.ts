import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { UserInfo } from '../models/user-info';
import { UserLogin } from '../models/user-signIn';
import { UserSignUp } from '../models/user-signUp';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  readonly rootUrl = 'https://localhost:7156';
  

  constructor(private http: HttpClient) { }


  UserAuthorized() : boolean {
    return localStorage.getItem("userToken") != null;
  }

  RemoveLocalUserData() : void {
    localStorage.removeItem("userToken");
    localStorage.removeItem("userId");
    localStorage.removeItem("userEmail");
  }

  SetLocalUserData(userInfo: UserInfo) {
    localStorage.setItem("userId", userInfo.id.toString());
    localStorage.setItem("userEmail", userInfo.email);
  }


  RegisterUser(user: UserSignUp) {
    const url = `${this.rootUrl}/Account/signUp`;
    const body: UserSignUp = {
      FirstName: user.FirstName,
      LastName: user.LastName,
      Email: user.Email,
      PhoneNumber: user.PhoneNumber,
      Password: user.Password,
      PasswordConfirm: user.PasswordConfirm
    };
    //console.log(body);
    return this.http.post(url, body);
  }

  AuthenticateUser(user: UserLogin) {
    const url = `${this.rootUrl}/Account/logIn`;
    const body: UserLogin = {
      Email: user.Email,
    Password: user.Password};
      
    var header = new HttpHeaders()
    .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`)
    .set("Content-Type", " application/json-patch+json")
    .set("accept", " */*");
  
    return this.http.post(url, body, { headers: header,
                                       responseType: 'text'
   });
  }
}

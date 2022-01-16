import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
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

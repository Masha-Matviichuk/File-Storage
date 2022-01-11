import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserInfo } from '../models/user-info';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  readonly rootUrl = 'https://localhost:7156';

  constructor(private _http : HttpClient) { }

  getUsers(): Observable<UserInfo[]> {
    var header = new HttpHeaders()
      .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);
  
    const url = `${this.rootUrl}/Admin/users`;
    return this._http.get<UserInfo[]>(url, { headers : header});
  }
  
  findUser(id: number) : Observable<UserInfo> {
    var header = new HttpHeaders()
      .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);
    const url = `${this.rootUrl}/Admin/users`;
    if(id != 0){
    const url = `${this.rootUrl}/Admin/users/${id}`;
    return this._http.get<UserInfo>(url, { headers : header});
    }
    return this._http.get<UserInfo>(url, { headers : header});
  }
  
  DeleteUser(id: number) {
    const url = `${this.rootUrl}/Admin/delete/${id}`;
    var header = new HttpHeaders()
    .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);
  
    return this._http.delete(url, { headers : header });
  }

  AssignUserToRole(email: string, role: string){
    const url = `${this.rootUrl}/Admin/users/assignUserToRole`;
    var header = new HttpHeaders()
      .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);
      const body = {
        "email": email,
        "roles": [role]
      };
      //console.log(body);
      return this._http.post(url, body, {headers: header});
  }
  
  getUserInfo(userId:number){
    var header = new HttpHeaders()
      .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);
    const url = `${this.rootUrl}/Admin/users/${userId}`;
    return this._http.get<UserInfo>(url, { headers : header});
  }

  blockUser(id:string, days: string){
    const url = `${this.rootUrl}/Admin/users/ban/${id}?days=${days}`;
    var header = new HttpHeaders().set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);
    
   const body = {"days": days}
    return this._http.put(url, body, { headers : header});
  }

}

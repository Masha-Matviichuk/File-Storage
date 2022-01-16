import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Access } from '../models/access';
import { FileInfo } from '../models/file-info';
import { Role } from '../models/role';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  readonly currentUrl = 'http://localhost:4200';

  readonly rootUrl = 'https://localhost:7156';

  constructor(private _http : HttpClient) { }



  FormatFileSize(sizeInBytes: number) : string {
    var i = -1;
    var byteUnits = [' KB', ' MB', ' GB', ' TB', 'PB', 'EB', 'ZB', 'YB'];
    do {
      sizeInBytes = sizeInBytes / 1024;
        i++;
    } while (sizeInBytes > 1024);

    return Math.max(sizeInBytes, 0.01).toFixed(2) + byteUnits[i];
  }

  FormatDate(dateInString: string) : string {
    return new Date(Date.parse(dateInString)).toLocaleString();
  }

  LinkForDownload(file: FileInfo): string{
    var url = `${this.currentUrl}/file/${file.id}`;
    return url;
  }

  getRoles(): Observable<Role[]> {
    const url = `${this.rootUrl}/Common/getRoles`;
    return this._http.get<Role[]>(url);
  }

  getFilesAccess(){
    const url = `${this.rootUrl}/Common/accessList`;
    return this._http.get<Access[]>(url);
  }

  GetUserRoles(): Observable<string[]> {
    var header = new HttpHeaders()
      .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);
    const url = `${this.rootUrl}/Common/getRole`;
    return this._http.get<string[]>(url, {headers: header});
  }
 
}

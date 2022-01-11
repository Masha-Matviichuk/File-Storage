import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http'
import { map, Observable } from 'rxjs';
import { FileInfo } from '../models/file-info';
import { Access } from '../models/access';



@Injectable({
  providedIn: 'root'
})
export class FileService {

   readonly rootUrl = 'https://localhost:7156';

  constructor(private _http : HttpClient) { }

  getFiles(): Observable<FileInfo[]> {
    var header = new HttpHeaders()
      .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);

      
    const url = `${this.rootUrl}/Files/userFiles`;
    return this._http.get<FileInfo[]>(url, { headers : header});
  }

  getAllFiles(): Observable<FileInfo[]> {
    var header = new HttpHeaders()
      .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);

      
    const url = `${this.rootUrl}/Files`;
    return this._http.get<FileInfo[]>(url, { headers : header});
  }

  findByKeyword(keyword: string) : Observable<FileInfo[]> {
    var header = new HttpHeaders()
      .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);
    const url = `${this.rootUrl}/Files`;
    if(keyword != null){
    const url = `${this.rootUrl}/Files/filesSearch/${keyword}`;
    return this._http.get<FileInfo[]>(url, { headers : header});
    }
    return this._http.get<FileInfo[]>(url, { headers : header});
  }

  getFileInfo(fileId:number){
    var header = new HttpHeaders()
      .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);
    const url = `${this.rootUrl}/Files/${fileId}`;
    return this._http.get<FileInfo>(url, { headers : header});
  }

    
    UploadFile(fileName: string, fileDescription: string, accessTypeId: string, fileToUpload: File){
    const url = `${this.rootUrl}/Files/upload`;
      var header = new HttpHeaders()
      .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);

      const formData: FormData = new FormData();
     
      formData.append("Title", fileName);
      formData.append("Description", fileDescription);
      formData.append("AccessId", accessTypeId);
      formData.append("uploadedFile", fileToUpload, fileToUpload.name);
     
      return this._http.post(url, formData, { headers : header,
      responseType: 'arraybuffer'});
  }

  EditFile(id: number, fileName: string, fileDescription: string, accessTypeId: string, fileToUpload: File){
    const url = `${this.rootUrl}/Files/edit/${id}`;
      var header = new HttpHeaders()
      .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);

      const formData: FormData = new FormData();
     
      formData.append("Title", fileName);
      formData.append("Description", fileDescription);
      formData.append("AccessId", accessTypeId);
      formData.append("uploadedFile", fileToUpload, fileToUpload.name);
     

      return this._http.put(url, formData, { headers : header,
      responseType: 'json'});
  }

  DeleteFile(id: number) {
    const url = `${this.rootUrl}/Files/delete/${id}`;
    var header = new HttpHeaders()
    .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);

    return this._http.delete(url, { headers : header });
  }

  DownloadFile(id: number) {
    const url = `${this.rootUrl}/Files/download/${id}`;
    var header = new HttpHeaders()
    .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);

    return this._http.get(url, { headers : header, responseType : "blob", reportProgress : true });
  }
}

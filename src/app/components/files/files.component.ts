import { Component, OnInit } from '@angular/core';
import { FileService } from '../../services/file.service';
import{FileInfo} from '../../models/file-info';
import { FileInfoComponent } from '../file-info/file-info.component';
import { Router } from '@angular/router';
import { saveAs } from 'file-saver'
import { AuthService } from 'src/app/services/auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonService } from 'src/app/services/common.service';

@Component({
  selector: 'app-files',
  templateUrl: './files.component.html',
  styleUrls: ['./files.component.css']
})
export class FilesComponent implements OnInit {

   files: FileInfo[];
   searchString: string;
   selectedFile: FileInfo;
   allFiles: FileInfo[];
   role: boolean;
   err: number;

  constructor(private _fileService: FileService,
    private _commonService: CommonService,
    private router: Router,
    private _authService: AuthService) { }

  ngOnInit() {
    this.userIsAdmin();
    
      this._fileService.getFiles().subscribe(files => this.files = files);
      this._fileService.getAllFiles().subscribe(allfiles => this.allFiles = allfiles);
        
     
    }
  

  onSearchForUser(keyword: string){
    if(keyword != null && keyword != ""){
      this._fileService.findByKeyword(keyword).subscribe(files =>this.files = files)
    }else{
      this._fileService.getFiles().subscribe(files => this.files = files);
    }

  }

  onSearchForAdmin(keyword: string){
    if(keyword != null && keyword != ""){
      this._fileService.findByKeyword(keyword).subscribe(files =>this.allFiles = files)
    }else{
      this._fileService.getAllFiles().subscribe(files => this.allFiles = files);
    }

  }

  onDeleteClick(id: number) {
    var confirmed = confirm("Are you sure to delete this file?");
    if(!confirmed) return;

    this._fileService.DeleteFile(id)
    .subscribe(
      () => {
        this.router.navigate(["/files"]);}, 
        (err: HttpErrorResponse)=>{
          this.err=err.status;
        }
    );
    this.router.navigate(["/files"]);
  }

  onDownloadClick(id: number, title: string) {
    this._fileService.DownloadFile(id)
    .subscribe(blobData => { saveAs(blobData, title)}, 
    (err: HttpErrorResponse)=>{
      this.err=err.status;
    });
  }

  userIsAdmin() {
   
      this._commonService.GetUserRoles().subscribe(role => this.role=role.some(x => x === "admin"));
    console.log(this.role)
  
  }

  logout() {
    this._authService.RemoveLocalUserData();
    this.router.navigate(['/signin']);
  }
}



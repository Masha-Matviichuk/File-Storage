import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Access } from 'src/app/models/access';
import { CommonService } from 'src/app/services/common.service';

import { FileService } from 'src/app/services/file.service';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.css']
})
export class UploadFileComponent implements OnInit {
  accessTypes: Access[];
  selectedAccessType: string ='';
  fileToUpload: File;
  title: string;
  description: string;
  err: number;
  
  constructor(private _fileService: FileService,
    private _commonService: CommonService,
    private router: Router) {
   }

  ngOnInit(): void {
    this.getAccessType();
  }

  getAccessType(){
    this._commonService.getFilesAccess().subscribe(accessTypes => this.accessTypes = accessTypes);
  }

  chooseAccessType(event: any){
    this.selectedAccessType = event.target.id;
  }

  handleFileInput(file: FileList) {
    this.fileToUpload = file[0];
  }

  resetForm(form?: NgForm){
    if(form != null){
      form.reset();
    }
    this.title = "";
    this.description = "";
    this.selectedAccessType = "";
  }

  onSubmit(form: NgForm): void {

      this._fileService.UploadFile(this.title, this.description, this.selectedAccessType, this.fileToUpload)
      .subscribe(() => {
        if(form != null){
          form.reset();
        }
        this.resetForm(form);
        this.router.navigate(['/files']);
      }, 
      (err: HttpErrorResponse)=>{
        this.err=err.status;
      });
    }
  }


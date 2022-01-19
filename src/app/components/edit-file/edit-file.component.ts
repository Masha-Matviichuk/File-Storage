import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Access } from 'src/app/models/access';
import { EditFile } from 'src/app/models/edit-file';
import { FileInfo } from 'src/app/models/file-info';
import { CommonService } from 'src/app/services/common.service';
import { FileService } from 'src/app/services/file.service';
import { FileInfoComponent } from '../file-info/file-info.component';

@Component({
  selector: 'app-edit-file',
  templateUrl: './edit-file.component.html',
  styleUrls: ['./edit-file.component.css']
})
export class EditFileComponent implements OnInit {

  accessTypes: Access[];
  selectedAccessType: string;
  fileToEdit: File ;
  title: string;
  description: string;
  file: EditFile = new EditFile();
  id: number;
  err: number;
  
  

  constructor(private _fileService: FileService,
    private _commonService: CommonService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
     this.id = +this.route.snapshot.paramMap.get('id')!;
    this.getFileInfo();
    this.getAccessType();
   
  }

  getAccessType(){
    this._commonService.getFilesAccess().subscribe(accessTypes => this.accessTypes = accessTypes);
  }

  chooseAccessType(event: any){
    this.selectedAccessType = event.target.id;
    console.log(this.selectedAccessType);
  }

  handleFileInput(file: FileList) {
    this.fileToEdit = file[0];
  }

  onSubmit(title : string, description: string): void {

   if (title===undefined) {
     title=this.file.title;
    }

   if (description==undefined) {
     description=this.file.description;
    }

      this._fileService.EditFile(this.id, title, description, this.selectedAccessType, this.fileToEdit)
      .subscribe(() => {
        this.router.navigate(['/files/'+this.id]);
      }, 
      (err: HttpErrorResponse)=>{
        this.err=err.status;
      });
    }

    getFileInfo(){
      this._fileService.getFileInfo(this.id).subscribe(file => this.file= {
        id: file.id,
        title: file.title,
        description:file.description,
        access:file.access,
      });

}
}

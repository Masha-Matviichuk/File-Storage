import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as saveAs from 'file-saver';
import { FileInfo } from 'src/app/models/file-info';
import { CommonService } from 'src/app/services/common.service';
import { FileService } from 'src/app/services/file.service';

@Component({
  selector: 'app-file-info',
  templateUrl: './file-info.component.html',
  styleUrls: ['./file-info.component.css']
})
export class FileInfoComponent implements OnInit {

  //@Input()

  file!: FileInfo;
  

  constructor(private _fileService: FileService, 
              private route: ActivatedRoute,
              private _commonService: CommonService) { }

  ngOnInit(){
    this.getFileInfo();
  }

  getFileInfo(){
    
    const id = +this.route.snapshot.paramMap.get('id')!;
    this._fileService.getFileInfo(id).subscribe(file => this.file={
      id: file.id,
      title: file.title,
      url: this._commonService.LinkForDownload(file),
      description:file.description,
      access: file.access,
      upload: this._commonService.FormatDate(file.upload),
      size: this._commonService.FormatFileSize(+file.size)
    });
    
}

onDownloadClick() {
  this._fileService.DownloadFile(this.file.id)
  .subscribe(blobData => { saveAs(blobData, this.file.title)});
}
}

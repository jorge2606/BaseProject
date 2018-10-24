import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../_services/authentication.service';
import { FileUploader } from 'ng2-file-upload';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-photo-profile',
  templateUrl: './photo-profile.component.html',
  styleUrls: ['./photo-profile.component.css']
})
export class PhotoProfileComponent implements OnInit {

  constructor(private authService : AuthenticationService) { }

    //image
    uploader:FileUploader;
    hasBaseDropZoneOver = false;
    baseUrl = environment.apiUrl; 
   
    fileOverBase(e:any):void {
      this.hasBaseDropZoneOver = e;
    }
  
  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl+this.authService.userId('id'),
      authToken: 'Bearer ' + this.authService.userId('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onBeforeUploadItem = function (item) {
      item.formData = [{'userId': '60651FA8-4443-4157-AB18-8F3886320362'}];
    };
    debugger
    console.log(this.uploader);

  }



  
  ngOnInit() {
    //image
    this.initializeUploader();
  }

}

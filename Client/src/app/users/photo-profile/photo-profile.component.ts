import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../_services/authentication.service';
import { FileUploader } from 'ng2-file-upload';
import { environment } from '../../../environments/environment';
import { Subject } from 'rxjs';
import { MessBetweenCompService } from '../../_services/mess-between-comp.service';

@Component({
  selector: 'app-photo-profile',
  templateUrl: './photo-profile.component.html',
  styleUrls: ['./photo-profile.component.css']
})
export class PhotoProfileComponent implements OnInit {

  constructor(private authService : AuthenticationService, 
              private messaBetweenComp : MessBetweenCompService) { }

    //image
    uploader:FileUploader;
    hasBaseDropZoneOver = false;
    baseUrl = environment.apiUrl; 
    idUser : number;
    urlImage : string;
    subject = new Subject<any>();
    image : Blob;
   
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

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        this.urlImage = this.authService.urlFile(this.idUser, 200,200) + "r=" + (Math.random() * 100) + 1;
        this.messaBetweenComp.sendMessage(this.urlImage);
      }
    }
    
  }

  ngOnInit() {
    //image
    this.initializeUploader();
    this.idUser = this.authService.userId('id');
    this.urlImage = this.authService.urlFile(this.idUser, 200,200);
  }

}

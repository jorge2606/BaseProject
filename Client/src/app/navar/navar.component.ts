import { AuthenticationService } from './../_services/authentication.service';
import { Observable } from 'rxjs';
import { Notifications } from './../_models/notifications';
import { NotificationsService } from './../_services/notifications.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navar',
  templateUrl: './navar.component.html',
  styleUrls: ['./navar.component.css']
})
export class NavarComponent implements OnInit {

  constructor(private notificaionServices : NotificationsService, private authService : AuthenticationService) { }

  notification : Notifications[];
  isLogged : Observable<boolean>;
  idUser : number;
  urlImage : string;

  ngOnInit() {
    this.notificaionServices.getAllNotifications().subscribe(
      x => this.notification = x
    );
    
    this.idUser = this.authService.userId('id');
    
    this.isLogged = this.authService.isLoggedIn;
    
    this.urlImage = this.authService.urlFile(this.idUser, 25,25);
      
  }

  logout(){
    this.authService.logout();
  }
}

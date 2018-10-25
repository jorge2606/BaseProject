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
    
    this.urlImage = "http://localhost:63098/StaticFiles/Profile/b2163009-9256-4357-bcf0-b21e86d960e0/155570_195329_1.jpg";
  }

  logout(){
    this.authService.logout();
  }
}

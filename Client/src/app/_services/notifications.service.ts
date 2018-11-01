import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Notifications } from '../_models/notifications';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  constructor(private http : HttpClient) { }

  getAllNotifications(){
    return this.http.get<Notifications[]>("http://localhost:63098/api/Notification/GetAllNotifications");
  }

  notificationRidden(id : Notifications){
    return this.http.put<Notifications>("http://localhost:63098/api/Notification/NotificationRidden/",id);
  }

}

import { NotificationsService } from './../../_services/notifications.service';
import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Notifications } from '../../_models/notifications';

@Component({
  selector: 'app-list-notifications',
  templateUrl: './list-notifications.component.html',
  styleUrls: ['./list-notifications.component.css']
})
export class ListNotificationsComponent {

  Contenido: Notifications[];
  @Input() Encabezado;
  @Input() button;
  col_size: number;
  itemsPerPage: number = 5;
  page = 0;
  verOcultar: string = 'angle-down';
  notificationRidden = new Notifications();
  colapseOrNo: boolean;
  public isCollapsed = true;

  constructor(public activeModal: NgbActiveModal, private notifService: NotificationsService) { }

  ngOnInit() {
    this.getAllNotifications(this.page);
  }

  loadPage(page: number) {
    if (page != 0) {
      this.getAllNotifications(page - 1);
    }
  }

  getAllNotifications(page: number): void {
    this.notifService.getPaginator(page).subscribe(result => {
      this.Contenido = result.list,
        this.col_size = result.totalRecords
    }
    )
  }

  delete(id: number) {
    this.notifService.delete(id).subscribe(
      x => this.getAllNotifications(this.page),
      error => {
        console.log("error", error);
      }
    );
  }

  ocultar(event: boolean) {
    this.isCollapsed = event;
  }

  mostrar(event: boolean) {
    this.isCollapsed = !event
  }

  onChange(notification: any) {
    console.log(notification);
    //this.isCollapsed = !this.isCollapsed;

    if (this.verOcultar == 'angle-down') {
      this.verOcultar = 'angle-up';
    } else {
      this.verOcultar = 'angle-down';
    }

    //si no esta leido que vaya a la base de datos y actualize
    //colapsable abierto
    if (notification.read == false) {

      this.notifService.notificationRidden(notification).subscribe(
        //x => this.getAllNotifications(this.page),
        () => {
            notification.colapse = !notification.colapse,
            notification.read = !notification.colapse;
        },
        error => {
          console.log("error", error);
        }
      );
    } else {
      //cerramos el colapsable
      notification.colapse = !notification.colapse;
    }
  }
}

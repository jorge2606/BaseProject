import { NotificationsService } from './../../_services/notifications.service';
import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-list-notifications',
  templateUrl: './list-notifications.component.html',
  styleUrls: ['./list-notifications.component.css']
})
export class ListNotificationsComponent {

  Contenido : Notification[];
  @Input() Encabezado;
  @Input() button;
  col_size: number;
  itemsPerPage : number = 5;
  page = 0;

  public isCollapsed = true;

  constructor(public activeModal: NgbActiveModal, private notifService : NotificationsService) { }

  ngOnInit() {
    this.getAllNotifications(this.page);
  }

  loadPage(page : number){
    if (page != 0){
      this.getAllNotifications(page-1);
    }
  }

  getAllNotifications(page : number): void {
    this.notifService.getPaginator(page).subscribe(result =>{
      this.Contenido = result.list,
      this.col_size = result.totalRecords
    }
    )
  }

  delete(id : number){
    this.notifService.delete(id).subscribe(
      x =>this.getAllNotifications(this.page),
      error => {
          console.log("error", error);
      }
    );
  }

}

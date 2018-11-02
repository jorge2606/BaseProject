import { AuthenticationService } from './../_services/authentication.service';
import { Observable } from 'rxjs';
import { Notifications } from './../_models/notifications';
import { NotificationsService } from './../_services/notifications.service';
import { Component, OnInit, Input } from '@angular/core';
import { MessBetweenCompService } from '../_services/mess-between-comp.service';
import { NgbdModalContent } from '../modals/modals.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from '../_services/user.service';
import { ListNotificationsComponent } from '../modals/list-notifications/list-notifications.component';

@Component({
  selector: 'app-navar',
  templateUrl: './navar.component.html',
  styleUrls: ['./navar.component.css']
})
export class NavarComponent implements OnInit {

  constructor(private notificaionServices : NotificationsService, 
              private authService : AuthenticationService,
              private messaBetweenComp : MessBetweenCompService,
              private modalService: NgbModal,
              private var_user_service: UserService ) { }

  notification : Notifications[];
  notificationridden = new Notifications();
  isLogged : Observable<boolean>;
  idUser : number;
  @Input() urlImage : string;


  ngOnInit() {
    this.notificaionServices.getAllNotifications().subscribe(
      x => this.notification = x
    );
    
    this.idUser = this.authService.userId('id');
    
    this.isLogged = this.authService.isLoggedIn;
    
    this.messaBetweenComp.getMessage().subscribe( x=> 
      this.urlImage = this.authService.urlFile(this.idUser, 25,25)+ "r=" + (Math.random() * 100) + 1 );
    
    if (!this.urlImage){
      this.urlImage = this.authService.urlFile(this.idUser, 25,25)+ "r=" + (Math.random() * 100) + 1;
    }
    
  }

  logout(){
    this.authService.logout();
  }

  //MODALS
  seeThisNotification(notif : String, id : number) {
    const modalRef = this.modalService.open(NgbdModalContent);
    modalRef.componentInstance.Encabezado = "Notificación";
    modalRef.componentInstance.Contenido = notif;
    modalRef.componentInstance.GuardaroEliminar = "Entendido";
    modalRef.result.then(() => {
      this.notificationridden.id = id,
      this.notificationridden.read = true,
      this.notificationridden.textData = "";
      this.notificationridden.tittle = "" 
      this.notificaionServices.notificationRidden(this.notificationridden).subscribe(
        x => this.notificaionServices.getAllNotifications().subscribe(
          x => console.log(x)
          
        )
      )
    },
      () => {
        console.log('Backdrop click');
    })
  }

  seeAllNotification() {
    const modalRef = this.modalService.open(ListNotificationsComponent);
    modalRef.componentInstance.Encabezado = "Todas las Notificaciones";
    modalRef.componentInstance.button = "Entendido";
    modalRef.result.then(() => {
      /*this.notificationridden.id = id,
      this.notificationridden.read = true,
      this.notificationridden.textData = "";
      this.notificationridden.tittle = "" 
      this.notificaionServices.notificationRidden(this.notificationridden).subscribe(
        x => this.notificaionServices.getAllNotifications().subscribe(
          x => console.log(x)
          
        )
      )*/
    },
      () => {
        console.log('Backdrop click');
    })
  }  

}

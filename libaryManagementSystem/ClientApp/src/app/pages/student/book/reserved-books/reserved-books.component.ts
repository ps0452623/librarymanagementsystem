import { Component, OnInit } from '@angular/core';
import { ReservationService } from '@services/reservation.service';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-reserved-books',
  templateUrl: './reserved-books.component.html',
  styleUrl: './reserved-books.component.scss'
})
export class ReservedBooksComponent implements OnInit {
  Reservations$: Observable<any[]>;
    imageUrl = environment.imageUploadUrl;
  
    
  

constructor (private reservationService : ReservationService){}


  ngOnInit(): void {

    this.GetAllReservations();

    }

  
  GetAllReservations(): void {
    this.Reservations$ = this.reservationService.GetAllReservations(); 

  }
}

import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { ApiServiceService } from './api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
private endpoint = 'BookReservation'; // Endpoint\
private apiURL = environment.baseUrl

// private imageUploadUrl = environment.imageUploadUrl;
  constructor(private Http: HttpClient, private apiService: ApiServiceService) { }

    ReserveBook(reservationData: any): Observable<any> {
      return this.apiService.post(this.endpoint + `/create`, reservationData);
}
}
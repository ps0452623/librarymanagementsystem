import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { ApiServiceService } from './api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
  
})
export class BookReservationStatusService {
  apiURL= environment.baseUrl;
  endpoint="Status";


//   constructor(private apiService: ApiServiceService)
//    {}
//    GetAllStatus(): Observable<any[]>{
//     return this.apiService.get(`${this.endpoint}/GetAll`)
//    }
//    GetStatusByid(id: FormData): Observable<any[]>
//    {
// return this.apiService.get(`${this.endpoint}/${id}`)
//    }
}

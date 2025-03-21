import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { ApiServiceService } from './api.service';
@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  private endpoint = 'auth'; // Endpoint
private apiURL = environment.baseUrl


  constructor(private Http: HttpClient, private apiService: ApiServiceService) { }

  // register user
  
RegisterUser(userdata: any): Observable<any> {
    return this.apiService.post(`auth/Register`, userdata);
  }
  login(userdata: any): Observable<any> {
    return this.apiService.post(`auth/Login`, userdata);
  }
  
  getRoles(): Observable<any[]> {
    return this.apiService.get(`role/GetAllRoles`);
  }
}
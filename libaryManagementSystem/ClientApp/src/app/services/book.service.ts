import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { ApiServiceService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class BookService {
private endpoint = 'Book'; // Endpoint
private apiURL = environment.baseUrl
  constructor(private Http: HttpClient, private apiService: ApiServiceService) { }

}

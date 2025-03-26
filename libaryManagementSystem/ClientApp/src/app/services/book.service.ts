import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { ApiServiceService } from './api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookService {
private endpoint = 'Book'; // Endpoint
private apiURL = environment.baseUrl

// private imageUploadUrl = environment.imageUploadUrl;
  constructor(private Http: HttpClient, private apiService: ApiServiceService) { }

  GetBooks(): Observable<any[]> {
      return this.apiService.get(`${this.endpoint}/GetAll`);
}
 AddBooks(bookdata: FormData): Observable<any[]>{
  return this.apiService.post(`${this.endpoint}/Add`, bookdata);
}
UpdateBook(bookId: any, bookdata: FormData): Observable<any[]> {
  return this.apiService.put(`${this.endpoint}/${bookId}/Update`, bookdata);
}
DeleteBook(bookId:any): Observable<any[]>{
  return this.apiService.delete(`${this.endpoint}/${bookId}/Delete`);
}

}
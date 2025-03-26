import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { ApiServiceService } from './api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookService {
private endpoint = 'Book'; // Endpoint\
private apiURL = environment.baseUrl
  constructor(private Http: HttpClient, private apiService: ApiServiceService) { }

  GetBooks(): Observable<any[]> {
      return this.apiService.get(this.endpoint+`/GetAll`);
}
 AddBooks(bookdata: any){
  return this.apiService.post(this.endpoint+`/Add`, bookdata);
}
GetCourse(): Observable<any[]> {
  return this.apiService.get(`Course/GetAll`);
}
GetBranchesByCourse(courseId: number): Observable<any[]> {
  return this.apiService.get(`Branch/GetByCourse`);
}

// ✅ Get Books with Pagination & Filters
GetFilteredBooks(searchbook: any): Observable<any> { 
  return this.apiService.post(this.endpoint+`/Search-Books`, searchbook);
      }
    }


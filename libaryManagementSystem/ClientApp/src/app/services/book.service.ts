import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { ApiServiceService } from './api.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Book {
  id: string;
  title: string;
  author: string;
  publisher: string;
  genre: string;
  isbn: string;
  yearPublished: number;
  copiesAvailable: number;
  bookShelfNumber: number;
  picture: string;
}

@Injectable({
  providedIn: 'root'
})
export class BookService {
private endpoint = 'Book'; // Endpoint
private apiURL = environment.baseUrl
  constructor(private Http: HttpClient, private apiService: ApiServiceService) { }

  deleteBook(id: string): Observable<void> {  
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}

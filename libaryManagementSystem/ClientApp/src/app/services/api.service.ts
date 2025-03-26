import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiServiceService {
  private apiURL = environment.baseUrl
  constructor(private http: HttpClient) { }
  // GET Request
  get<T>(endpoint: string, filters?: any): Observable<T> {
    let params = new HttpParams();

    // ✅ Convert filters object to query params
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key] !== null && filters[key] !== '') {
          params = params.append(key, filters[key]); // Correct way to append query params
        }
      });
    }
    return this.http.get<T>(`${this.apiURL}/${endpoint}`, {  params }).pipe(
      catchError(this.handleError)
    );
  }

  // POST Request
  post<T>(endpoint: string, data: any): Observable<T> {
    return this.http.post<T>(`${this.apiURL}/${endpoint}`, data).pipe(
      catchError(this.handleError)
    );
  }

  // PUT Request
  put<T>(endpoint: string, data: any): Observable<T> {
    return this.http.put<T>(`${this.apiURL}/${endpoint}`, data).pipe(
      catchError(this.handleError)
    );
  }

  // DELETE Request
  delete<T>(endpoint: string): Observable<T> {
    return this.http.delete<T>(`${this.apiURL}/${endpoint}`).pipe(
      catchError(this.handleError)
    );
  }

  // Handle API Errors
  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred!';
    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(() => new Error(errorMessage));
  }
}